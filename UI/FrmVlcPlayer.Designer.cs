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
            this.FlowLayoutPanelPlayerControls = new System.Windows.Forms.FlowLayoutPanel();
            this.ButtonPlayPause = new System.Windows.Forms.Button();
            this.ProgressBarBusy = new System.Windows.Forms.ProgressBar();
            this.TrackBarPlayProgress = new System.Windows.Forms.TrackBar();
            this.LabelPlayTime = new System.Windows.Forms.Label();
            this.TimerUpdatePlayProgress = new System.Windows.Forms.Timer(this.components);
            this.TableLayoutPanelMain.SuspendLayout();
            this.FlowLayoutPanelPlayerControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarPlayProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // TableLayoutPanelMain
            // 
            this.TableLayoutPanelMain.ColumnCount = 1;
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelMain.Controls.Add(this.FlowLayoutPanelPlayerControls, 0, 1);
            this.TableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanelMain.Name = "TableLayoutPanelMain";
            this.TableLayoutPanelMain.RowCount = 2;
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanelMain.Size = new System.Drawing.Size(784, 461);
            this.TableLayoutPanelMain.TabIndex = 0;
            // 
            // FlowLayoutPanelPlayerControls
            // 
            this.FlowLayoutPanelPlayerControls.Controls.Add(this.ButtonPlayPause);
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
            this.ButtonPlayPause.Click += new System.EventHandler(this.ButtonPlayPause_Click);
            // 
            // ProgressBarBusy
            // 
            this.ProgressBarBusy.Location = new System.Drawing.Point(84, 3);
            this.ProgressBarBusy.Name = "ProgressBarBusy";
            this.ProgressBarBusy.Size = new System.Drawing.Size(100, 23);
            this.ProgressBarBusy.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.ProgressBarBusy.TabIndex = 1;
            // 
            // TrackBarPlayProgress
            // 
            this.TrackBarPlayProgress.AutoSize = false;
            this.TrackBarPlayProgress.Location = new System.Drawing.Point(190, 3);
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
            this.LabelPlayTime.Location = new System.Drawing.Point(376, 0);
            this.LabelPlayTime.Name = "LabelPlayTime";
            this.LabelPlayTime.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.LabelPlayTime.Size = new System.Drawing.Size(99, 21);
            this.LabelPlayTime.TabIndex = 2;
            this.LabelPlayTime.Text = "LabelPlayTime";
            this.LabelPlayTime.Visible = false;
            // 
            // TimerUpdatePlayProgress
            // 
            this.TimerUpdatePlayProgress.Interval = 1000;
            this.TimerUpdatePlayProgress.Tick += new System.EventHandler(this.TimerUpdatePlayProgress_Tick);
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
    }
}
