using System;
using System.Windows.Forms;
using Traything.Data;

namespace Traything.UI
{
	public partial class FrmQueryString : Form
	{
		public FrmQueryString(ActionItem item, string prompt)
		{
			this.ActionItem = item;
			this.Prompt = prompt;
			InitializeComponent();
		}

		private ActionItem ActionItem;
		private string Prompt;
		public string QueryString { get; set; }

		private void FrmQueryString_Shown(object sender, EventArgs e)
		{
			this.Text = this.ActionItem.Name;
			this.LabelPrompt.Text = this.Prompt;
			this.TextBoxString.Focus();
		}

		private void TextBoxString_TextChanged(object sender, EventArgs e)
		{
			this.QueryString = this.TextBoxString.Text;
		}
	}
}
