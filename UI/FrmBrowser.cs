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
		public FrmBrowser()
		{
			InitializeComponent();
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
			this.Controls.Add(this.Browser);
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
			this.Browser.Load("about:blank");
		}
	}
}
