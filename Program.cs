using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Traything.UI;

namespace Traything
{
	internal static class Program
	{
		internal static readonly string RunningVersion = Regex.Replace(Assembly.GetExecutingAssembly().GetName().Version.ToString(), @"^(.+?)(\.0+)$", "$1");
		internal static readonly string BaseExecutingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.TrimEnd('\\'));

		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FrmMain());
		}
	}
}
