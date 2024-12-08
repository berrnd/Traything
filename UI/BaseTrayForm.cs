using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Traything.Data;

namespace Traything.UI
{
	public partial class BaseTrayForm : Form
	{
		// Visual Studio designer requires a parameterless constructor
		public BaseTrayForm()
		{
			InitializeComponent();
		}

		public BaseTrayForm(FrmMain parent)
		{
			InitializeComponent();
			this.Parent = parent;
			this.Show();
		}

		internal new FrmMain Parent;
		protected ActionItem ActionItem;
		protected bool Ready = false;
		private const int EXTRA_NEGATIVE_PADDING = 7; // Returned Taskbar/Form sizes/bounds include margins (or something like that), make it overall a little tighter

		private void SetDefaultTrayLocation()
		{
			if (this.ActionItem != null)
			{
				int width = this.ActionItem.Width, height = this.ActionItem.Height;

				if (width == -1)
				{
					width = Screen.PrimaryScreen.Bounds.Width + EXTRA_NEGATIVE_PADDING;

					if (TaskbarHelper.Position == TaskbarPosition.Left || TaskbarHelper.Position == TaskbarPosition.Right)
					{
						width -= TaskbarHelper.CurrentBounds.Width - EXTRA_NEGATIVE_PADDING;
					}
					else // Bottom or Top
					{
						width += EXTRA_NEGATIVE_PADDING;
					}
				}

				if (height == -1)
				{
					height = Screen.PrimaryScreen.Bounds.Height + EXTRA_NEGATIVE_PADDING;

					if (TaskbarHelper.Position == TaskbarPosition.Bottom || TaskbarHelper.Position == TaskbarPosition.Top)
					{
						height -= TaskbarHelper.CurrentBounds.Height;
					}
				}

				this.Width = width;
				this.Height = height;

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

				x += TaskbarHelper.CurrentBounds.Width - EXTRA_NEGATIVE_PADDING;
				y -= this.Height - EXTRA_NEGATIVE_PADDING;
			}
			else if (TaskbarHelper.Position == TaskbarPosition.Right)
			{
				x = Screen.PrimaryScreen.Bounds.Right;
				y = Screen.PrimaryScreen.Bounds.Bottom;

				x -= this.Width + TaskbarHelper.CurrentBounds.Width - EXTRA_NEGATIVE_PADDING;
				y -= this.Height - EXTRA_NEGATIVE_PADDING;
			}
			else if (TaskbarHelper.Position == TaskbarPosition.Top)
			{
				x = Screen.PrimaryScreen.Bounds.Right;
				y = Screen.PrimaryScreen.Bounds.Top;

				x -= this.Width - EXTRA_NEGATIVE_PADDING;
				y += TaskbarHelper.CurrentBounds.Height;
			}
			else // Bottom (Windows default)
			{
				x = Screen.PrimaryScreen.Bounds.Right;
				y = Screen.PrimaryScreen.Bounds.Bottom;

				x -= this.Width - EXTRA_NEGATIVE_PADDING;
				y -= this.Height + TaskbarHelper.CurrentBounds.Height - EXTRA_NEGATIVE_PADDING;
			}

			return new Point(x, y);
		}

		private void BaseTrayForm_Shown(object sender, EventArgs e)
		{
			this.Hide();
			this.Ready = true;
		}

		public virtual void ShowTrayForm(ActionItem item, List<ActionItem> inplaceActions)
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

			this.SetDefaultTrayLocation();
			this.Show();
			this.Activate();

			if (this.ActionItem.Width == -1 && this.ActionItem.Height == -1)
			{
				this.WindowState = FormWindowState.Maximized; // Needs to happen here / only has an effect while the window is visible
			}
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
