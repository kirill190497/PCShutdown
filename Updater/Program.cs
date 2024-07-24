using System.Diagnostics;
using GlobExpressions;
using System;



namespace Updater
{
    static class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
           
           
            string dir;
            if (args.Length == 0 )
            {
                dir = Directory.GetCurrentDirectory();
            }
            else
            {
               dir  = args[0];
               
            }


            
            var zip_file = "PCShutdown.zip";
            List<string> no_move = new List<string>
            {
                zip_file, "Updater.exe", "old"
            };
            
            if (File.Exists(Path.Combine(dir, zip_file)))
            {

            
            
                var processes = Process.GetProcessesByName("PCShutdown");
                foreach (var app in processes)
                {
                    app.Kill();
                }
            


            // {major}.{minor}.{patch}

           

            

            
                var files = Glob.Files(dir, "*");
                var directories = Glob.Directories(dir, "*");

                if (!Path.Exists(Path.Combine(dir, "old")))
                {
                    Directory.CreateDirectory(Path.Combine(dir, "old"));
                }

                foreach (var item in files)
                {
                    if (!no_move.Contains(item))
                        File.Move(Path.Combine(dir, item), Path.Combine(dir, "old", item),true);
                }

                foreach (var item in directories)
                {
                    if (!no_move.Contains(item))
                        Directory.Move(Path.Combine(dir, item), Path.Combine(dir, "old", item));
                }

                System.IO.Compression.ZipFile.ExtractToDirectory(Path.Combine(dir, zip_file), dir);

                

                File.Delete(zip_file);
                Directory.Delete(Path.Combine(dir, "old"), true);
                _ = Process.Start(Path.Combine(dir, "PCShutdown.exe"));
            }
            else
            {
                Environment.Exit(0);
            }

            
        }
        
    }
}
