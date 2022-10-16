using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Traything
{
    public partial class FrmVlcPlayer : Form
    {
        public FrmVlcPlayer()
        {
            InitializeComponent();
        }

        private LibVLC VlcLib;
        private VideoView VlcVideoView;
        private ActionItem ActionItem;

        private void SetupVlc()
        {
            Core.Initialize(Path.Combine(Program.BaseExecutingPath, "libvlc\\win-x64"));
            this.VlcLib = new LibVLC();
            this.VlcVideoView = new VideoView();
            this.VlcVideoView.MediaPlayer = new MediaPlayer(this.VlcLib);

            this.VlcVideoView.Dock = DockStyle.Fill;
            this.TableLayoutPanelMain.Controls.Add(this.VlcVideoView, 0, 0);
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

        private void FrmVlcPlayer_Shown(object sender, EventArgs e)
        {
            this.Hide();
            this.SetupVlc();
        }

        public void ShowTrayPlayer(ActionItem item)
        {
            this.ActionItem = item;
            this.Text = item.Name;

            if (item.StayOpen)
            {
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
            }

            this.VlcVideoView.MediaPlayer.Play(new Media(this.VlcLib, new Uri(item.PathOrUrl)));
            this.SetLocation();
            this.Show();
            this.Activate();
        }

        private void FrmVlcPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            this.VlcVideoView.MediaPlayer.Stop();
        }

        private void FrmVlcPlayer_Deactivate(object sender, EventArgs e)
        {
            if (this.ActionItem != null && !this.ActionItem.StayOpen)
            {
                this.Close();
            }
        }

        public void ReallyClose()
        {
            this.FormClosing -= this.FrmVlcPlayer_FormClosing;
            this.Close();
        }

        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            this.VlcVideoView.MediaPlayer.Play();
        }

        private void ButtonPause_Click(object sender, EventArgs e)
        {
            this.VlcVideoView.MediaPlayer.Pause();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            this.VlcVideoView.MediaPlayer.Stop();
        }
    }
}
