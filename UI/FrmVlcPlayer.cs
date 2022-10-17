using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Traything.Data;

namespace Traything.UI
{
	public partial class FrmVlcPlayer : BaseTrayForm
	{
		public FrmVlcPlayer()
		{
			InitializeComponent();
		}

		private LibVLC VlcLib;
		private VideoView VlcVideoView;
		private TransparentPanel VlcPlayerOverlayPanel;

		private void SetupVlc()
		{
			Core.Initialize(Path.Combine(Program.BaseExecutingPath, "libvlc\\win-x64"));
			this.VlcLib = new LibVLC();
			this.VlcLib.SetDialogHandlers(VlcDlgError, VlcDlgLogin, VlcDlgQuestion, VlcDlgDisplayProgress, VlcDlgUpdateProgress);
			this.VlcVideoView = new VideoView();
			this.VlcVideoView.MediaPlayer = new MediaPlayer(this.VlcLib);
			this.VlcVideoView.MediaPlayer.Paused += MediaPlayer_Paused;
			this.VlcVideoView.MediaPlayer.Playing += MediaPlayer_Playing;

			this.VlcVideoView.Dock = DockStyle.Fill;
			this.TableLayoutPanelMain.Controls.Add(this.VlcVideoView, 0, 0);

			this.VlcPlayerOverlayPanel = new TransparentPanel();
			this.VlcPlayerOverlayPanel.Dock = DockStyle.Fill;
			this.Controls.Add(this.VlcPlayerOverlayPanel);
			this.VlcPlayerOverlayPanel.BringToFront();
			this.VlcPlayerOverlayPanel.DoubleClick += VlcPlayerOverlayPanel_DoubleClick;
		}

		private void VlcPlayerOverlayPanel_DoubleClick(object sender, EventArgs e)
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

		private void MediaPlayer_Playing(object sender, EventArgs e)
		{
			this.Invoke(new Action(() =>
			{
				this.ButtonPlayPause.Text = "Pause";
				this.ProgressBarBusy.Visible = false;
				this.LabelPlayTime.Visible = true;
				this.TrackBarPlayProgress.Visible = true;
			}));
		}

		private void MediaPlayer_Paused(object sender, EventArgs e)
		{
			this.Invoke(new Action(() =>
			{
				this.ButtonPlayPause.Text = "Resume";
			}));
		}

		private void FrmVlcPlayer_Shown(object sender, EventArgs e)
		{
			this.SetupVlc();
		}

		public override void ShowTrayForm(ActionItem item)
		{
			this.ProgressBarBusy.Visible = true;
			this.LabelPlayTime.Visible = false;
			this.LabelPlayTime.Text = "";
			this.TrackBarPlayProgress.Visible = false;
			this.ButtonPlayPause.Enabled = false;
			this.VlcVideoView.MediaPlayer.Play(new Media(this.VlcLib, new Uri(item.PathOrUrl)));
			this.TimerUpdatePlayProgress.Start();
			base.ShowTrayForm(item);
		}

		private void FrmVlcPlayer_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.VlcVideoView.MediaPlayer.Stop();
			this.TimerUpdatePlayProgress.Stop();
		}

		private void ButtonPlayPause_Click(object sender, EventArgs e)
		{
			this.VlcVideoView.MediaPlayer.Pause();
		}

		private void TimerUpdatePlayProgress_Tick(object sender, EventArgs e)
		{
			this.LabelPlayTime.Text = $"{TimeSpan.FromMilliseconds(this.VlcVideoView.MediaPlayer.Time):hh\\:mm\\:ss} / {TimeSpan.FromMilliseconds(this.VlcVideoView.MediaPlayer.Length):hh\\:mm\\:ss}";

			int currentSeconds = Convert.ToInt32(this.VlcVideoView.MediaPlayer.Time / 1000);
			int totalSeconds = Convert.ToInt32(this.VlcVideoView.MediaPlayer.Length / 1000);
			if (currentSeconds < totalSeconds)
			{
				this.TrackBarPlayProgress.Maximum = totalSeconds;
				this.TrackBarPlayProgress.Value = currentSeconds;
				this.TrackBarPlayProgress.Enabled = true;
			}
			else
			{
				// Some streams report invalid total length values
				this.TrackBarPlayProgress.Enabled = false;
			}

			this.ButtonPlayPause.Enabled = this.VlcVideoView.MediaPlayer.CanPause;
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
			throw new NotImplementedException();
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
}
