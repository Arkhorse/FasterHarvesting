using FasterHarvesting.Utilities;

namespace FasterHarvesting.CustomList
{
	internal class CustomListHandler
	{
		private static List<string> demoTools = new()
		{
			"GEAR_Hatchet",
			"GEAR_HatchetImprovised"
		};

		private static CustomDuration MinimumDuration = new(0, 0, 0, 3, true);

		private static ICustomListEntry demo { get; } = new ICustomListEntry(
			"INTERACTIVE_BranchA_Prefab",
			0.01f,
			1.5f,
			UsableToolsOriginal: demoTools,
			m_CustomDuration: MinimumDuration
		);

		public static void Initilize()
		{
			if (Settings.Instance.InteractiveLog) Logging.Log("Config Initilize");
			//if (!Settings.Instance.AllowCustomList) return;

			try
			{
				if (!Directory.Exists(Main.CustomListFolder))
				{
					Directory.CreateDirectory(Main.CustomListFolder);
				}
			}
			catch
			{
				Logging.LogError("Attempting to create custom list directory failed");
				throw;
			}
			PopulateConfigFiles();
		}

		public static void ReInitilize()
		{
			if (Settings.Instance.InteractiveLog) Logging.Log("Config ReInitilize");
			//if (!Settings.Instance.AllowCustomList) return;

			try
			{
				if (!Directory.Exists(Main.CustomListFolder))
				{
					Directory.CreateDirectory(Main.CustomListFolder);
				}
			}
			catch
			{
				Logging.LogError("Attempting to create custom list directory failed");
				throw;
			}

			Main.ObjectsToAlter.Clear();

			PopulateConfigFiles();
		}

		public static void PopulateConfigFiles()
		{
			if (Settings.Instance.InteractiveLog) Logging.Log("Config Populate");
			try
			{
				string[] files = Directory.GetFiles(Main.CustomListFolder, "*.json");
				Array.Sort(files, StringComparer.InvariantCultureIgnoreCase);

				if (Settings.Instance.InteractiveLog) Logging.LogSeperator();
				foreach (string file in files)
				{
					JsonFile.Load(file);
				}
				

				foreach (ICustomListEntry entry in Main.ObjectsToAlter)
				{
					if (VerifyEntry(entry))
					{
						if (Settings.Instance.InteractiveLog) Logging.Log($"Entry Added: {entry.ObjectName}");
						continue;
					}
					else
					{
						Main.ObjectsToAlter.Remove(entry);
					}
				}
				if (!Settings.Instance.InteractiveLog) Logging.Log($"Configs loaded: {Main.ObjectsToAlter.Count}");
				if (Settings.Instance.InteractiveLog) Logging.LogSeperator();

			}
			catch (FileNotFoundException fnf)
			{
				Logging.LogError($"Custom list file not found. Reason: {fnf.Message} {fnf.StackTrace}");
			}
			catch (Exception e)
			{
				Logging.LogError($"Unknown error prevented the Custom List from being populated. Exception: {e.Message} {e.StackTrace}");
			}
		}

		public static bool VerifyEntry(ICustomListEntry? entry)
		{
			if (entry != null)
			{
				if (Settings.Instance.ADVANCED_listadd)
				{
					Logging.Log($"\tObjectName:                  {entry.ObjectName}");
					Logging.Log($"\tObjectBreakDownTime:         {entry.ObjectBreakDownTime}");
					Logging.Log($"\tObjectBreakDownTimeOriginal: {entry.ObjectBreakDownTimeOriginal}");
				}

				if (entry.ObjectBreakDownTime > 12.00f)
				{
					entry.ObjectBreakDownTime = 12.00f;
					Logging.LogWarning($"Entry: {entry.ObjectName} has a BreakDown Time greater than 12.00f");
				}
				if (entry.ObjectBreakDownTime < 0.01f)
				{
					entry.ObjectBreakDownTime = 0.02f; // 0.02f works for all objects
					Logging.LogWarning($"Entry: {entry.ObjectName} has a BreakDown Time less than 0.01f");
				}

				//GameObject entryObjectRaw = GameObject.Find(entry.ObjectName);

				//if (entryObjectRaw)
				//{
				//    if (Settings.Instance.InteractiveLog) Logging.Log($"entry GameObject has been found, {entry.ObjectName}");
				//    Il2Cpp.BreakDown entryBreakDown = entryObjectRaw.GetComponent<Il2Cpp.BreakDown>();
				//    if (entryBreakDown)
				//    {
				//        if (Settings.Instance.InteractiveLog) Logging.Log("entry has a BreakDown component");
				//        return true;
				//    }
				//}
				//// These objects normally cant be found on game load, need to move this to an OnSceneWasInit method or check for scene name somewhere before this
				//else Logging.LogWarning($"entry GameObject has not been found, {entry.ObjectName}");

				return true;
			}

			return false;
		}

		public static void SaveDemo()
		{
			try
			{
				if (!File.Exists(Main.DemoFile)) 
				{
					JsonFile.Save(demo);
				}
			}
			catch (UnauthorizedAccessException)
			{
				Logging.LogError("Access was denied when attempting to save the demo config file");
				throw;
			}
			catch (DirectoryNotFoundException)
			{
				Logging.LogError("Directory was not found. This catch should never be called, if it is, there is another problem occuring");
				throw;
			}
			catch
			{
				Logging.LogError("Unknown error prevented the demo file from saving");
				throw;
			}
		}

		public static void CreateNewConfig(string ObjectName, float BreakDownTime, float BreakDownTimeOriginal)
		{
			ICustomListEntry entry = new(ObjectName, BreakDownTime, BreakDownTimeOriginal);
			JsonFile.Save(entry);
		}

		public static void CreateNewConfig(string ObjectName, float BreakDownTime, float BreakDownTimeOriginal, List<string> UsableToolsOriginal)
		{
			ICustomListEntry entry = new(ObjectName, BreakDownTime, BreakDownTimeOriginal, UsableToolsOriginal:UsableToolsOriginal);
			JsonFile.Save(entry);
		}
	}
}
