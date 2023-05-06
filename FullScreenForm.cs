using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
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
            MainForm form = new MainForm(true);
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
                IntPtr result = IntPtr.Zero;
                SendMessageTimeout(programIntPtr, 0x52c, IntPtr.Zero, IntPtr.Zero, 0, 0x3e8, result);
                EnumWindows((hwnd, lParam) =>
                {
                    if (FindWindowEx(hwnd, IntPtr.Zero, "SHELLDLL_DefView", null) != IntPtr.Zero)
                    {
                        IntPtr tempHwnd = FindWindowEx(IntPtr.Zero, hwnd, "WorkerW", null);
                        ShowWindow(tempHwnd, 0);
                    }
                    return true;
                }, IntPtr.Zero);
            }
            SetParent(Handle, programIntPtr);
            WindowState = FormWindowState.Maximized;
            Show();
            label1.Height = (int)(2.0 / 5 * Height);
            label2.Height = Height - label1.Height;
        }

        private void FullScreenForm_Load(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font.FontFamily, label1.Width / (float)label1.Text.Length, label1.Font.Style, GraphicsUnit.Pixel);
            label2.Text = string.Format("after {0}s", time.ToString().PadLeft(4, '0'));
            label2.Font = new Font(label2.Font.FontFamily, (label2.Width / (float)label2.Text.Length) - 10, label2.Font.Style, GraphicsUnit.Pixel);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (time == 0)
            {
                timer.Stop();
                Environment.Exit(0);
#if !DEBUG
                BSoD();
#endif
            }
            time--;
            SetText2Label(string.Format("after {0}s", time.ToString().PadLeft(4, '0')));
            if (time % 2 == 0)
            {
                BackColor = Color.Black;
                label1.ForeColor = label2.ForeColor = Color.Red;
            }
            else
            {
                BackColor = Color.Red;
                label1.ForeColor = label2.ForeColor = Color.White;
            }
            switch (time)
            {
                case 0:
                    BackColor = Color.Black;
                    label1.ForeColor = label2.ForeColor = Color.Red;
                    SetText2Label("after 0000s");
                    break;
                case 10:
                    backgroundWorker1.RunWorkerAsync();
                    break;
                case 17:
                    new System.Threading.Thread(() => PlaySound(Properties.Resources.Countdown2, Handle));
                    break;
                default:
                    PlaySound(Properties.Resources.Countdown, Handle);
                    break;
            }
        }

        delegate void SetTextCallBack(string text);

        private void SetText2Label(string text)
        {
            if (label2.InvokeRequired)
            {
                SetTextCallBack stcb = new SetTextCallBack(SetText2Label);
                Invoke(stcb, new object[] { text });
            }
            else label2.Text = text;
        }

        protected override void DefWndProc(ref Message m)
        {
            base.DefWndProc(ref m);
            if (m.Msg == 0x3B9)
            {
                MusicPlayer musicPlayer = new MusicPlayer("countdown");
                musicPlayer.Stop();
                musicPlayer.Close();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                bool b = time <= 5;
                //WindowShaker.ShakeCurrentWindows(b);
                WindowShaker.ShakeMouse(b);
            }
        }
    }
}
