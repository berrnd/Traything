using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Traything
{
	public class Settings
	{
		internal static readonly string _Path = Path.Combine(Program.BaseExecutingPath, "Traything.xml");
		public List<ActionItem> Actions = new List<ActionItem>();

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
			}
			else
			{
				settings = Settings.Example();
			}

			return settings;
		}

		public void Save()
		{
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
			s.Actions.Add(new ActionItem { Name = "Notepad", Type = ActionType.StartApplication, Commandline = "notepad.exe" });
			s.Actions.Add(new ActionItem { Name = "Calculator", Type = ActionType.StartApplication, Commandline = "calc.exe" });
			s.Actions.Add(new ActionItem { Name = "----Separator1----", Type = ActionType.Separator });
			s.Actions.Add(new ActionItem { Name = "HTTP GET request", Type = ActionType.HttpGetRequest, Url = "https://demo.grocy.info/api/system/info" });
			ActionItem item = new ActionItem { Name = "HTTP POST request", Type = ActionType.HttpPostRequest, Url = "https://demo.grocy.info/api/objects/locations", PostData = "{ \"name\": \"Test1\" }" };
			item.Headers.Add("Content-Type: application/json");
			s.Actions.Add(item);
			s.Actions.Add(new ActionItem { Name = "----Separator2----", Type = ActionType.Separator });
			s.Actions.Add(new ActionItem { Name = "Tray browser (stay open)", Type = ActionType.ShowTrayBrowser, PathOrUrl = "https://grocy.info", StayOpen = true });
			s.Actions.Add(new ActionItem { Name = "Tray browser (auto hide when focus lost)", Type = ActionType.ShowTrayBrowser, PathOrUrl = "https://grocy.info", StayOpen = false });
			s.Actions.Add(new ActionItem { Name = "----Separator3----", Type = ActionType.Separator });
			s.Actions.Add(new ActionItem { Name = "Tray stream player (stay open)", Type = ActionType.ShowTrayMediaPlayer, PathOrUrl = "https://mcdn.daserste.de/daserste/de/master.m3u8", StayOpen = true });
			s.Actions.Add(new ActionItem { Name = "Tray stream player (auto hide when focus lost)", Type = ActionType.ShowTrayMediaPlayer, PathOrUrl = "https://mcdn.daserste.de/daserste/de/master.m3u8", StayOpen = false });
			s.Actions.Add(new ActionItem { Name = "----Separator4----", Type = ActionType.Separator });
			s.Actions.Add(new ActionItem { Name = "Close", Type = ActionType.CloseTraything });
			return s;
		}
	}
}
