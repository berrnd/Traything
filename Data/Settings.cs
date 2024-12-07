using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Traything.Data
{
	public class Settings
	{
		internal static readonly string _Path = Path.Combine(Program.BaseExecutingPath, "Traything.xml");
		public List<ActionItem> Actions { get; set; } = new List<ActionItem>();
		public string Version { get; set; } = "1.3.0"; // Version flag was introduced after v1.3.0, so assuming that version initially

		public static Settings Load()
		{
			Settings settings = new Settings();

			if (File.Exists(Settings._Path))
			{
				using (StreamReader reader = new StreamReader(Settings._Path))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(Settings));
					settings = (Settings)serializer.Deserialize(reader);
					reader.Close();
				}

				if (new Version(settings.Version).CompareTo(new Version(Program.RunningVersion)) != 0)
				{
					settings.Save(); // Migrations (may) have been applied => save the file immediately
				}
			}
			else
			{
				settings = Settings.Example();
			}

			return settings;
		}

		public void Save()
		{
			this.Version = Program.RunningVersion;

			XmlSerializer serializer = new XmlSerializer(typeof(Settings));
			using (TextWriter textWriter = new StreamWriter(Settings._Path))
			{
				serializer.Serialize(textWriter, this);
				textWriter.Close();
			}
		}

		public static Settings Example()
		{
			Settings s = new Settings();
			s.Actions.Add(new ActionItem { Name = "Example/demo configuration", Type = ActionType.Headline });
			s.Actions.Add(new ActionItem { Name = "Open Notepad", Type = ActionType.StartApplication, Commandline = "notepad.exe" });
			s.Actions.Add(new ActionItem { Name = "Open Calculator", Type = ActionType.StartApplication, Commandline = "calc.exe" });
			s.Actions.Add(new ActionItem { Name = "----Separator1----", Type = ActionType.Separator });
			s.Actions.Add(new ActionItem { Name = "HTTP GET Request", Type = ActionType.HttpGetRequest, Url = "https://traything-demo.berrnd.xyz/httprequest" });
			ActionItem item = new ActionItem { Name = "HTTP POST Request", Type = ActionType.HttpPostRequest, Url = "https://traything-demo.berrnd.xyz/httprequest", PostData = "{ \"name\": \"Test1\" }" };
			item.Headers.Add("Content-Type: application/json");
			s.Actions.Add(item);
			s.Actions.Add(new ActionItem { Name = "----Separator2----", Type = ActionType.Separator });
			s.Actions.Add(new ActionItem { Name = "TrayBrowser (stay open)", Type = ActionType.ShowTrayBrowser, PathOrUrl = "https://traything-demo.berrnd.xyz", StayOpen = true });
			s.Actions.Add(new ActionItem { Name = "TrayBrowser (auto hide when focus lost)", Type = ActionType.ShowTrayBrowser, PathOrUrl = "https://traything-demo.berrnd.xyz", StayOpen = false });
			s.Actions.Add(new ActionItem { Name = "----Separator3----", Type = ActionType.Separator });
			s.Actions.Add(new ActionItem { Name = "TrayMediaPlayer (stay open)", Type = ActionType.ShowTrayMediaPlayer, PathOrUrl = "https://traything-demo.berrnd.xyz/stream", StayOpen = true });
			s.Actions.Add(new ActionItem { Name = "TrayMediaPlayer (auto hide when focus lost)", Type = ActionType.ShowTrayMediaPlayer, PathOrUrl = "https://traything-demo.berrnd.xyz/stream", StayOpen = false });
			s.Actions.Add(new ActionItem { Name = "----Separator4----", Type = ActionType.Separator });
			s.Actions.Add(new ActionItem { Name = "Close", Type = ActionType.CloseTraything });
			return s;
		}
	}
}
