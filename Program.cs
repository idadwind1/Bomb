using System;
using System.Windows.Forms;

namespace Bomb
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            int time = 0;
            bool? full_screen = null, time_given = false;
            foreach (var arg in args)
            {
                if ((arg.ToLower() == "-f" || arg.ToLower() == "--fullscreen") && full_screen == null) full_screen = true;
                else if ((arg.ToLower() == "-n" || arg.ToLower() == "--normal") && full_screen == null) full_screen = false;
                else if (int.TryParse(arg, out time)) time_given = true;
            }
            if (full_screen ?? false)
                if (time_given ?? false) Application.Run(new FullScreenForm(time));
                else Application.Run(new FullScreenForm());
            else
                if (time_given ?? false) Application.Run(new MainForm());
                else Application.Run(new MainForm(time));
            Application.Run(new FullScreenForm());
        }
    }
}
