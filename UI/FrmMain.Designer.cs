namespace Traything.UI
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.NotifyIconTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContextMenuStripTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.ButtonEdit = new System.Windows.Forms.Button();
            this.ButtonRemove = new System.Windows.Forms.Button();
            this.ButtonMoveDown = new System.Windows.Forms.Button();
            this.ButtonMoveUp = new System.Windows.Forms.Button();
            this.ListBoxActions = new System.Windows.Forms.ListBox();
            this.LinkLabelAbout = new System.Windows.Forms.LinkLabel();
            this.ButtonExecute = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NotifyIconTray
            // 
            this.NotifyIconTray.ContextMenuStrip = this.ContextMenuStripTray;
            this.NotifyIconTray.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIconTray.Icon")));
            this.NotifyIconTray.Text = "Traything";
            this.NotifyIconTray.Visible = true;
            this.NotifyIconTray.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIconTray_MouseClick);
            // 
            // ContextMenuStripTray
            // 
            this.ContextMenuStripTray.Name = "contextMenuStrip1";
            this.ContextMenuStripTray.ShowImageMargin = false;
            this.ContextMenuStripTray.ShowItemToolTips = false;
            this.ContextMenuStripTray.Size = new System.Drawing.Size(36, 4);
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.Location = new System.Drawing.Point(12, 12);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(75, 23);
            this.ButtonAdd.TabIndex = 2;
            this.ButtonAdd.Text = "Add";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            this.ButtonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // ButtonEdit
            // 
            this.ButtonEdit.Enabled = false;
            this.ButtonEdit.Location = new System.Drawing.Point(93, 12);
            this.ButtonEdit.Name = "ButtonEdit";
            this.ButtonEdit.Size = new System.Drawing.Size(75, 23);
            this.ButtonEdit.TabIndex = 2;
            this.ButtonEdit.Text = "Edit";
            this.ButtonEdit.UseVisualStyleBackColor = true;
            this.ButtonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // ButtonRemove
            // 
            this.ButtonRemove.Enabled = false;
            this.ButtonRemove.Location = new System.Drawing.Point(174, 12);
            this.ButtonRemove.Name = "ButtonRemove";
            this.ButtonRemove.Size = new System.Drawing.Size(75, 23);
            this.ButtonRemove.TabIndex = 2;
            this.ButtonRemove.Text = "Remove";
            this.ButtonRemove.UseVisualStyleBackColor = true;
            this.ButtonRemove.Click += new System.EventHandler(this.ButtonRemove_Click);
            // 
            // ButtonMoveDown
            // 
            this.ButtonMoveDown.Enabled = false;
            this.ButtonMoveDown.Location = new System.Drawing.Point(377, 12);
            this.ButtonMoveDown.Name = "ButtonMoveDown";
            this.ButtonMoveDown.Size = new System.Drawing.Size(75, 23);
            this.ButtonMoveDown.TabIndex = 2;
            this.ButtonMoveDown.Text = "Move Down";
            this.ButtonMoveDown.UseVisualStyleBackColor = true;
            this.ButtonMoveDown.Click += new System.EventHandler(this.ButtonMoveDown_Click);
            // 
            // ButtonMoveUp
            // 
            this.ButtonMoveUp.Enabled = false;
            this.ButtonMoveUp.Location = new System.Drawing.Point(296, 12);
            this.ButtonMoveUp.Name = "ButtonMoveUp";
            this.ButtonMoveUp.Size = new System.Drawing.Size(75, 23);
            this.ButtonMoveUp.TabIndex = 2;
            this.ButtonMoveUp.Text = "Move Up";
            this.ButtonMoveUp.UseVisualStyleBackColor = true;
            this.ButtonMoveUp.Click += new System.EventHandler(this.ButtonMoveUp_Click);
            // 
            // ListBoxActions
            // 
            this.ListBoxActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListBoxActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListBoxActions.FormattingEnabled = true;
            this.ListBoxActions.ItemHeight = 24;
            this.ListBoxActions.Location = new System.Drawing.Point(12, 41);
            this.ListBoxActions.Name = "ListBoxActions";
            this.ListBoxActions.Size = new System.Drawing.Size(814, 508);
            this.ListBoxActions.TabIndex = 3;
            this.ListBoxActions.SelectedIndexChanged += new System.EventHandler(this.ListBoxActions_SelectedIndexChanged);
            this.ListBoxActions.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxActions_MouseDoubleClick);
            // 
            // LinkLabelAbout
            // 
            this.LinkLabelAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LinkLabelAbout.AutoSize = true;
            this.LinkLabelAbout.Location = new System.Drawing.Point(791, 17);
            this.LinkLabelAbout.Name = "LinkLabelAbout";
            this.LinkLabelAbout.Size = new System.Drawing.Size(35, 13);
            this.LinkLabelAbout.TabIndex = 4;
            this.LinkLabelAbout.TabStop = true;
            this.LinkLabelAbout.Text = "About";
            this.LinkLabelAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelAbout_LinkClicked);
            // 
            // ButtonExecute
            // 
            this.ButtonExecute.Enabled = false;
            this.ButtonExecute.Location = new System.Drawing.Point(499, 12);
            this.ButtonExecute.Name = "ButtonExecute";
            this.ButtonExecute.Size = new System.Drawing.Size(75, 23);
            this.ButtonExecute.TabIndex = 6;
            this.ButtonExecute.Text = "Execute";
            this.ButtonExecute.UseVisualStyleBackColor = true;
            this.ButtonExecute.Click += new System.EventHandler(this.ButtonExecute_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 583);
            this.Controls.Add(this.ButtonExecute);
            this.Controls.Add(this.LinkLabelAbout);
            this.Controls.Add(this.ListBoxActions);
            this.Controls.Add(this.ButtonMoveDown);
            this.Controls.Add(this.ButtonMoveUp);
            this.Controls.Add(this.ButtonRemove);
            this.Controls.Add(this.ButtonEdit);
            this.Controls.Add(this.ButtonAdd);
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Traything";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon NotifyIconTray;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStripTray;
        private System.Windows.Forms.Button ButtonAdd;
        private System.Windows.Forms.Button ButtonEdit;
        private System.Windows.Forms.Button ButtonRemove;
        private System.Windows.Forms.Button ButtonMoveDown;
        private System.Windows.Forms.Button ButtonMoveUp;
        private System.Windows.Forms.ListBox ListBoxActions;
        private System.Windows.Forms.LinkLabel LinkLabelAbout;
        private System.Windows.Forms.Button ButtonExecute;
    }
}

