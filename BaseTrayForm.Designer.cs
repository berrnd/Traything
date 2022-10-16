namespace Traything
{
    partial class BaseTrayForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseTrayForm));
            this.SuspendLayout();
            // 
            // BaseTrayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BaseTrayForm";
            this.Text = "BaseTrayForm";
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.BaseTrayForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BaseTrayForm_FormClosing);
            this.Shown += new System.EventHandler(this.BaseTrayForm_Shown);
            this.ResizeEnd += new System.EventHandler(this.BaseTrayForm_ResizeEnd);
            this.LocationChanged += new System.EventHandler(this.BaseTrayForm_LocationChanged);
            this.ResumeLayout(false);

        }

        #endregion
    }
}