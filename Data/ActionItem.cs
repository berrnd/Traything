using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Traything.Data
{
	public class ActionItem
	{
		[Category("Common"), Display(Order = 10)]
		[Description("The name of the menu item")]
		public string Name { get; set; }

		[Category("Common"), Display(Order = 20)]
		[Description("The type of this action")]
		public ActionType Type { get; set; }

		[Category("Common"), Display(Order = 30)]
		[Description("Whether this action will be shown on all or only this computer")]
		public ActionScope Scope { get; set; }

		[Category("Common"), Display(Order = 40)]
		[Description("When Scope = ThisComputer, one or multiple hostnames of the corresponding machine (case-insensitiv)")]
		public BindingList<string> Hostnames { get; private set; } = new BindingList<string>();

		[Category("Common"), Display(Order = 50)]
		[Description("Don't show an error message when the action fails")]
		public bool IgnoreErrors { get; set; } = false;

		[Category("StartApplication"), Display(Order = 100)]
		[Description("When Type = StartApplication, the commandline to be executed")]
		public string Commandline { get; set; }

		[Category("HttpRequest"), Display(Order = 200)]
		[Description("When Type = HttpGetRequest or HttpPostRequest, the URL used for the web request")]
		public string Url { get; set; }

		[Category("HttpRequest"), Display(Order = 210)]
		[Description("When Type = HttpPostRequest, the POST data to be sent")]
		public string PostData { get; set; }

		[Category("HttpRequest"), Display(Order = 230)]
		[Description("When Type = HttpGetRequest or HttpPostRequest, headers to be used")]
		public BindingList<string> Headers { get; private set; } = new BindingList<string>();

		[Category("TrayWindows"), Display(Order = 210)]
		[Description("When Type = ShowTrayBrowser or ShowTrayMediaPlayer, a local path or URL to be opened")]
		public string PathOrUrl { get; set; }

		[Category("TrayWindows"), Display(Order = 310)]
		[Description("When Type = ShowTrayBrowser or ShowTrayMediaPlayer, the width of the window")]
		public int Width { get; set; } = 770;

		[Category("TrayWindows"), Display(Order = 320)]
		[Description("When Type = ShowTrayBrowser or ShowTrayMediaPlayer, the height of the window")]
		public int Height { get; set; } = 500;

		[Category("TrayWindows"), Display(Order = 330)]
		[Description("When Type = ShowTrayBrowser or ShowTrayMediaPlayer, whether to keep the window open when it loses focus")]
		public bool StayOpen { get; set; } = false;

		[Category("TrayMediaPlayer"), Display(Order = 400)]
		[Description("When Type = ShowTrayMediaPlayer, whether to mute audio on start")]
		public bool StartMuted { get; set; } = false;

		[Category("TrayMediaPlayer"), Display(Order = 410)]
		[Description("When Type = ShowTrayMediaPlayer, whether to start in fullscreen mode")]
		public bool StartFullscreen { get; set; } = false;

		[Category("TrayMediaPlayer"), Display(Order = 420)]
		[Description("When Type = ShowTrayMediaPlayer, whether to start minimized (e.g. for audio only playback)")]
		public bool StartMinimized { get; set; } = false;

		public override string ToString()
		{
			return $"{this.Name} [{this.Type}] [{this.Scope}]";
		}
	}

	public enum ActionType
	{
		StartApplication,
		HttpGetRequest,
		HttpPostRequest,
		ShowTrayBrowser,
		ShowTrayMediaPlayer,

		CloseTraything,
		Separator,
		Headline
	}

	public enum ActionScope
	{
		Global,
		ThisComputer
	}
}
