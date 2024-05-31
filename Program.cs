using System;
using System.Windows.Forms;
using Bomb.Classes;

namespace Bomb
{
    internal static class Program
    {
        /// <summary>
        /// Main entrance of the program。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var time = 0;
            bool? full_screen = null, time_given = false;
            foreach (var arg in args)
            {
                if (string.IsNullOrEmpty(arg)) continue;
                if (arg.ToLower() == "-f" || arg.ToLower() == "--fullscreen") 
                    if (full_screen.Value || full_screen == null) full_screen = true;
                    else
                    {
                        MessageBox.Show("Bomb: Upexpected arguments. Fullscreen toggle cannot be used with normal toggle. See --help", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                else if ((arg.ToLower() == "-n" || arg.ToLower() == "--normal") && full_screen == null)
                    if (!full_screen.Value || full_screen == null)
                        full_screen = false;
                    else
                    {
                        MessageBox.Show("Bomb: Upexpected arguments. Fullscreen toggle cannot be used with normal toggle. See --help", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                else if (arg.ToLower() == "-h" || arg.ToLower() == "--help")
                {
                    MessageBox.Show(
                        "Usage:\n" +
                        "Bomb.exe [-f | --fullscreen | -n | --normal] [-h | --help] [--BSoD] [time]\n" +
                        "[-f | --fullscreen | -n | --normal]:\nTo decide whether the program starts in fullscreen mode or normal mode. Default: -n\n" +
                        "[-h | --help]" +
                        "[--BSoD]:\nTest BSoD Function by doing it.\n" +
                        "[time]:\nDecide the countdown time of the bomb. Default: Unset (Won't auto start)", "Help"
                        );
                    return;
                }
                else if (arg.ToLower() == "--bsod") Functions.BSoD();
                else if (int.TryParse(arg, out time)) time_given = true;
                else
                {
                    MessageBox.Show(
                    "Bomb:  Unexpected argument(s). See --help.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (full_screen ?? false)
                if (time_given ?? false) Application.Run(new FullScreenForm(time));
                else Application.Run(new FullScreenForm());
            else
                if (time_given ?? false) Application.Run(new MainForm(time));
            else Application.Run(new MainForm());
        }
    }
}
