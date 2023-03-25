using CefSharp;
using CefSharp.WinForms;
using System;
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
				cefSettings.UserDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Traything");
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

			this.Browser.Load(item.PathOrUrl);
			base.ShowTrayForm(item);
		}

		private void FrmBrowser_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.Browser.GetZoomLevelAsync().ContinueWith(task =>
			{
				if (task.Result != this.ActionItem.BrowserZoomLevel)
				{
					this.ActionItem.BrowserZoomLevel = task.Result;
					this.Parent.SaveSettings();
				}
			});

			this.Browser.Load("about:blank");
		}
	}

	public class BrowserContextMenuHandler : IContextMenuHandler
	{
		public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
		{
			model.Clear();

			model.AddItem((CefMenuCommand)26501, "Zoom In");
			model.AddItem((CefMenuCommand)26502, "Zoom Out");
			model.AddItem((CefMenuCommand)26503, "Zoom Reset");
		}

		public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
		{
			// Zoom In
			if (commandId == (CefMenuCommand)26501)
			{
				browser.GetZoomLevelAsync().ContinueWith(task =>
				{
					browser.SetZoomLevel(task.Result + 0.5);
				});

				return true;
			}

			// Zoom Out
			if (commandId == (CefMenuCommand)26502)
			{
				browser.GetZoomLevelAsync().ContinueWith(task =>
				{
					browser.SetZoomLevel(task.Result - 0.5);
				});

				return true;
			}

			// Zoom Reset
			if (commandId == (CefMenuCommand)26503)
			{
				browser.SetZoomLevel(0);
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
