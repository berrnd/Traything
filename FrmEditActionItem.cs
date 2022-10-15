using System;
using System.Windows.Forms;

namespace Traything
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
