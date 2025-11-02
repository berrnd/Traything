namespace Traything.UI
{
    partial class FrmVlcPlayer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.TableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
			this.PanelVlcPlayerContainer = new System.Windows.Forms.Panel();
			this.FlowLayoutPanelPlayerControls = new System.Windows.Forms.FlowLayoutPanel();
			this.ButtonPlayPause = new System.Windows.Forms.Button();
			this.ButtonPlaylistNext = new System.Windows.Forms.Button();
			this.ContextMenuStripPlaylist = new Traything.UI.CustomContextMenuStrip(this.components);
			this.ProgressBarBusy = new System.Windows.Forms.ProgressBar();
			this.TrackBarPlayProgress = new System.Windows.Forms.TrackBar();
			this.LabelPlayTime = new System.Windows.Forms.Label();
			this.TimerUpdatePlayProgress = new System.Windows.Forms.Timer(this.components);
			this.ContextMenuStripVlcPlayerOverlayPanel = new Traything.UI.CustomContextMenuStrip(this.components);
			this.ToolStripMenuItemToggleFullscreen = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemToggleMute = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripSeparatorInplaceActions = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripSeparatorClose = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
			this.TableLayoutPanelMain.SuspendLayout();
			this.FlowLayoutPanelPlayerControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TrackBarPlayProgress)).BeginInit();
			this.ContextMenuStripVlcPlayerOverlayPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// TableLayoutPanelMain
			// 
			this.TableLayoutPanelMain.ColumnCount = 1;
			this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TableLayoutPanelMain.Controls.Add(this.PanelVlcPlayerContainer, 0, 0);
			this.TableLayoutPanelMain.Controls.Add(this.FlowLayoutPanelPlayerControls, 0, 1);
			this.TableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
			this.TableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
			this.TableLayoutPanelMain.Name = "TableLayoutPanelMain";
			this.TableLayoutPanelMain.RowCount = 2;
			this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TableLayoutPanelMain.Size = new System.Drawing.Size(784, 461);
			this.TableLayoutPanelMain.TabIndex = 0;
			// 
			// PanelVlcPlayerContainer
			// 
			this.PanelVlcPlayerContainer.BackColor = System.Drawing.Color.Black;
			this.PanelVlcPlayerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelVlcPlayerContainer.Location = new System.Drawing.Point(0, 0);
			this.PanelVlcPlayerContainer.Margin = new System.Windows.Forms.Padding(0);
			this.PanelVlcPlayerContainer.Name = "PanelVlcPlayerContainer";
			this.PanelVlcPlayerContainer.Size = new System.Drawing.Size(784, 425);
			this.PanelVlcPlayerContainer.TabIndex = 1;
			// 
			// FlowLayoutPanelPlayerControls
			// 
			this.FlowLayoutPanelPlayerControls.Controls.Add(this.ButtonPlayPause);
			this.FlowLayoutPanelPlayerControls.Controls.Add(this.ButtonPlaylistNext);
			this.FlowLayoutPanelPlayerControls.Controls.Add(this.ProgressBarBusy);
			this.FlowLayoutPanelPlayerControls.Controls.Add(this.TrackBarPlayProgress);
			this.FlowLayoutPanelPlayerControls.Controls.Add(this.LabelPlayTime);
			this.FlowLayoutPanelPlayerControls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FlowLayoutPanelPlayerControls.Location = new System.Drawing.Point(3, 428);
			this.FlowLayoutPanelPlayerControls.Name = "FlowLayoutPanelPlayerControls";
			this.FlowLayoutPanelPlayerControls.Size = new System.Drawing.Size(778, 30);
			this.FlowLayoutPanelPlayerControls.TabIndex = 0;
			// 
			// ButtonPlayPause
			// 
			this.ButtonPlayPause.Enabled = false;
			this.ButtonPlayPause.Location = new System.Drawing.Point(3, 3);
			this.ButtonPlayPause.Name = "ButtonPlayPause";
			this.ButtonPlayPause.Size = new System.Drawing.Size(75, 23);
			this.ButtonPlayPause.TabIndex = 0;
			this.ButtonPlayPause.Text = "Play";
			this.ButtonPlayPause.UseVisualStyleBackColor = true;
			this.ButtonPlayPause.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonPlayPause_MouseDown);
			// 
			// ButtonPlaylistNext
			// 
			this.ButtonPlaylistNext.ContextMenuStrip = this.ContextMenuStripPlaylist;
			this.ButtonPlaylistNext.Location = new System.Drawing.Point(84, 3);
			this.ButtonPlaylistNext.Name = "ButtonPlaylistNext";
			this.ButtonPlaylistNext.Size = new System.Drawing.Size(75, 23);
			this.ButtonPlaylistNext.TabIndex = 4;
			this.ButtonPlaylistNext.Text = "Next";
			this.ButtonPlaylistNext.UseVisualStyleBackColor = true;
			this.ButtonPlaylistNext.Visible = false;
			this.ButtonPlaylistNext.Click += new System.EventHandler(this.ButtonPlaylistNext_Click);
			// 
			// ContextMenuStripPlaylist
			// 
			this.ContextMenuStripPlaylist.Name = "ContextMenuStripPlaylist";
			this.ContextMenuStripPlaylist.ShowImageMargin = false;
			this.ContextMenuStripPlaylist.ShowItemToolTips = false;
			this.ContextMenuStripPlaylist.Size = new System.Drawing.Size(36, 4);
			this.ContextMenuStripPlaylist.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripPlaylist_Opening);
			this.ContextMenuStripPlaylist.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ContextMenuStripPlaylist_ItemClicked);
			// 
			// ProgressBarBusy
			// 
			this.ProgressBarBusy.Location = new System.Drawing.Point(165, 3);
			this.ProgressBarBusy.Name = "ProgressBarBusy";
			this.ProgressBarBusy.Size = new System.Drawing.Size(100, 23);
			this.ProgressBarBusy.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.ProgressBarBusy.TabIndex = 1;
			// 
			// TrackBarPlayProgress
			// 
			this.TrackBarPlayProgress.AutoSize = false;
			this.TrackBarPlayProgress.Location = new System.Drawing.Point(271, 3);
			this.TrackBarPlayProgress.Maximum = 100;
			this.TrackBarPlayProgress.Name = "TrackBarPlayProgress";
			this.TrackBarPlayProgress.Size = new System.Drawing.Size(180, 23);
			this.TrackBarPlayProgress.TabIndex = 3;
			this.TrackBarPlayProgress.Visible = false;
			this.TrackBarPlayProgress.Scroll += new System.EventHandler(this.TrackBarPlayProgress_Scroll);
			// 
			// LabelPlayTime
			// 
			this.LabelPlayTime.AutoSize = true;
			this.LabelPlayTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LabelPlayTime.Location = new System.Drawing.Point(457, 0);
			this.LabelPlayTime.Name = "LabelPlayTime";
			this.LabelPlayTime.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.LabelPlayTime.Size = new System.Drawing.Size(99, 21);
			this.LabelPlayTime.TabIndex = 2;
			this.LabelPlayTime.Text = "LabelPlayTime";
			this.LabelPlayTime.Visible = false;
			// 
			// TimerUpdatePlayProgress
			// 
			this.TimerUpdatePlayProgress.Interval = 500;
			this.TimerUpdatePlayProgress.Tick += new System.EventHandler(this.TimerUpdatePlayProgress_Tick);
			// 
			// ContextMenuStripVlcPlayerOverlayPanel
			// 
			this.ContextMenuStripVlcPlayerOverlayPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemToggleFullscreen,
            this.ToolStripMenuItemToggleMute,
            this.ToolStripSeparatorInplaceActions,
            this.ToolStripSeparatorClose,
            this.ToolStripMenuItemClose});
			this.ContextMenuStripVlcPlayerOverlayPanel.Name = "ContextMenuStripVlcOverlayPanel";
			this.ContextMenuStripVlcPlayerOverlayPanel.ShowImageMargin = false;
			this.ContextMenuStripVlcPlayerOverlayPanel.ShowItemToolTips = false;
			this.ContextMenuStripVlcPlayerOverlayPanel.Size = new System.Drawing.Size(174, 82);
			// 
			// ToolStripMenuItemToggleFullscreen
			// 
			this.ToolStripMenuItemToggleFullscreen.Name = "ToolStripMenuItemToggleFullscreen";
			this.ToolStripMenuItemToggleFullscreen.Size = new System.Drawing.Size(173, 22);
			this.ToolStripMenuItemToggleFullscreen.Text = "Toggle fullscreen mode";
			this.ToolStripMenuItemToggleFullscreen.Click += new System.EventHandler(this.ToolStripMenuItemToggleFullscreen_Click);
			// 
			// ToolStripMenuItemToggleMute
			// 
			this.ToolStripMenuItemToggleMute.Name = "ToolStripMenuItemToggleMute";
			this.ToolStripMenuItemToggleMute.Size = new System.Drawing.Size(173, 22);
			this.ToolStripMenuItemToggleMute.Text = "Toggle mute";
			this.ToolStripMenuItemToggleMute.Click += new System.EventHandler(this.ToolStripMenuItemToggleMute_Click);
			// 
			// ToolStripSeparatorInplaceActions
			// 
			this.ToolStripSeparatorInplaceActions.Name = "ToolStripSeparatorInplaceActions";
			this.ToolStripSeparatorInplaceActions.Size = new System.Drawing.Size(170, 6);
			this.ToolStripSeparatorInplaceActions.Visible = false;
			// 
			// ToolStripSeparatorClose
			// 
			this.ToolStripSeparatorClose.Name = "ToolStripSeparatorClose";
			this.ToolStripSeparatorClose.Size = new System.Drawing.Size(170, 6);
			// 
			// ToolStripMenuItemClose
			// 
			this.ToolStripMenuItemClose.Name = "ToolStripMenuItemClose";
			this.ToolStripMenuItemClose.Size = new System.Drawing.Size(173, 22);
			this.ToolStripMenuItemClose.Text = "Close";
			this.ToolStripMenuItemClose.Click += new System.EventHandler(this.ToolStripMenuItemClose_Click);
			// 
			// FrmVlcPlayer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 461);
			this.Controls.Add(this.TableLayoutPanelMain);
			this.Name = "FrmVlcPlayer";
			this.Text = "FrmVlcPlayer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmVlcPlayer_FormClosing);
			this.Shown += new System.EventHandler(this.FrmVlcPlayer_Shown);
			this.TableLayoutPanelMain.ResumeLayout(false);
			this.FlowLayoutPanelPlayerControls.ResumeLayout(false);
			this.FlowLayoutPanelPlayerControls.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.TrackBarPlayProgress)).EndInit();
			this.ContextMenuStripVlcPlayerOverlayPanel.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TableLayoutPanelMain;
        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanelPlayerControls;
        private System.Windows.Forms.Button ButtonPlayPause;
        private System.Windows.Forms.ProgressBar ProgressBarBusy;
        private System.Windows.Forms.Label LabelPlayTime;
        private System.Windows.Forms.Timer TimerUpdatePlayProgress;
        private System.Windows.Forms.TrackBar TrackBarPlayProgress;
        private System.Windows.Forms.Panel PanelVlcPlayerContainer;
        private System.Windows.Forms.Button ButtonPlaylistNext;
        private Traything.UI.CustomContextMenuStrip ContextMenuStripPlaylist;
        private Traything.UI.CustomContextMenuStrip ContextMenuStripVlcPlayerOverlayPanel;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemToggleFullscreen;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemToggleMute;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparatorClose;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemClose;
		private System.Windows.Forms.ToolStripSeparator ToolStripSeparatorInplaceActions;
	}
}
