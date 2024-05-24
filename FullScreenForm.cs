using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Bomb.Classes;
using static Bomb.MainForm;

namespace Bomb
{
    public partial class FullScreenForm : Form
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string winName);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageTimeout(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, uint fuFlage, uint timeout, IntPtr result);

        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc proc, IntPtr lParam);
        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string className, string winName);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hwnd, IntPtr parentHwnd);

        private IntPtr programIntPtr = IntPtr.Zero;

        int time = 0;

        public FullScreenForm()
        {
            ShowMainForm();
            InitializeComponent();
            init();
        }

        public FullScreenForm(int time)
        {
            //ShowMainForm();
            InitializeComponent();
            init();
            this.time = time;
        }

        private void ShowMainForm()
        {
            var form = new MainForm(true);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Cancel)
            {
                Environment.Exit(0);
                return;
            }
            time = form.time;
        }

        private void init()
        {
            programIntPtr = FindWindow("Progman", null);
            if (programIntPtr != IntPtr.Zero)
            {
                var result = IntPtr.Zero;
                SendMessageTimeout(programIntPtr, 0x52c, IntPtr.Zero, IntPtr.Zero, 0, 0x3e8, result);
                EnumWindows((hwnd, lParam) =>
                {
                    if (FindWindowEx(hwnd, IntPtr.Zero, "SHELLDLL_DefView", null) != IntPtr.Zero)
                    {
                        var tempHwnd = FindWindowEx(IntPtr.Zero, hwnd, "WorkerW", null);
                        ShowWindow(tempHwnd, 0);
                    }
                    return true;
                }, IntPtr.Zero);
            }
            SetParent(Handle, programIntPtr);
            WindowState = FormWindowState.Maximized;
            Show();
            explode_lbl.Height = (int)(2.0 / 5 * Height);
            countdown_lbl.Height = Height - explode_lbl.Height;
        }

        private void FullScreenForm_Load(object sender, EventArgs e)
        {
            explode_lbl.Font = new Font(explode_lbl.Font.FontFamily, explode_lbl.Width / (float)explode_lbl.Text.Length, explode_lbl.Font.Style, GraphicsUnit.Pixel);
            countdown_lbl.Text = string.Format("after {0}s", time.ToString().PadLeft(4, '0'));
            countdown_lbl.Font = new Font(countdown_lbl.Font.FontFamily, (countdown_lbl.Width / (float)countdown_lbl.Text.Length) - 10, countdown_lbl.Font.Style, GraphicsUnit.Pixel);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (time == 0)
            {
                timer.Stop();
#if !DEBUG
                Functions.BSoD();
#endif
                Environment.Exit(0);
            }
            time--;
            SetText2Label(string.Format("after {0}s", time.ToString().PadLeft(4, '0')));
            if (time % 2 == 0)
            {
                BackColor = Color.Black;
                explode_lbl.ForeColor = countdown_lbl.ForeColor = Color.Red;
            }
            else
            {
                BackColor = Color.Red;
                explode_lbl.ForeColor = countdown_lbl.ForeColor = Color.White;
            }
            switch (time)
            {
                case 0:
                    BackColor = Color.Black;
                    explode_lbl.ForeColor = countdown_lbl.ForeColor = Color.Red;
                    SetText2Label("after 0000s");
                    break;
                case 10:
                    shaker.RunWorkerAsync();
                    break;
                case 17:
                    InvokeAction(() => SoundPlayer.PlaySound(Properties.Resources.Countdown2, Handle), this);
                    break;
                default:
                    if (time >= 17)
                        InvokeAction(() => SoundPlayer.PlaySound(Properties.Resources.Countdown, Handle), this);
                    break;
            }
        }

        delegate void SetTextCallBack(string text);

        private void SetText2Label(string text)
        {
            if (countdown_lbl.InvokeRequired)
            {
                var stcb = new SetTextCallBack(SetText2Label);
                Invoke(stcb, new object[] { text });
            }
            else countdown_lbl.Text = text;
        }

        protected override void DefWndProc(ref Message m)
        {
            base.DefWndProc(ref m);
            if (m.Msg == 0x3B9)
            {
                var musicPlayer = new SoundPlayer("countdown");
                musicPlayer.Stop();
                musicPlayer.Close();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                var b = time <= 5;
                //WindowShaker.ShakeCurrentWindows(b);
                WindowShaker.ShakeMouse(b);
            }
        }
    }
}
