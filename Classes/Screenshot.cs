using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PCShutdown.Classes
{
    internal class Screenshot
    {
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
            var workpath = Properties.Settings.Default.WorkPath;
            var path = Path.Combine(workpath, "screenshot.png");
            
            bmpScreenshot.Save(path, ImageFormat.Png);
            return path;
            
        }
        
        public static void DeleteFile() {
            var workpath = Properties.Settings.Default.WorkPath;
            var path = Path.Combine(workpath, "screenshot.png");
            File.Delete(path);
        }
    }
}






























