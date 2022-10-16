using CefSharp;
using CefSharp.WinForms;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Traything
{
    public partial class FrmBrowser : Form
    {
        public FrmBrowser()
        {
            InitializeComponent();
        }

        private ChromiumWebBrowser Browser;
        private ActionItem ActionItem;

        private void SetupCef()
        {
            if (!Cef.IsInitialized)
            {
                Cef.EnableHighDPISupport();

                CefSettings cefSettings = new CefSettings();
                cefSettings.BrowserSubprocessPath = Path.Combine(Program.BaseExecutingPath, @"CefSharp.BrowserSubprocess.exe");
                cefSettings.CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Traything");
                cefSettings.UserDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Traything");
                cefSettings.LogSeverity = LogSeverity.Disable;
                cefSettings.CefCommandLineArgs.Add("--enable-media-stream", "");
                cefSettings.CefCommandLineArgs.Add("--lang", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

                Cef.Initialize(cefSettings, performDependencyCheck: false, browserProcessHandler: null);
            }
            
            this.Browser = new ChromiumWebBrowser("about:blank");
            this.Browser.Dock = DockStyle.Fill;
            this.Controls.Add(this.Browser);
        }

        private void SetLocation()
		{
            if (this.ActionItem != null)
            {
                this.Width = this.ActionItem.Width;
                this.Height = this.ActionItem.Height;
            }

            int x = 0, y = 0;
			if (TaskbarHelper.Position == TaskbarPosition.Left)
			{
                x = Screen.PrimaryScreen.Bounds.Left;
                y = Screen.PrimaryScreen.Bounds.Bottom;

                x += TaskbarHelper.DisplayBounds.Width;
                y -= this.Height;
            }
            else if (TaskbarHelper.Position == TaskbarPosition.Right)
            {
                x = Screen.PrimaryScreen.Bounds.Right;
                y = Screen.PrimaryScreen.Bounds.Bottom;

                x += TaskbarHelper.DisplayBounds.Width;
                y -= this.Height;
            }
            else if (TaskbarHelper.Position == TaskbarPosition.Top)
            {
                x = Screen.PrimaryScreen.Bounds.Right;
                y = Screen.PrimaryScreen.Bounds.Top;

                x -= this.Width;
                y -= this.Height + TaskbarHelper.DisplayBounds.Height;
            }
            else // Bottom (Windows default)
			{
				x = Screen.PrimaryScreen.Bounds.Right;
				y = Screen.PrimaryScreen.Bounds.Bottom;

				x -= this.Width;
				y -= this.Height + TaskbarHelper.DisplayBounds.Height;
			}

			this.Location = new Point(x, y);
        }

        private void FrmBrowser_Shown(object sender, EventArgs e)
        {
            this.Hide();
            this.SetupCef();
        }

        public void ShowTrayBrowser(ActionItem item)
        {
            this.ActionItem = item;
            this.Text = item.Name;

            if (item.StayOpen)
            {
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
            }
            
            this.Browser.Load(item.PathOrUrl);
            this.SetLocation();
            this.Show();
            this.Activate();
        }

        private void FrmBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.Browser.Load("about:blank");
        }

        private void FrmBrowser_Deactivate(object sender, EventArgs e)
        {
            if (this.ActionItem != null && !this.ActionItem.StayOpen)
            {
                this.Close();
            }
        }

        public void ReallyClose()
        {
            this.FormClosing -= this.FrmBrowser_FormClosing;
            this.Close();
        }
    }
}
