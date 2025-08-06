using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using GlobExpressions;

namespace PCShutdown.Classes
{
    internal class Screenshot
    {
        public static string DirPath = Path.Combine(ShutdownApp.Cfg.WorkPath, "screenshots");
        public static string Save() 
        {
            //Create a new bitmap.
            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                           Screen.PrimaryScreen.Bounds.Height);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        Screen.PrimaryScreen.Bounds.Size
                                        );

            // Save the screenshot to the specified path that the user has chosen.
            
            if (!Path.Exists(DirPath))
            {
                Directory.CreateDirectory(DirPath);
            }
            var date = DateTime.Now;

            var path = Path.Combine(DirPath, $"screenshot_{DateTime.Now:ddMMyyyy_HHmmss-fff}.png");
            
            bmpScreenshot.Save(path, ImageFormat.Png);
            return path;
            
        }
        
        public static IEnumerable<string> FilesList() 
        {
            if (Directory.Exists(DirPath))
            {
                var files = Glob.Files(DirPath, "*.png");

                return files;
            }
            else 
            {
                return Enumerable.Empty<string>();
            }

           
        }
        public static void DeleteFiles() {
            var workpath = ShutdownApp.Cfg.WorkPath;
            Directory.Delete(DirPath, true);
            Directory.CreateDirectory(DirPath);
        }
    }
}






























