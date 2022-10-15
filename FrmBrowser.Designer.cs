namespace Traything
{
    partial class FrmBrowser
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
            this.SuspendLayout();
            // 
            // FrmBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmBrowser";
            this.ShowInTaskbar = false;
            this.Text = "FrmBrowser";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.FrmBrowser_Activated);
            this.Deactivate += new System.EventHandler(this.FrmBrowser_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBrowser_FormClosing);
            this.Shown += new System.EventHandler(this.FrmBrowser_Shown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}