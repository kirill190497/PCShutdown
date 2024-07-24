using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCShutdown.Classes
{
    public class GUIConsoleWriter
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        private const int ATTACH_PARENT_PROCESS = -1;

        StreamWriter _stdOutWriter;

        // this must be called early in the program
        public GUIConsoleWriter()
        {
            // this needs to happen before attachconsole.
            // If the output is not redirected we still get a valid stream but it doesn't appear to write anywhere
            // I guess it probably does write somewhere, but nowhere I can find out about
            var stdout = Console.OpenStandardOutput();
            _stdOutWriter = new StreamWriter(stdout);
            _stdOutWriter.AutoFlush = true;

            AttachConsole(ATTACH_PARENT_PROCESS);
        }

        public void WriteLine(string line)
        {
            _stdOutWriter.WriteLine(line);
            Console.WriteLine(line);
        }

        public void Dispose()
        {
            _stdOutWriter.Dispose();
            FreeConsole();
        }

    }
}
