using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;


namespace MonLaunchC
{
    class Program
    {
        static long LaunchTimeout = 5000;

        // Win32 function to move window
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        static void Main(string[] args)
        {
            // Check that we have arguments
            if (args.Length >= 2)
            {
                // Check that monitor number is valid
                int MonNum;
                bool ResParse = int.TryParse(args[0], out MonNum);
                if (((MonNum) > Screen.AllScreens.Length) || ((MonNum) < 1) || (ResParse != true))
                {
                    Console.WriteLine("No monitor with that ID. First monitor = 1. Last monitor = " + Screen.AllScreens.Length.ToString() + ".");
                }
                else
                {
                    // Valid number, get bounding rectangle of monitor
                    Rectangle ScrWA = Screen.AllScreens[MonNum - 1].WorkingArea;

                    // Define and start process
                    Process myProcess = new Process();
                    IntPtr hdlWindow;
                    myProcess.StartInfo.FileName = args[1];
                    if (args.Length > 2)
                    {
                        myProcess.StartInfo.Arguments = String.Join(" ", args.Skip(2));
                    }
                    myProcess.Start();

                    // Wait for process to have a window handle - upt to timeout
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    do { Thread.Sleep(10); } while ((sw.ElapsedMilliseconds < LaunchTimeout) & (myProcess.MainWindowHandle == new IntPtr(0)));
                    sw.Stop();

                    // Get window handle
                    hdlWindow = myProcess.MainWindowHandle;

                    // Move window
                    MoveWindow(hdlWindow, ScrWA.Left, ScrWA.Top, ScrWA.Width, ScrWA.Height, true);
                }
            }
            else
            {
                // No or incorrect number of arguments - provide help
                Console.WriteLine("Usage: monlaunch.exe <monitor ID to run on> <command to run>");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Monitors: " + Screen.AllScreens.Length.ToString());
                Console.WriteLine("------------------------------------");
                int count = 0;
                foreach (Screen scr in Screen.AllScreens)
                {
                    count += 1;
                    Console.WriteLine("Monitor " + count.ToString() + ": " + scr.DeviceName);
                }
            }
        }
    }
}
