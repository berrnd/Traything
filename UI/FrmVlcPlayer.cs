using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System;
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
		public FrmVlcPlayer()
		{
			InitializeComponent();
			this.Show();
		}

		private LibVLC VlcLib;
		private VideoView VlcVideoView;
		private Media VlcMedia;
		private TransparentPanel VlcPlayerOverlayPanel;
		private DateTime PlaybackStartTime;
		private bool FirstPlaybackStart = true;

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

			this.VlcVideoView.Dock = DockStyle.Fill;
			this.PanelVlcPlayerContainer.Controls.Add(this.VlcVideoView);

			this.VlcPlayerOverlayPanel = new TransparentPanel();
			this.VlcPlayerOverlayPanel.Dock = DockStyle.Fill;
			this.VlcPlayerOverlayPanel.ContextMenuStrip = this.ContextMenuStripVlcPlayerOverlayPanel;
			this.PanelVlcPlayerContainer.Controls.Add(this.VlcPlayerOverlayPanel);
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
			if (this.FirstPlaybackStart)
			{
				this.FirstPlaybackStart = false;
				this.VlcVideoView.MediaPlayer.Mute = this.ActionItem.StartMuted;
			}

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

		private void FrmVlcPlayer_Shown(object sender, EventArgs e)
		{
			this.SetupVlc();
		}

		public override void ShowTrayForm(ActionItem item)
		{
			while (!this.Ready)
			{
				Application.DoEvents();
			}

			this.FirstPlaybackStart = true;
			this.LoadMediaAndPlay(item.PathOrUrl);
			base.ShowTrayForm(item);
			this.UpdateTitle();

			if (item.StartFullscreen)
			{
				this.ToggleFullscreen();
			}

			if (item.StartMinimized)
			{
				this.WindowState = FormWindowState.Minimized;
			}
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

				this.ContextMenuStripPlaylist.Items.Clear();
				foreach (Media item in this.VlcMedia.SubItems)
				{
					this.ContextMenuStripPlaylist.Items.Add(new ToolStripMenuItem
					{
						Text = item.Meta(MetadataType.Title),
						Tag = item.Mrl
					});
				}

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

			if (this.VlcMedia.SubItems.Count > 0)
			{
				this.BeginInvoke(new Action(() =>
				{
					foreach (ToolStripMenuItem item in this.ContextMenuStripPlaylist.Items)
					{
						if ((string)item.Tag == media.Mrl)
						{
							item.Font = new Font(item.Font, FontStyle.Bold);
						}
						else
						{
							item.Font = new Font(item.Font, FontStyle.Regular);
						}
					}
				}));
			}
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
		}

		private void ButtonPlayPause_Click(object sender, EventArgs e)
		{
			this.VlcVideoView.MediaPlayer.Pause();
		}

		private void TimerUpdatePlayProgress_Tick(object sender, EventArgs e)
		{
			int currentSeconds = Convert.ToInt32(this.VlcVideoView.MediaPlayer.Time / 1000);
			int totalSeconds = Convert.ToInt32(this.VlcVideoView.MediaPlayer.Length / 1000);
			if (currentSeconds < totalSeconds)
			{
				this.TrackBarPlayProgress.Maximum = totalSeconds;
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
				MessageBox.Show(text, $"VLC error: {title}", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return Task.CompletedTask;
		}

		private void ButtonPlaylistNext_Click(object sender, EventArgs e)
		{
			Media nextItem = this.VlcMedia.SubItems.SkipWhile(x => x.Mrl != this.VlcVideoView.MediaPlayer.Media.Mrl).Skip(1).FirstOrDefault();
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
			this.VlcVideoView.MediaPlayer.Mute = !this.VlcVideoView.MediaPlayer.Mute;
		}

		private void ToolStripMenuItemClose_Click(object sender, EventArgs e)
		{
			this.Close();
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
