namespace Traything.UI
{
	partial class FrmQueryString
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQueryString));
            this.TextBoxString = new System.Windows.Forms.TextBox();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.LabelPrompt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextBoxString
            // 
            this.TextBoxString.Location = new System.Drawing.Point(12, 24);
            this.TextBoxString.Name = "TextBoxString";
            this.TextBoxString.Size = new System.Drawing.Size(320, 20);
            this.TextBoxString.TabIndex = 0;
            this.TextBoxString.TextChanged += new System.EventHandler(this.TextBoxString_TextChanged);
            // 
            // ButtonOk
            // 
            this.ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonOk.Location = new System.Drawing.Point(257, 50);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 1;
            this.ButtonOk.Text = "OK";
            this.ButtonOk.UseVisualStyleBackColor = true;
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(176, 50);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 2;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // LabelPrompt
            // 
            this.LabelPrompt.AutoSize = true;
            this.LabelPrompt.Location = new System.Drawing.Point(9, 9);
            this.LabelPrompt.Name = "LabelPrompt";
            this.LabelPrompt.Size = new System.Drawing.Size(66, 13);
            this.LabelPrompt.TabIndex = 3;
            this.LabelPrompt.Text = "LabelPrompt";
            // 
            // FrmQueryString
            // 
            this.AcceptButton = this.ButtonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(339, 82);
            this.Controls.Add(this.LabelPrompt);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.TextBoxString);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmQueryString";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmQueryString";
            this.Shown += new System.EventHandler(this.FrmQueryString_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TextBoxString;
		private System.Windows.Forms.Button ButtonOk;
		private System.Windows.Forms.Button ButtonCancel;
		private System.Windows.Forms.Label LabelPrompt;
	}
}
