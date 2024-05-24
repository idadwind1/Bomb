using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Bomb.Classes;

namespace Bomb
{
    public partial class MainForm : Form
    {
        public int time = 20;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(int time)
        {
            InitializeComponent();
            this.time = time;
            time_txtbox.Text = time.ToString();
            activate_btn.PerformClick();
        }

        public bool as_dialog = false;

        public MainForm(bool as_dialog)
        {
            InitializeComponent();
            this.as_dialog = as_dialog;
        }

        private void time_txtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                activate_btn.Focus();
                return;
            }
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b') return;
            e.Handled = true;
        }

        private void activate_btn_Click(object sender, EventArgs e)
        {
            if (as_dialog)
            {
                DialogResult = DialogResult.OK;
                return;
            }
            activate_btn.Text = "Activated";
            explode2_lbl.Text = "After";
            explode_lbl.Text = "Explode";
            explode_lbl.Font = new Font(explode_lbl.Font.FontFamily, 17F);
            time_txtbox.Visible = activate_btn.Enabled = false;
            time_lbl.Visible = true;
            time = int.Parse(time_txtbox.Text);
            if (time < 18)
            {
                InvokeAction(() => SoundPlayer.PlaySound(Properties.Resources.Countdown2, Handle, time), this);
                if (time <= 10) shaker.Start();
            }
            text_render.RunWorkerAsync();
            timer1.Start();
        }

        private void time_txtbox_Enter(object sender, EventArgs e)
        {
            time_txtbox.BorderStyle = BorderStyle.Fixed3D;
            time_txtbox.BackColor = SystemColors.Window;
            time_txtbox.SelectAll();
        }

        private void time_txtbox_Leave(object sender, EventArgs e)
        {
            time_txtbox.BorderStyle = BorderStyle.None;
            time_txtbox.BackColor = SystemColors.Control;
            try
            {
                time_txtbox.Text = int.Parse(time_txtbox.Text).ToString().PadLeft(4, '0');
                time_lbl.Text = time_txtbox.Text;
            }
            catch { }
        }

        delegate void SetTextCallBack(string text);

        public static void InvokeAction(Action action, Control control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
                return;
            }
            action();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (as_dialog)
            {
                if (DialogResult != DialogResult.OK)
                    DialogResult = DialogResult.Cancel;
                return;
            }
            Hide();
            if (activate_btn.Enabled)
            {
                SoundPlayer.Stop("countdown");
                SoundPlayer.Close("countdown");
                return;
            }
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(5000);
            e.Cancel = true;
        }

        private void shaker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                InvokeAction(() =>
                {
                    var b = (int)time_lbl.Tag <= 5;
                    WindowShaker.ShakeCurrentWindows(b);
                    WindowShaker.ShakeMouse(b);
                }, this);
            }
        }

        private void txt_render_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                BackColor = Color.FromArgb(204, 2, 2);
                panel1.BackColor = SystemColors.Control;
                Thread.Sleep(250);
                BackColor = SystemColors.Control;
                Thread.Sleep(250);
                BackColor = Color.FromArgb(238, 210, 2);
                panel1.BackColor = SystemColors.Control;
                Thread.Sleep(250);
                BackColor = SystemColors.Control;
                Thread.Sleep(250);
            }
        }


        protected override void DefWndProc(ref Message m)
        {
            base.DefWndProc(ref m);
            if (m.Msg == 0x3B9)
            {
                var sound_player = new SoundPlayer("countdown");
                sound_player.Stop();
                sound_player.Close();
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            notifyIcon1.Visible = false;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            int.TryParse(time_txtbox.Text, out time);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time--;
            switch (time)
            {
                case -1:
#if !DEBUG
                    Functions.BSoD();
#endif
                    Environment.Exit(0);
                    return;
                case 0:
                    InvokeAction(() =>
                    {
                        ForeColor = Color.Red;
                        notifyIcon1.Text = explode_lbl.Text = "Exploded";
                    }, this);
                    break;
                case 10:
                    InvokeAction(() => shaker.Start(), this);
                    break;
                case 17:
                    InvokeAction(() => SoundPlayer.PlaySound(Properties.Resources.Countdown2, Handle), this);
                    break;
                default:
                    if (time >= 17)
                        InvokeAction(() => SoundPlayer.PlaySound(Properties.Resources.Countdown, Handle), this);
                    break;
            }
            InvokeAction(() =>
            {
                time_lbl.Tag = time;
                notifyIcon1.Text = (time_lbl.Text = time.ToString().PadLeft(4, '0')) + "s";
            }, this);
        }

        private void shaker_Tick(object sender, EventArgs e)
        {
            InvokeAction(() =>
            {
                var b = (int)time_lbl.Tag <= 5;
                WindowShaker.ShakeCurrentWindows(b);
                WindowShaker.ShakeMouse(b);
            }, this);
        }
    }
}
