using GlobExpressions;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
 


namespace Updater
{
    static class Program
    {

        
        static string dir = Directory.GetCurrentDirectory();
        static string zip_file = "PCShutdown.zip";
        static List<string> no_move =
            [
                zip_file,  "old", "config.json" //"Updater.exe",
            ];
        [STAThread]
        static void Main(string[] args)
        {
           
           
            
            if (args.Length > 0 )
            {
                dir = args[0];
                if (args.Contains("--force"))
                {
                    Console.WriteLine("Force download latest version!");

                    if (File.Exists(Path.Combine(dir, zip_file))) {
                        File.Delete(Path.Combine(dir, zip_file));
                    }
                }
            }

            
            if (!File.Exists(Path.Combine(dir, zip_file)))
            {
                var url = @"https://api.github.com/repos/kirill190497/PCShutdown/releases/latest";
                var remote = Request.GetJSON(url);
                long size = Convert.ToInt64(remote["assets"][0]["size"]);
                var file_url = remote["assets"][0]["browser_download_url"].ToString();
                var file_dest = Path.Combine(dir, file_url.Split("/")[^1]);
                Console.WriteLine(file_url);
                Console.WriteLine(file_dest);

                Request.SaveFile(file_url, file_dest);
                Console.WriteLine("Downloading");
                
                /*
                using (var progress = new ProgressBar())
                {
                    
                        progress.Report((double)i / 100);
                        //Thread.Sleep(20);
                        while (true)
                        {
                            if (File.Exists(Path.Combine(dir, zip_file)))
                            {
                                var length = new FileInfo(file_dest).Length;
                            progress.Report((double)length / size);
                            if (size == length) { Console.WriteLine("Done! Begin update"); break; }
                            }
                        }
                    
                }*/

                while (true)
                {
                    if (File.Exists(Path.Combine(dir, zip_file)))
                    {
                        var length = new FileInfo(file_dest).Length;
                        using (var progress = new ProgressBar())
                        {
                            while (length != size)
                            {
                                length = new FileInfo(file_dest).Length;
                                progress.Report((double)length / size);
                            }
                        }
                        Console.WriteLine("Downloading done! Begin update");
                        break; 
                    }
                }


            }
            else
            {
                Console.WriteLine("File exists! Begin update");
            }




            BeginUpdate();

        }

        static void BeginUpdate()
        {
            var processes = Process.GetProcessesByName("PCShutdown");
            foreach (var app in processes)
            {
                app.Kill();
                Console.WriteLine("Stoping process: " + app.ProcessName+  "(" + app.Id + ")" );
            }


            var files = Glob.Files(dir, "*");
            var directories = Glob.Directories(dir, "*");

            if (!Path.Exists(Path.Combine(dir, "old")))
            {
                Directory.CreateDirectory(Path.Combine(dir, "old"));
            }
            else
            {
                Directory.Delete(Path.Combine(dir, "old"), true);
                Directory.CreateDirectory(Path.Combine(dir, "old"));
            }
            Console.WriteLine("Saving old version");
            foreach (var item in files)
            {
                if (!no_move.Contains(item))
                    File.Move(Path.Combine(dir, item), Path.Combine(dir, "old", item), true);
            }

            foreach (var item in directories)
            {
                if (!no_move.Contains(item))
                    Directory.Move(Path.Combine(dir, item), Path.Combine(dir, "old", item));
            }
            Console.WriteLine("Saving old version complete");
            Console.WriteLine("Extractiong new version");
            try
            {
               ZipFile.ExtractToDirectory(Path.Combine(dir, zip_file), dir, true);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }



           
            File.Delete(Path.Combine(dir, zip_file));
            //Directory.Delete(Path.Combine(dir, "old"), true);
            _ = Process.Start(Path.Combine(dir, "PCShutdown.exe"));
            Console.WriteLine("Update finished! Run new version file");
        }
        
    }


}
