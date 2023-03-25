using System;
using System.Windows.Forms;
using Traything.Data;

namespace Traything.UI
{
	public partial class FrmQueryString : Form
	{
		public FrmQueryString(ActionItem item)
		{
			this.ActionItem = item;
			InitializeComponent();
		}

		private ActionItem ActionItem;
		public string QueryString { get; set; }

		private void FrmQueryString_Shown(object sender, EventArgs e)
		{
			this.Text = "Enter placeholder for " + this.ActionItem.Name;
			this.TextBoxString.Focus();
		}

		private void TextBoxString_TextChanged(object sender, EventArgs e)
		{
			this.QueryString = this.TextBoxString.Text;
		}
	}
}
