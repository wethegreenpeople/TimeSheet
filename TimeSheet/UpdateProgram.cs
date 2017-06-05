using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Net;

namespace TimeSheet
{
    static class UpdateProgram
    {

        public static bool CheckForUpdates(string updateFile)
        {
            using (var client = new WebClient())
            {
                File.Delete("newestversion.ini");
                client.DownloadFile("http://uraqt.xyz/uselessprograms/newestversion.ini", "newestversion.ini");
            }

            bool UpdatesAvailable = false;
            string latestVersion;

            latestVersion = File.ReadLines(updateFile).First();
            string currentVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
            if (latestVersion != currentVersion)
            {
                UpdatesAvailable = true;
                FormUpdate update = new FormUpdate();
                update.Show();
            }

            return UpdatesAvailable;
        }

        public static void RunUpdates()
        {
            
        }
    }
}
