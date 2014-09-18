using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MonBrowse
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);

        private const int ATTACH_PARENT_PROCESS = -1;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AttachConsole(ATTACH_PARENT_PROCESS);

             Rectangle ScrWA;
         string SiteURL;

            // Check that we have arguments
            if (args.Length == 2)
            {
                // Check that monitor number is valid
                int MonNum;
                bool ResParse = int.TryParse(args[0], out MonNum);
                if (((MonNum) > Screen.AllScreens.Length) || ((MonNum) < 1) || (ResParse != true))
                {
                    Console.WriteLine("No monitor with that ID. First monitor = 1. Last monitor = " + Screen.AllScreens.Length.ToString() + ".");
                    Application.Exit();
                }
                else
                {
                    // Valid number, get bounding rectangle of monitor
                    ScrWA = Screen.AllScreens[MonNum - 1].WorkingArea;
                    SiteURL = args[1];

                    // Load form
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new frmMain(ScrWA,SiteURL));
                }
            }
            else
            {
                // No or incorrect number of arguments - provide help
                Console.WriteLine("Usage: monbrowse.exe <monitor ID to run on> <URL to load>");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Monitors: " + Screen.AllScreens.Length.ToString());
                Console.WriteLine("------------------------------------");
                int count = 0;
                foreach (Screen scr in Screen.AllScreens)
                {
                    count += 1;
                    Console.WriteLine("Monitor " + count.ToString() + ": " + scr.DeviceName);
                }
                Application.Exit();
            }





        }
    }
}
