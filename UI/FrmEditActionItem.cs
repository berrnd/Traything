using System;
using System.Windows.Forms;
using Traything.Data;

namespace Traything.UI
{
	public partial class FrmEditActionItem : Form
	{
		public FrmEditActionItem(ActionItem item)
		{
			InitializeComponent();

			this.EditItem = item;
		}

		public ActionItem EditItem { get; private set; }

		private void FrmEditActionItem_Load(object sender, EventArgs e)
		{
			this.PropertyGridActionItem.SelectedObject = this.EditItem;
		}
	}
}
