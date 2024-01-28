namespace Provod
{
    public class ConsoleFileManager
    {
        private static string vozvrat = "";

        static void Main(string[] args)
        {
            while (true)
            {
                ShowDrives();

                if (Arrow.Run().ToLower() == "exit") 
                {
                    break;
                }

                if (int.TryParse(Arrow.Run(), out int selectedIndex))
                {
                    if (selectedIndex > 0 && selectedIndex <= DriveInfo.GetDrives().Length)
                    {
                        DriveInfo drive = DriveInfo.GetDrives()[selectedIndex - 1];
                        vozvrat = drive.Name;
                        ShowDirectoryShtyki(vozvrat);
                        CHTO_tut(vozvrat);
                    }
                }
            }
        }

        private static void CHTO_tut(string path)
        {
            while (true)
            {
                
                if (Arrow.Run().ToLower() == "666")
                {
                    vozvrat = Directory.GetParent(path)?.FullName ?? "";
                    return;
                }

                if (int.TryParse(Arrow.Run(), out int selectedIndex))
                {
                    string[] entries = Directory.GetFileSystemEntries(path);
                    if (selectedIndex > 0 && selectedIndex <= entries.Length)
                    {
                        string selectedEntry = entries[selectedIndex - 1];
                        if (File.Exists(selectedEntry))
                        {
                            try
                            {
                                System.Diagnostics.Process.Start(selectedEntry);
                                Console.ReadKey();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        else if (Directory.Exists(selectedEntry))
                        {
                            vozvrat = selectedEntry;
                            ShowDirectoryShtyki(vozvrat);
                        }
                    }
                }
            }
        }

        private static void ShowDrives()
        {
            Console.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Выбери диск:");
            for (int i = 0; i < drives.Length; i++)
            {
                Console.WriteLine("    "  + $"{i + 1}. {drives[i].Name} - {drives[i].DriveFormat} ({drives[i].AvailableFreeSpace / (1024 * 1024 * 1024)}GB free of {drives[i].TotalSize / (1024 * 1024 * 1024)}GB)");

            }
            Console.WriteLine("--------------------------------");
        }

        private static void ShowDirectoryShtyki(string path)
        {
            Console.WriteLine("--------------------------------");
            Console.Clear();
            Console.WriteLine($"В папке находится вот это {path}:");
            string[] entries = Directory.GetFileSystemEntries(path);

            for (int i = 0; i < entries.Length; i++)
            {
                string entry = entries[i];
                string type = File.Exists(entry) ? "File" : "Directory";
                Console.WriteLine("    "+$"{ i + 1}. {Path.GetFileName(entry)} - {type}");
            }
            Console.WriteLine("--------------------------------");
        }
    }
    class Arrow
    {
        public static string Run()
        {
            bool online = true;

            int position = 2;
            while (online)
            {

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow && position != 1)
                {
                    Console.SetCursorPosition(0, position);
                    Console.Write("    ");
                    position--;
                    Console.SetCursorPosition(0, position);
                    Console.WriteLine("->");
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(0, position);
                    Console.Write("    ");
                    position++;
                    Console.SetCursorPosition(0, position);
                    Console.WriteLine("->");
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.SetCursorPosition(0, position);
                    Console.Write("    ");
                    return Convert.ToString(position);
                    break;
                }
                else if (key.Key == ConsoleKey.Escape)
                {

                    return Convert.ToString(666);
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            return Convert.ToString(position);


        }
    }
}