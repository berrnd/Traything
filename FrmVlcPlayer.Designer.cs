namespace Traything
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
            this.TableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.FlowLayoutPanelPlayerControls = new System.Windows.Forms.FlowLayoutPanel();
            this.ButtonPlay = new System.Windows.Forms.Button();
            this.ButtonPause = new System.Windows.Forms.Button();
            this.ButtonStop = new System.Windows.Forms.Button();
            this.TableLayoutPanelMain.SuspendLayout();
            this.FlowLayoutPanelPlayerControls.SuspendLayout();
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
            this.FlowLayoutPanelPlayerControls.Controls.Add(this.ButtonPlay);
            this.FlowLayoutPanelPlayerControls.Controls.Add(this.ButtonPause);
            this.FlowLayoutPanelPlayerControls.Controls.Add(this.ButtonStop);
            this.FlowLayoutPanelPlayerControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowLayoutPanelPlayerControls.Location = new System.Drawing.Point(3, 428);
            this.FlowLayoutPanelPlayerControls.Name = "FlowLayoutPanelPlayerControls";
            this.FlowLayoutPanelPlayerControls.Size = new System.Drawing.Size(778, 30);
            this.FlowLayoutPanelPlayerControls.TabIndex = 0;
            // 
            // ButtonPlay
            // 
            this.ButtonPlay.Location = new System.Drawing.Point(3, 3);
            this.ButtonPlay.Name = "ButtonPlay";
            this.ButtonPlay.Size = new System.Drawing.Size(75, 23);
            this.ButtonPlay.TabIndex = 0;
            this.ButtonPlay.Text = "Play";
            this.ButtonPlay.UseVisualStyleBackColor = true;
            this.ButtonPlay.Click += new System.EventHandler(this.ButtonPlay_Click);
            // 
            // ButtonPause
            // 
            this.ButtonPause.Location = new System.Drawing.Point(84, 3);
            this.ButtonPause.Name = "ButtonPause";
            this.ButtonPause.Size = new System.Drawing.Size(75, 23);
            this.ButtonPause.TabIndex = 1;
            this.ButtonPause.Text = "Pause";
            this.ButtonPause.UseVisualStyleBackColor = true;
            this.ButtonPause.Click += new System.EventHandler(this.ButtonPause_Click);
            // 
            // ButtonStop
            // 
            this.ButtonStop.Location = new System.Drawing.Point(165, 3);
            this.ButtonStop.Name = "ButtonStop";
            this.ButtonStop.Size = new System.Drawing.Size(75, 23);
            this.ButtonStop.TabIndex = 2;
            this.ButtonStop.Text = "Stop";
            this.ButtonStop.UseVisualStyleBackColor = true;
            this.ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // FrmVlcPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.TableLayoutPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmVlcPlayer";
            this.ShowInTaskbar = false;
            this.Text = "FrmVlcPlayer";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.FrmVlcPlayer_Activated);
            this.Deactivate += new System.EventHandler(this.FrmVlcPlayer_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmVlcPlayer_FormClosing);
            this.Shown += new System.EventHandler(this.FrmVlcPlayer_Shown);
            this.TableLayoutPanelMain.ResumeLayout(false);
            this.FlowLayoutPanelPlayerControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TableLayoutPanelMain;
        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanelPlayerControls;
        private System.Windows.Forms.Button ButtonPlay;
        private System.Windows.Forms.Button ButtonPause;
        private System.Windows.Forms.Button ButtonStop;
    }
}