namespace Traything.UI
{
	partial class FrmLauncher
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
            this.FlowLayoutPanelActionButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // FlowLayoutPanelActionButtons
            // 
            this.FlowLayoutPanelActionButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowLayoutPanelActionButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FlowLayoutPanelActionButtons.Location = new System.Drawing.Point(0, 0);
            this.FlowLayoutPanelActionButtons.Name = "FlowLayoutPanelActionButtons";
            this.FlowLayoutPanelActionButtons.Size = new System.Drawing.Size(800, 450);
            this.FlowLayoutPanelActionButtons.TabIndex = 0;
            // 
            // FrmLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.FlowLayoutPanelActionButtons);
            this.Name = "FrmLauncher";
            this.Text = "FrmLauncher";
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanelActionButtons;
	}
}