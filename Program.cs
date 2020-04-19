using System;
using System.Linq;
using System.IO;

namespace SteamShortcuts
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("steam library path: ");
            string input = Console.ReadLine();

            if (input.Last().ToString() == "/")
                input = input.Remove(input.Length - 1);

            input += "/steamapps/";

            var gameIDs = Directory.EnumerateFiles(input).Where(x => x.Contains(".acf"));
            string appID = "";
            foreach (var id in gameIDs)
            {
                string gameName;
                using (StreamReader r = new StreamReader(id))
                {
                    string json = r.ReadToEnd();
                    string[] s = json.Split('"');

                    appID = s[5];
                    gameName = s[13];

                }

                gameName = gameName.Replace(':', '-');

                using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\Steam Games\\" + gameName + ".url"))
                {
                    writer.WriteLine("[InternetShortcut]");
                    writer.WriteLine("URL=steam://rungameid/" + appID);
                }
            }
        }

        public class Game
        {
            public string name = "";
        }
    }
}
