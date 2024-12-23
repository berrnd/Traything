namespace Traything.UI
{
	partial class FrmAbout
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
            this.LabelHeadline = new System.Windows.Forms.Label();
            this.LabelFooter = new System.Windows.Forms.Label();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.LabelSayThanksQuestions = new System.Windows.Forms.Label();
            this.LinkLabelSayThanks = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // LabelHeadline
            // 
            this.LabelHeadline.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelHeadline.Location = new System.Drawing.Point(12, 9);
            this.LabelHeadline.Name = "LabelHeadline";
            this.LabelHeadline.Size = new System.Drawing.Size(242, 43);
            this.LabelHeadline.TabIndex = 0;
            this.LabelHeadline.Text = "Traything";
            this.LabelHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelFooter
            // 
            this.LabelFooter.ForeColor = System.Drawing.SystemColors.GrayText;
            this.LabelFooter.Location = new System.Drawing.Point(13, 141);
            this.LabelFooter.Name = "LabelFooter";
            this.LabelFooter.Size = new System.Drawing.Size(241, 45);
            this.LabelFooter.TabIndex = 1;
            this.LabelFooter.Text = "Traything is a hobby project by Bernd Bestel\r\nCreated with passion since 2022\r\nLi" +
    "fe runs on Code";
            this.LabelFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelVersion
            // 
            this.LabelVersion.Location = new System.Drawing.Point(13, 52);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(241, 23);
            this.LabelVersion.TabIndex = 4;
            this.LabelVersion.Text = "Version xxxx";
            this.LabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelSayThanksQuestions
            // 
            this.LabelSayThanksQuestions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSayThanksQuestions.Location = new System.Drawing.Point(13, 85);
            this.LabelSayThanksQuestions.Name = "LabelSayThanksQuestions";
            this.LabelSayThanksQuestions.Size = new System.Drawing.Size(241, 23);
            this.LabelSayThanksQuestions.TabIndex = 5;
            this.LabelSayThanksQuestions.Text = " Do you find Traything useful?";
            this.LabelSayThanksQuestions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LinkLabelSayThanks
            // 
            this.LinkLabelSayThanks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LinkLabelSayThanks.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.LinkLabelSayThanks.Location = new System.Drawing.Point(13, 108);
            this.LinkLabelSayThanks.Name = "LinkLabelSayThanks";
            this.LinkLabelSayThanks.Size = new System.Drawing.Size(241, 20);
            this.LinkLabelSayThanks.TabIndex = 6;
            this.LinkLabelSayThanks.TabStop = true;
            this.LinkLabelSayThanks.Text = "Say thanks ❤";
            this.LinkLabelSayThanks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LinkLabelSayThanks.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelSayThanks_LinkClicked);
            // 
            // FrmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 195);
            this.Controls.Add(this.LinkLabelSayThanks);
            this.Controls.Add(this.LabelSayThanksQuestions);
            this.Controls.Add(this.LabelVersion);
            this.Controls.Add(this.LabelFooter);
            this.Controls.Add(this.LabelHeadline);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Traything";
            this.Load += new System.EventHandler(this.FrmAbout_Load);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label LabelHeadline;
		private System.Windows.Forms.Label LabelFooter;
		private System.Windows.Forms.Label LabelVersion;
		private System.Windows.Forms.Label LabelSayThanksQuestions;
		private System.Windows.Forms.LinkLabel LinkLabelSayThanks;
	}
}
