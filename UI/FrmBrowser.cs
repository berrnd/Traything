using CefSharp;
using CefSharp.WinForms;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Traything.Data;

namespace Traything.UI
{
	public partial class FrmBrowser : BaseTrayForm
	{
		public FrmBrowser(FrmMain parent)
		{
			InitializeComponent();
			this.Parent = parent;
			this.Show();
		}

		private ChromiumWebBrowser Browser;

		private void SetupCef()
		{
			if (!Cef.IsInitialized)
			{
				CefSettings cefSettings = new CefSettings();
				cefSettings.BrowserSubprocessPath = Path.Combine(Program.BaseExecutingPath, @"CefSharp.BrowserSubprocess.exe");
				cefSettings.CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Traything");
				cefSettings.LogSeverity = LogSeverity.Disable;
				cefSettings.CefCommandLineArgs.Add("--enable-media-stream");
				cefSettings.CefCommandLineArgs.Add("--unsafely-treat-insecure-origin-as-secure", "file://");
				cefSettings.CefCommandLineArgs.Add("--allow-running-insecure-content");
				cefSettings.CefCommandLineArgs.Add("--disable-web-security");
				cefSettings.CefCommandLineArgs.Add("--disable-gpu");
				cefSettings.CefCommandLineArgs.Add("--lang", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

				Cef.Initialize(cefSettings, performDependencyCheck: false, browserProcessHandler: null);
			}

			this.Browser = new ChromiumWebBrowser("about:blank");
			this.Browser.Dock = DockStyle.Fill;
			this.Browser.MenuHandler = new BrowserContextMenuHandler();
			this.Browser.FrameLoadEnd += Browser_FrameLoadEnd;
			this.Browser.Tag = this;
			this.Controls.Add(this.Browser);
		}

		private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
		{
			this.Browser.SetZoomLevel(this.ActionItem.BrowserZoomLevel);
		}

		private void FrmBrowser_Shown(object sender, EventArgs e)
		{
			this.SetupCef();
		}

		public override void ShowTrayForm(ActionItem item)
		{
			while (!this.Ready)
			{
				Application.DoEvents();
			}

			this.Browser.Load(item.PathOrUrlReplaced);
			base.ShowTrayForm(item);

			if (item.StartFullscreen)
			{
				this.ToggleFullscreen();
			}
		}

		private void FrmBrowser_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.Browser.IsBrowserInitialized)
			{
				this.Browser.GetZoomLevelAsync().ContinueWith(task =>
				{
					if (task.Result != this.ActionItem.BrowserZoomLevel)
					{
						this.ActionItem.BrowserZoomLevel = task.Result;
						this.Parent.SaveSettings();
					}
				});
			}

			this.Browser.Load("about:blank");

			if (this.Fullscreen_On)
			{
				this.ToggleFullscreen();
			}
		}

		private bool Fullscreen_On = false;
		private FormBorderStyle Fullscreen_SavedBorderStyle;
		private Rectangle Fullscreen_SavedBounds;
		internal void ToggleFullscreen()
		{
			if (this.Fullscreen_On)
			{
				// Exit fullscreen
				this.FormBorderStyle = this.Fullscreen_SavedBorderStyle;
				this.Bounds = this.Fullscreen_SavedBounds;
			}
			else
			{
				// Start fullscreen
				this.Fullscreen_SavedBorderStyle = this.FormBorderStyle;
				this.Fullscreen_SavedBounds = this.Bounds;
				this.WindowState = FormWindowState.Normal;
				this.FormBorderStyle = FormBorderStyle.None;
				this.Bounds = Screen.GetBounds(this.Bounds);
			}

			this.Fullscreen_On = !this.Fullscreen_On;
		}
	}

	public class BrowserContextMenuHandler : IContextMenuHandler
	{
		public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
		{
			model.Clear();

			model.AddItem((CefMenuCommand)26501, browser.MainFrame.Url);
			model.AddSeparator();
			model.AddItem((CefMenuCommand)26521, "Zoom In");
			model.AddItem((CefMenuCommand)26522, "Zoom Out");
			model.AddItem((CefMenuCommand)26523, "Zoom Reset");
			model.AddSeparator();
			model.AddItem((CefMenuCommand)26531, "Toggle fullscreen mode");
			model.AddSeparator();
			model.AddItem((CefMenuCommand)26541, "Close");

		}

		public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
		{
			ChromiumWebBrowser chromiumBrowser = browserControl as ChromiumWebBrowser;
			FrmBrowser parentForm = chromiumBrowser.Tag as FrmBrowser;

			// Current URL
			if (commandId == (CefMenuCommand)26501)
			{
				Clipboard.SetText(browser.MainFrame.Url);

				return true;
			}


			// Zoom In
			if (commandId == (CefMenuCommand)26521)
			{
				browser.GetZoomLevelAsync().ContinueWith(task =>
				{
					browser.SetZoomLevel(task.Result + 0.5);
				});

				return true;
			}

			// Zoom Out
			if (commandId == (CefMenuCommand)26522)
			{
				browser.GetZoomLevelAsync().ContinueWith(task =>
				{
					browser.SetZoomLevel(task.Result - 0.5);
				});

				return true;
			}

			// Zoom Reset
			if (commandId == (CefMenuCommand)26523)
			{
				browser.SetZoomLevel(0);
				return true;
			}

			// Toggle fullscreen mode
			if (commandId == (CefMenuCommand)26531)
			{
				parentForm.BeginInvoke(new Action(() =>
				{
					parentForm.ToggleFullscreen();
				}));

				return true;
			}

			// Close
			if (commandId == (CefMenuCommand)26541)
			{
				parentForm.BeginInvoke(new Action(() =>
				{
					parentForm.Close();
				}));

				return true;
			}

			return false;
		}

		public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
		{ }

		public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
		{
			return false;
		}
	}
}
