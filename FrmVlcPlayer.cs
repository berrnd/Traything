using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System;
using System.IO;
using System.Windows.Forms;

namespace Traything
{
    public partial class FrmVlcPlayer : BaseTrayForm
    {
        public FrmVlcPlayer()
        {
            InitializeComponent();
        }

        private LibVLC VlcLib;
        private VideoView VlcVideoView;

        private void SetupVlc()
        {
            Core.Initialize(Path.Combine(Program.BaseExecutingPath, "libvlc\\win-x64"));
            this.VlcLib = new LibVLC();
            this.VlcVideoView = new VideoView();
            this.VlcVideoView.MediaPlayer = new MediaPlayer(this.VlcLib);

            this.VlcVideoView.Dock = DockStyle.Fill;
            this.TableLayoutPanelMain.Controls.Add(this.VlcVideoView, 0, 0);
        }

        private void FrmVlcPlayer_Shown(object sender, EventArgs e)
        {
            this.SetupVlc();
        }

        public override void ShowTrayForm(ActionItem item)
        {
            this.VlcVideoView.MediaPlayer.Play(new Media(this.VlcLib, new Uri(item.PathOrUrl)));
            base.ShowTrayForm(item);
        }

        private void FrmVlcPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.VlcVideoView.MediaPlayer.Stop();
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
