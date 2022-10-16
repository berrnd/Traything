using System;
using System.Drawing;
using System.Windows.Forms;

namespace Traything
{
	public partial class BaseTrayForm : Form
	{
		public BaseTrayForm()
		{
			InitializeComponent();
		}

		protected ActionItem ActionItem;

		private void SetDefaultTrayLocationLocation()
		{
			if (this.ActionItem != null)
			{
				this.Width = this.ActionItem.Width;
				this.Height = this.ActionItem.Height;

				this.ShowInTaskbar = this.ActionItem.StayOpen;
			}

			this.TopMost = true;
			this.Location = this.GetDefaultTrayLocation();
		}

		private Point GetDefaultTrayLocation()
		{
			int x, y;
			if (TaskbarHelper.Position == TaskbarPosition.Left)
			{
				x = Screen.PrimaryScreen.Bounds.Left;
				y = Screen.PrimaryScreen.Bounds.Bottom;

				x += TaskbarHelper.DisplayBounds.Width;
				y -= this.Height;
			}
			else if (TaskbarHelper.Position == TaskbarPosition.Right)
			{
				x = Screen.PrimaryScreen.Bounds.Right;
				y = Screen.PrimaryScreen.Bounds.Bottom;

				x += TaskbarHelper.DisplayBounds.Width;
				y -= this.Height;
			}
			else if (TaskbarHelper.Position == TaskbarPosition.Top)
			{
				x = Screen.PrimaryScreen.Bounds.Right;
				y = Screen.PrimaryScreen.Bounds.Top;

				x -= this.Width;
				y -= this.Height + TaskbarHelper.DisplayBounds.Height;
			}
			else // Bottom (Windows default)
			{
				x = Screen.PrimaryScreen.Bounds.Right;
				y = Screen.PrimaryScreen.Bounds.Bottom;

				x -= this.Width;
				y -= this.Height + TaskbarHelper.DisplayBounds.Height;
			}

			return new Point(x, y);
		}

		private void BaseTrayForm_Shown(object sender, EventArgs e)
		{
			this.Hide();
		}

		public virtual void ShowTrayForm(ActionItem item)
		{
			this.ActionItem = item;
			this.Text = item.Name;

			if (item.StayOpen)
			{
				this.FormBorderStyle = FormBorderStyle.Sizable;
			}
			else
			{
				this.FormBorderStyle = FormBorderStyle.None;
			}

			this.SetDefaultTrayLocationLocation();
			this.Show();
			this.Activate();
		}

		private void BaseTrayForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			this.WindowState = FormWindowState.Normal; // Needs to happen here / only has an effect while the window is visible
			this.Hide();
		}

		private void BaseTrayForm_Deactivate(object sender, EventArgs e)
		{
			if (this.ActionItem != null && !this.ActionItem.StayOpen)
			{
				this.Close();
			}
		}

		public void ReallyClose()
		{
			this.FormClosing -= this.BaseTrayForm_FormClosing;
			this.Close();
		}

		private void BaseTrayForm_LocationChanged(object sender, EventArgs e)
		{
			this.OnLocationOrSizeChanged();
		}

		private void BaseTrayForm_ResizeEnd(object sender, EventArgs e)
		{
			this.OnLocationOrSizeChanged();
		}

		private void OnLocationOrSizeChanged()
		{
			if (this.ActionItem == null)
			{
				return;
			}

			if (this.Size.Height != this.ActionItem.Height || this.Size.Width != this.ActionItem.Width || this.Location != this.GetDefaultTrayLocation())
			{
				// NOT default tray location/size
				this.TopMost = false;
			}
			else
			{
				// Default tray location/size
				this.TopMost = true;
			}
		}
	}
}
