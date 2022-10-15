using System.Diagnostics;
using System.Windows.Forms;

namespace Traything
{
	public partial class FrmAbout : Form
	{
		public FrmAbout()
		{
			InitializeComponent();
		}

		private void FrmAbout_Load(object sender, System.EventArgs e)
		{
			this.LabelVersion.Text = this.LabelVersion.Text.Replace("xxxx", Program.RunningVersion);
		}

		private void LinkLabelSayThanks_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://berrnd.de/say-thanks?project=Traything&version=" + Program.RunningVersion);
		}
	}
}
