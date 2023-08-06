using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Traything.Data;

namespace Traything.UI
{
	public partial class FrmMain : Form
	{
		public FrmMain()
		{
			InitializeComponent();

			this.ConfigFileWatcher = new FileSystemWatcher();
		}

		private Settings Settings;
		private readonly List<FrmBrowser> Browsers = new List<FrmBrowser>();
		private readonly List<FrmVlcPlayer> Players = new List<FrmVlcPlayer>();
		private FileSystemWatcher ConfigFileWatcher;

		internal void SaveSettings()
		{
			this.Settings.Save();
		}

		private void FrmMain_Shown(object sender, EventArgs e)
		{
			this.Hide();
			this.Reload();

			if (File.Exists(Settings._Path))
			{
				this.ConfigFileWatcher.Path = Path.GetDirectoryName(Settings._Path);
				this.ConfigFileWatcher.Filter = Path.GetFileName(Settings._Path);
				this.ConfigFileWatcher.Changed += ConfigFileWatcher_Changed;
				this.ConfigFileWatcher.EnableRaisingEvents = true;
			}
		}

		private void ConfigFileWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			Task.Delay(1000).ContinueWith(x => this.Invoke(new Action(() =>
			{
				this.Reload();
			})));
		}

		private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
		}

		private void NotifyIconTray_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.Show();
				this.Activate();
			}
		}

		private void ButtonEdit_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.ListBoxActions.SelectedIndex;

			if (new FrmEditActionItem((ActionItem)this.ListBoxActions.SelectedItem).ShowDialog(this) == DialogResult.Cancel)
			{
				this.Reload();
			}
			else
			{
				this.ConfigFileWatcher.EnableRaisingEvents = false;
				this.Settings.Save();
				this.ConfigFileWatcher.EnableRaisingEvents = true;
			}

			this.Reload();
			this.ListBoxActions.SelectedIndex = selectedIndex;
		}

		private void Reload()
		{
			this.Settings = Settings.Load();

			this.ListBoxActions.Items.Clear();
			this.ContextMenuStripTray.Items.Clear();
			foreach (ActionItem item in this.Settings.Actions)
			{
				this.ListBoxActions.Items.Add(item);

				if (item.Scope == ActionScope.Global || (item.Scope == ActionScope.ThisComputer && item.Hostnames.Any(x => x.Equals(Environment.MachineName, StringComparison.OrdinalIgnoreCase))))
				{
					if (item.Type == ActionType.Separator)
					{
						this.ContextMenuStripTray.Items.Add(new ToolStripSeparator());
					}
					else if (item.Type == ActionType.Headline)
					{
						this.ContextMenuStripTray.Items.Add(new ToolStripMenuItem() { Text = item.Name, Enabled = false });
					}
					else
					{
						ToolStripMenuItem menuItem = new ToolStripMenuItem();
						menuItem.Text = item.Name;
						menuItem.Tag = item;
						menuItem.Click += this.TrayMenuItem_Click;
						this.ContextMenuStripTray.Items.Add(menuItem);
					}
				}
			}

			if (this.Settings.Actions.Any(x => x.Type == ActionType.ShowTrayBrowser && (x.Scope == ActionScope.Global || (x.Scope == ActionScope.ThisComputer && x.Hostnames.Any(y => y.Equals(Environment.MachineName, StringComparison.OrdinalIgnoreCase))))))
			{
				// Preheat CefSharp (one instance)
				if (this.Browsers.Count == 0)
				{
					this.Browsers.Add(new FrmBrowser(this));
				}
			}
			else
			{
				// Shutdown CefSharp
				if (this.Browsers.Count > 0)
				{
					foreach (FrmBrowser browser in this.Browsers.ToList())
					{
						browser.ReallyClose();
						this.Browsers.Remove(browser);
					}
				}
			}

			if (this.Settings.Actions.Any(x => x.Type == ActionType.ShowTrayMediaPlayer && (x.Scope == ActionScope.Global || (x.Scope == ActionScope.ThisComputer && x.Hostnames.Any(y => y.Equals(Environment.MachineName, StringComparison.OrdinalIgnoreCase))))))
			{
				// Preheat VLC (one instance)
				if (this.Players.Count == 0)
				{
					this.Players.Add(new FrmVlcPlayer(this));
				}
			}
			else
			{
				// Shutdown VLC
				if (this.Players.Count > 0)
				{
					foreach (FrmVlcPlayer player in this.Players.ToList())
					{
						player.ReallyClose();
						this.Players.Remove(player);
					}
				}
			}
		}

		private void TrayMenuItem_Click(object sender, EventArgs e)
		{
			this.ExecuteAction((ActionItem)((ToolStripMenuItem)sender).Tag);
		}

		private void ExecuteAction(ActionItem item)
		{
			try
			{
				item.PathOrUrlReplaced = item.PathOrUrl;

				if (item.PathOrUrlReplaced != null && item.PathOrUrlReplaced.Contains("{QUERYSTRING"))
				{
					foreach (Match placeholder in Regex.Matches(item.PathOrUrlReplaced, "{(.*?)}"))
					{
						string prompt = "Enter placeholder value";
						if (placeholder.Groups[1].Value.Contains(":"))
						{
							prompt += $" ({placeholder.Groups[1].Value.Split(':')[1]})";
						}

						using (FrmQueryString dialog = new FrmQueryString(item, prompt))
						{
							if (dialog.ShowDialog(this) == DialogResult.OK)
							{
								item.PathOrUrlReplaced = item.PathOrUrlReplaced.Replace(placeholder.Value, dialog.QueryString);
							}
							else
							{
								return;
							}
						}
					}
				}

				if (item.Type == ActionType.CloseTraything)
				{
					if (this.Browsers.Count > 0)
					{
						foreach (FrmBrowser browser in this.Browsers)
						{
							browser.ReallyClose();
						}
					}
					if (this.Players.Count > 0)
					{
						foreach (FrmVlcPlayer player in this.Players)
						{
							player.ReallyClose();
						}
					}

					this.FormClosing -= this.FrmMain_FormClosing;
					this.Close();
				}
				else if (item.Type == ActionType.StartApplication)
				{
					ProcessStartInfo si = new ProcessStartInfo("cmd", " /c start \"\" " + item.Commandline);
					si.CreateNoWindow = true;
					si.UseShellExecute = true;
					Process.Start(si);
				}
				else if (item.Type == ActionType.HttpGetRequest)
				{
					WebClient wc = new WebClient();
					foreach (string header in item.Headers)
					{
						wc.Headers.Add(header);
					}
					wc.DownloadString(item.Url);
				}
				else if (item.Type == ActionType.HttpPostRequest)
				{
					WebClient wc = new WebClient();
					foreach (string header in item.Headers)
					{
						wc.Headers.Add(header);
					}
					wc.UploadString(item.Url, item.PostData);
				}
				else if (item.Type == ActionType.ShowTrayBrowser)
				{
					this.GetAvailableBrowser().ShowTrayForm(item);
				}
				else if (item.Type == ActionType.ShowTrayMediaPlayer)
				{
					this.GetAvailableVlcPlayer().ShowTrayForm(item);
				}
			}
			catch (Exception ex)
			{
				if (!item.IgnoreErrors)
				{
					MessageBox.Show(ex.Message, "Error while executing action", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void LinkLabelAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			new FrmAbout().ShowDialog(this);
		}

		private void ButtonAdd_Click(object sender, EventArgs e)
		{
			FrmEditActionItem dlg = new FrmEditActionItem(new ActionItem());
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				this.Settings.Actions.Add(dlg.EditItem);
				this.ConfigFileWatcher.EnableRaisingEvents = false;
				this.Settings.Save();
				this.ConfigFileWatcher.EnableRaisingEvents = true;
				this.Reload();
				this.ListBoxActions.SelectedIndex = this.ListBoxActions.Items.Count - 1;
			}
		}

		private void ButtonRemove_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure to remove \"" + this.ListBoxActions.SelectedItem.ToString() + "\"?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				this.Settings.Actions.Remove((ActionItem)this.ListBoxActions.SelectedItem);
				this.ConfigFileWatcher.EnableRaisingEvents = false;
				this.Settings.Save();
				this.ConfigFileWatcher.EnableRaisingEvents = true;
				this.Reload();

				this.ButtonEdit.Enabled = false;
				this.ButtonRemove.Enabled = false;
				this.ButtonMoveUp.Enabled = false;
				this.ButtonMoveDown.Enabled = false;
			}
		}

		private void ButtonMoveUp_Click(object sender, EventArgs e)
		{
			int newIndex = this.ListBoxActions.SelectedIndex - 1;

			this.Settings.Actions.RemoveAt(this.ListBoxActions.SelectedIndex);
			this.Settings.Actions.Insert(newIndex, (ActionItem)this.ListBoxActions.SelectedItem);
			this.ConfigFileWatcher.EnableRaisingEvents = false;
			this.Settings.Save();
			this.ConfigFileWatcher.EnableRaisingEvents = true;
			this.Reload();
			this.ListBoxActions.SelectedIndex = newIndex;
		}

		private void ButtonMoveDown_Click(object sender, EventArgs e)
		{
			int newIndex = this.ListBoxActions.SelectedIndex + 1;

			this.Settings.Actions.RemoveAt(this.ListBoxActions.SelectedIndex);
			this.Settings.Actions.Insert(newIndex, (ActionItem)this.ListBoxActions.SelectedItem);
			this.ConfigFileWatcher.EnableRaisingEvents = false;
			this.Settings.Save();
			this.ConfigFileWatcher.EnableRaisingEvents = true;
			this.Reload();
			this.ListBoxActions.SelectedIndex = newIndex;
		}

		private void ListBoxActions_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ButtonEdit.Enabled = true;
			this.ButtonRemove.Enabled = true;
			this.ButtonMoveUp.Enabled = this.ListBoxActions.SelectedIndex != 0;
			this.ButtonMoveDown.Enabled = this.ListBoxActions.SelectedIndex != this.ListBoxActions.Items.Count - 1;
			this.ButtonExecute.Enabled = this.ListBoxActions.SelectedItems.Count == 1;
		}

		private void ListBoxActions_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (this.ListBoxActions.SelectedItem != null)
			{
				this.ButtonEdit.PerformClick();
			}
		}

		private FrmBrowser GetAvailableBrowser()
		{
			FrmBrowser availableBrowser = this.Browsers.Find(x => x.Visible == false);

			if (availableBrowser == null)
			{
				availableBrowser = new FrmBrowser(this);
				this.Browsers.Add(availableBrowser);
			}

			return availableBrowser;
		}

		private FrmVlcPlayer GetAvailableVlcPlayer()
		{
			FrmVlcPlayer availablePlayer = this.Players.Find(x => x.Visible == false);

			if (availablePlayer == null)
			{
				availablePlayer = new FrmVlcPlayer(this);
				this.Players.Add(availablePlayer);
			}

			return availablePlayer;
		}

		private void ButtonExecute_Click(object sender, EventArgs e)
		{
			this.ExecuteAction((ActionItem)this.ListBoxActions.SelectedItem);
		}
	}
}
