using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Traything.Data;

namespace Traything.UI
{
	public partial class FrmVlcPlayer : BaseTrayForm
	{
		public FrmVlcPlayer(FrmMain parent)
		{
			InitializeComponent();
			this.Parent = parent;
			this.Show();
		}

		private LibVLC VlcLib;
		private VideoView VlcVideoView;
		private Media VlcMedia;
		private TransparentPanel VlcPlayerOverlayPanel;
		private DateTime PlaybackStartTime;
		private bool FirstPlaybackStarted = false;
		private bool OverlaySingleClick = true;

		private void SetupVlc()
		{
			Core.Initialize(Path.Combine(Program.BaseExecutingPath, "libvlc\\win-x64"));
			this.VlcLib = new LibVLC();
			this.VlcLib.SetDialogHandlers(this.VlcDlgError, this.VlcDlgLogin, this.VlcDlgQuestion, this.VlcDlgDisplayProgress, this.VlcDlgUpdateProgress);

			this.VlcVideoView = new VideoView();
			this.VlcVideoView.MediaPlayer = new MediaPlayer(this.VlcLib);
			this.VlcVideoView.MediaPlayer.Playing += MediaPlayer_Playing;
			this.VlcVideoView.MediaPlayer.Paused += MediaPlayer_Paused;
			this.VlcVideoView.MediaPlayer.EndReached += MediaPlayer_EndReached;
			this.VlcVideoView.MediaPlayer.Buffering += MediaPlayer_Buffering;

			this.VlcVideoView.Dock = DockStyle.Fill;
			this.PanelVlcPlayerContainer.Controls.Add(this.VlcVideoView);

			this.VlcPlayerOverlayPanel = new TransparentPanel();
			this.VlcPlayerOverlayPanel.Dock = DockStyle.Fill;
			this.VlcPlayerOverlayPanel.ContextMenuStrip = this.ContextMenuStripVlcPlayerOverlayPanel;
			this.PanelVlcPlayerContainer.Controls.Add(this.VlcPlayerOverlayPanel);
			this.VlcPlayerOverlayPanel.BringToFront();
			this.VlcPlayerOverlayPanel.MouseClick += VlcPlayerOverlayPanel_MouseClick;
			this.VlcPlayerOverlayPanel.MouseDoubleClick += VlcPlayerOverlayPanel_MouseDoubleClick; ;
		}

		private void VlcPlayerOverlayPanel_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.OverlaySingleClick = false;

			// Double click handling
			if (e.Button == MouseButtons.Left)
			{
				if (this.WindowState == FormWindowState.Maximized)
				{
					this.WindowState = FormWindowState.Normal;
				}
				else
				{
					this.WindowState = FormWindowState.Maximized;
				}
			}
		}

		private async void VlcPlayerOverlayPanel_MouseClick(object sender, MouseEventArgs e)
		{
			this.OverlaySingleClick = true;
			await Task.Delay(SystemInformation.DoubleClickTime);

			if (!this.OverlaySingleClick)
			{
				return;
			}

			// Single click handling
			if (e.Button == MouseButtons.Left && this.ButtonPlayPause.Enabled)
			{
				this.VlcVideoView.MediaPlayer.Pause();
			}
		}

		private void MediaPlayer_Playing(object sender, EventArgs e)
		{
			this.BeginInvoke(new Action(() =>
			{
				this.ButtonPlayPause.Text = "Pause";
				this.ProgressBarBusy.Visible = false;
				this.LabelPlayTime.Visible = true;
				this.TrackBarPlayProgress.Visible = true;
			}));
		}

		private void MediaPlayer_Paused(object sender, EventArgs e)
		{
			this.BeginInvoke(new Action(() =>
			{
				this.ButtonPlayPause.Text = "Resume";
			}));
		}

		private void MediaPlayer_EndReached(object sender, EventArgs e)
		{
			this.TimerUpdatePlayProgress.Stop();

			this.BeginInvoke(new Action(() =>
			{
				this.ButtonPlayPause.Text = "Play";
			}));

			if (this.VlcMedia.SubItems.Count > 0) // Play next item when it's a playlist
			{
				this.BeginInvoke(new Action(() =>
				{
					this.ButtonPlaylistNext.PerformClick();
				}));
			}
		}

		private void MediaPlayer_Buffering(object sender, MediaPlayerBufferingEventArgs e)
		{
			if (!this.FirstPlaybackStarted && this.VlcMedia.Tracks.Count() > 0)
			{
				this.FirstPlaybackStarted = true;

				if (this.ActionItem.StartMuted)
				{
					this.ToggleMute();
				}
			}
		}

		private void FrmVlcPlayer_Shown(object sender, EventArgs e)
		{
			this.SetupVlc();
		}

		public override void ShowTrayForm(ActionItem item, List<ActionItem> inplaceActions)
		{
			while (!this.Ready)
			{
				Application.DoEvents();
			}

			this.FirstPlaybackStarted = false;
			this.LoadMediaAndPlay(item.PathOrUrlReplaced);
			base.ShowTrayForm(item, inplaceActions);
			this.UpdateTitle();

			if (item.StartFullscreen)
			{
				this.ToggleFullscreen();
			}

			if (item.StartMinimized)
			{
				this.WindowState = FormWindowState.Minimized;
			}

			int i = 1;
			if (inplaceActions != null && inplaceActions.Count > 0)
			{
				this.ToolStripSeparatorInplaceActions.Visible = true;

				foreach (ActionItem action in inplaceActions)
				{
					ToolStripMenuItem menuItem = new ToolStripMenuItem(action.Name);
					menuItem.Tag = action;
					menuItem.Click += this.InplaceActionMenuItem_Click;
					this.ContextMenuStripVlcPlayerOverlayPanel.Items.Insert(this.ContextMenuStripVlcPlayerOverlayPanel.Items.IndexOf(this.ToolStripSeparatorInplaceActions) + i, menuItem);
					i++;
				}
			}

			// Always insert one dummy in-place action menu item
			// When not doing this, the context menu stayed empty when not having any in-place actions (however)
			ToolStripMenuItem dummyMenuItem = new ToolStripMenuItem();
			dummyMenuItem.Visible = false;
			this.ContextMenuStripVlcPlayerOverlayPanel.Items.Insert(this.ContextMenuStripVlcPlayerOverlayPanel.Items.IndexOf(this.ToolStripSeparatorInplaceActions) + i, dummyMenuItem);
		}

		private void LoadMediaAndPlay(string pathOrUrl)
		{
			this.VlcMedia = new Media(this.VlcLib, new Uri(pathOrUrl));

			// Only parse media if it's (most likely) a playlist
			bool isPlayList = pathOrUrl.ToLower().Contains(".m3u") || pathOrUrl.ToLower().Contains(".m3u8");
			if (isPlayList)
			{
				this.VlcMedia.Parse(MediaParseOptions.ParseNetwork).Wait();
			}

			if (this.VlcMedia.SubItems.Count == 0 || !isPlayList)
			{
				// Single item / no playlist
				this.ButtonPlaylistNext.Visible = false;
				this.PlayMedia(this.VlcMedia);
			}
			else
			{
				// Multiple items / playlist
				this.ButtonPlaylistNext.Visible = true;
				this.PlayMedia(this.VlcMedia.SubItems.First());
			}
		}

		private void PlayMedia(Media media)
		{
			this.BeginInvoke(new Action(() =>
			{
				this.ProgressBarBusy.Visible = true;
				this.LabelPlayTime.Visible = false;
				this.LabelPlayTime.Text = "";
				this.TrackBarPlayProgress.Visible = false;
				this.ButtonPlayPause.Enabled = false;
				this.TimerUpdatePlayProgress.Start();
			}));

			this.PlaybackStartTime = DateTime.Now;
			this.VlcVideoView.MediaPlayer.Play(media);
			this.UpdateTitle();
		}

		private void UpdateTitle()
		{
			if (this.ActionItem == null)
			{
				return;
			}

			string mediaTitle = this.VlcVideoView.MediaPlayer.Media.Meta(MetadataType.Title);
			if (this.ActionItem.ShowMediaTitle && !string.IsNullOrEmpty(mediaTitle))
			{
				this.Text = $"{this.ActionItem.Name} [{mediaTitle}]";
			}
			else
			{
				this.Text = this.ActionItem.Name;
			}
		}

		private void FrmVlcPlayer_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.Fullscreen_On)
			{
				this.ToggleFullscreen();
			}

			this.VlcVideoView.MediaPlayer.Stop();
			this.TimerUpdatePlayProgress.Stop();
			this.ContextMenuStripPlaylist.Items.Clear();
			this.Mute_On = false;
			this.ToolStripSeparatorInplaceActions.Visible = false;

			// Clear inplace action context menu items
			List<ToolStripItem> itemsToRemove = new List<ToolStripItem>(); // Enumeration cannot be changed while it is being enumerated
			int minIndex = this.ContextMenuStripVlcPlayerOverlayPanel.Items.IndexOf(this.ToolStripSeparatorInplaceActions);
			int maxIndex = this.ContextMenuStripVlcPlayerOverlayPanel.Items.IndexOf(this.ToolStripSeparatorClose);
			foreach (ToolStripItem item in this.ContextMenuStripVlcPlayerOverlayPanel.Items)
			{
				if (this.ContextMenuStripVlcPlayerOverlayPanel.Items.IndexOf(item) > minIndex && this.ContextMenuStripVlcPlayerOverlayPanel.Items.IndexOf(item) < maxIndex)
				{
					itemsToRemove.Add(item);
				}
			}
			foreach (ToolStripItem item in itemsToRemove)
			{
				this.ContextMenuStripVlcPlayerOverlayPanel.Items.Remove(item);
			}
		}

		private void ButtonPlayPause_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.VlcVideoView.MediaPlayer.Pause();
			}
			else if (e.Button == MouseButtons.Right)
			{
				this.PlayMedia(this.VlcVideoView.MediaPlayer.Media);
			}
		}

		private void TimerUpdatePlayProgress_Tick(object sender, EventArgs e)
		{
			int currentSeconds = Convert.ToInt32(this.VlcVideoView.MediaPlayer.Time / 1000);
			int totalSeconds = Convert.ToInt32(this.VlcVideoView.MediaPlayer.Length / 1000);
			if (currentSeconds < totalSeconds)
			{
				if (this.TrackBarPlayProgress.Maximum != totalSeconds)
				{
					this.TrackBarPlayProgress.Maximum = totalSeconds;
				}

				this.TrackBarPlayProgress.Value = currentSeconds;
				this.TrackBarPlayProgress.Enabled = true;
				this.ButtonPlayPause.Enabled = this.VlcVideoView.MediaPlayer.CanPause;

				this.LabelPlayTime.Text = $"{TimeSpan.FromMilliseconds(this.VlcVideoView.MediaPlayer.Time):hh\\:mm\\:ss} / {TimeSpan.FromMilliseconds(this.VlcVideoView.MediaPlayer.Length):hh\\:mm\\:ss}";
			}
			else
			{
				// Some (live) streams report invalid total length values
				this.TrackBarPlayProgress.Enabled = false;
				this.ButtonPlayPause.Enabled = false;

				this.LabelPlayTime.Text = $"Live / {TimeSpan.FromTicks((DateTime.Now.Subtract(this.PlaybackStartTime)).Ticks):hh\\:mm\\:ss}";
			}

			this.RearrangePlayerControls();
		}

		private void TrackBarPlayProgress_Scroll(object sender, EventArgs e)
		{
			this.VlcVideoView.MediaPlayer.SeekTo(TimeSpan.FromSeconds(this.TrackBarPlayProgress.Value));
		}

		private Task VlcDlgUpdateProgress(Dialog dialog, float position, string text)
		{
			throw new NotImplementedException();
		}

		private Task VlcDlgDisplayProgress(Dialog dialog, string title, string text, bool indeterminate, float position, string cancelText, CancellationToken token)
		{
			throw new NotImplementedException();
		}

		private Task VlcDlgQuestion(Dialog dialog, string title, string text, DialogQuestionType type, string cancelText, string firstActionText, string secondActionText, CancellationToken token)
		{
			// Auto accept stream TLS errors
			dialog.PostAction(1);
			return Task.CompletedTask;
		}

		private Task VlcDlgLogin(Dialog dialog, string title, string text, string defaultUsername, bool askStore, CancellationToken token)
		{
			throw new NotImplementedException();
		}

		private Task VlcDlgError(string title, string text)
		{
			if (!this.ActionItem.IgnoreErrors)
			{
				this.BeginInvoke(new Action(() =>
				{
					MessageBox.Show(text, $"VLC error: {title}", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}));
			}

			return Task.CompletedTask;
		}

		private void ButtonPlaylistNext_Click(object sender, EventArgs e)
		{
			Media nextItem = null;
			if (Control.ModifierKeys == Keys.None)
			{
				// Forwards
				nextItem = this.VlcMedia.SubItems.SkipWhile(x => x.Mrl != this.VlcVideoView.MediaPlayer.Media.Mrl).Skip(1).FirstOrDefault();
			}
			else
			{
				// Backwards
				nextItem = this.VlcMedia.SubItems.TakeWhile(x => x.Mrl != this.VlcVideoView.MediaPlayer.Media.Mrl).LastOrDefault();
			}

			if (nextItem != null)
			{
				this.PlayMedia(nextItem);
			}
		}

		private void ContextMenuStripPlaylist_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			Media desiredItem = this.VlcMedia.SubItems.Where(x => x.Mrl == (string)e.ClickedItem.Tag).FirstOrDefault();
			if (desiredItem != null)
			{
				this.PlayMedia(desiredItem);
			}
		}

		private bool Fullscreen_On = false;
		private FormBorderStyle Fullscreen_SavedBorderStyle;
		private Rectangle Fullscreen_SavedBounds;
		private void ToggleFullscreen()
		{
			if (this.Fullscreen_On)
			{
				// Exit fullscreen
				this.FormBorderStyle = this.Fullscreen_SavedBorderStyle;
				this.Bounds = this.Fullscreen_SavedBounds;
				this.TableLayoutPanelMain.RowStyles[1].Height = 36;
			}
			else
			{
				// Start fullscreen
				this.Fullscreen_SavedBorderStyle = this.FormBorderStyle;
				this.Fullscreen_SavedBounds = this.Bounds;
				this.WindowState = FormWindowState.Normal;
				this.FormBorderStyle = FormBorderStyle.None;
				this.TableLayoutPanelMain.RowStyles[1].Height = 0;
				this.Bounds = Screen.GetBounds(this.Bounds);
			}

			this.Fullscreen_On = !this.Fullscreen_On;
		}

		private void ToolStripMenuItemToggleFullscreen_Click(object sender, EventArgs e)
		{
			this.ToggleFullscreen();
		}

		private void ToolStripMenuItemToggleMute_Click(object sender, EventArgs e)
		{
			this.ToggleMute();
		}

		private void ToolStripMenuItemClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ContextMenuStripPlaylist_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;

			// Load playlist context menu items on-demand / on first context menu opening
			if (this.VlcMedia.SubItems.Count > 0 && this.ContextMenuStripPlaylist.Items.Count == 0)
			{
				// Using a separate list instead of adding each item directly to the ContextMenuStrips significantly improves performance
				List<ToolStripMenuItem> menuItems = new List<ToolStripMenuItem>(this.VlcMedia.SubItems.Count);

				foreach (Media item in this.VlcMedia.SubItems)
				{
					menuItems.Add(new ToolStripMenuItem
					{
						Text = item.Meta(MetadataType.Title),
						Tag = item.Mrl
					});
				}

				this.ContextMenuStripPlaylist.Items.AddRange(menuItems.ToArray());
			}

			foreach (ToolStripMenuItem item in this.ContextMenuStripPlaylist.Items)
			{
				item.Text = item.Text.Replace("▶ ", "");
				if ((string)item.Tag == this.VlcVideoView.MediaPlayer.Media.Mrl)
				{
					item.Text = "▶ " + item.Text;
				}
			}

			Cursor.Current = Cursors.Default;
			e.Cancel = false;
		}

		private bool Mute_On = false;
		private int Mute_SavedAudioTrackIndex;
		private void ToggleMute()
		{
			if (this.Mute_On) // Unmute
			{
				this.VlcVideoView.MediaPlayer.SetAudioTrack(this.Mute_SavedAudioTrackIndex);
			}
			else // Mute
			{
				this.Mute_SavedAudioTrackIndex = this.VlcVideoView.MediaPlayer.AudioTrack;
				this.VlcVideoView.MediaPlayer.SetAudioTrack(-1);
			}

			this.Mute_On = !this.Mute_On;
		}

		private void RearrangePlayerControls()
		{
			// Make the TrackBarPlayProgress use the entire left over window width
			int remainingWidth = this.FlowLayoutPanelPlayerControls.Width;
			foreach (Control item in this.FlowLayoutPanelPlayerControls.Controls)
			{
				if (!(item is TrackBar) && item.Visible)
				{
					remainingWidth -= (item.Width + item.Margin.Left + item.Margin.Right);
				}
			}
			this.TrackBarPlayProgress.Width = (remainingWidth - this.TrackBarPlayProgress.Margin.Left - this.TrackBarPlayProgress.Margin.Right);
		}

		private void InplaceActionMenuItem_Click(object sender, EventArgs e)
		{
			this.Parent.ExecuteAction((ActionItem)((ToolStripMenuItem)sender).Tag);
		}
	}
}

public class TransparentPanel : Panel
{
	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams p = base.CreateParams;
			p.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
			return p;
		}
	}
}
