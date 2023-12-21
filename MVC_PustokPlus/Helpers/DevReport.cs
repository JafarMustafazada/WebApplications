using System.IO;

namespace MVC_PustokPlus.Helpers;

public static class DevReport
{
	public static void ConsoleLog(string msg)
	{
		string filepath = Path.Combine(Directory.GetCurrentDirectory(), "report.txt");
		if (File.Exists(filepath))
		{
			//writes to file
			System.IO.File.WriteAllText(filepath, msg);
		}
		else
		{
			// Create the file.
			using (FileStream fs = File.Create(filepath))
			{
				System.IO.File.WriteAllText(filepath, msg);
			}
		}
	}
}
