using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Traything.Data;

namespace Traything.UI
{
	public partial class FrmLauncher : BaseTrayForm
	{
		public FrmLauncher(FrmMain parent)
		{
			InitializeComponent();
			this.Parent = parent;
			this.Show();
		}

		public override void ShowTrayForm(ActionItem item, List<ActionItem> inplaceActions)
		{
			while (!this.Ready)
			{
				Application.DoEvents();
			}

			// Override default tray form style
			item.StayOpen = true;

			base.ShowTrayForm(item, inplaceActions);

			// Override default tray form style
			this.FormBorderStyle = FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;

			foreach (ActionItem actionItem in inplaceActions)
			{
				Button button = new Button();
				button.Text = actionItem.Name;
				button.Height = 24;
				button.Font = new Font(button.Font.FontFamily, 16, FontStyle.Bold);
				button.AutoSize = true;
				button.Width = this.FlowLayoutPanelActionButtons.Width - 6;
				button.Tag = actionItem;
				button.Click += this.ActionItemButton_Click;

				this.FlowLayoutPanelActionButtons.Controls.Add(button);
			}
		}

		private void ActionItemButton_Click(object sender, System.EventArgs e)
		{
			this.Parent.ExecuteAction((ActionItem)((Button)sender).Tag);
		}
	}
}
