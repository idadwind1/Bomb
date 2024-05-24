namespace Bomb
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.text_render = new System.ComponentModel.BackgroundWorker();
            this.explode_lbl = new System.Windows.Forms.Label();
            this.activate_btn = new System.Windows.Forms.Button();
            this.explode2_lbl = new System.Windows.Forms.Label();
            this.time_txtbox = new System.Windows.Forms.TextBox();
            this.s_lbl = new System.Windows.Forms.Label();
            this.time_lbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.shaker = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // text_render
            // 
            this.text_render.DoWork += new System.ComponentModel.DoWorkEventHandler(this.txt_render_DoWork);
            // 
            // explode_lbl
            // 
            resources.ApplyResources(this.explode_lbl, "explode_lbl");
            this.explode_lbl.Name = "explode_lbl";
            // 
            // activate_btn
            // 
            resources.ApplyResources(this.activate_btn, "activate_btn");
            this.activate_btn.Name = "activate_btn";
            this.activate_btn.UseVisualStyleBackColor = true;
            this.activate_btn.Click += new System.EventHandler(this.activate_btn_Click);
            // 
            // explode2_lbl
            // 
            resources.ApplyResources(this.explode2_lbl, "explode2_lbl");
            this.explode2_lbl.Name = "explode2_lbl";
            // 
            // time_txtbox
            // 
            this.time_txtbox.BackColor = System.Drawing.SystemColors.Control;
            this.time_txtbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.time_txtbox, "time_txtbox");
            this.time_txtbox.Name = "time_txtbox";
            this.time_txtbox.Enter += new System.EventHandler(this.time_txtbox_Enter);
            this.time_txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.time_txtbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.time_txtbox_KeyPress);
            this.time_txtbox.Leave += new System.EventHandler(this.time_txtbox_Leave);
            // 
            // s_lbl
            // 
            resources.ApplyResources(this.s_lbl, "s_lbl");
            this.s_lbl.Name = "s_lbl";
            // 
            // time_lbl
            // 
            resources.ApplyResources(this.time_lbl, "time_lbl");
            this.time_lbl.Name = "time_lbl";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.s_lbl);
            this.panel1.Controls.Add(this.time_lbl);
            this.panel1.Controls.Add(this.time_txtbox);
            this.panel1.Controls.Add(this.explode2_lbl);
            this.panel1.Controls.Add(this.activate_btn);
            this.panel1.Controls.Add(this.explode_lbl);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // shaker
            // 
            this.shaker.Interval = 1;
            this.shaker.Tick += new System.EventHandler(this.shaker_Tick);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.ComponentModel.BackgroundWorker text_render;
        private System.Windows.Forms.Label explode_lbl;
        private System.Windows.Forms.Button activate_btn;
        private System.Windows.Forms.Label explode2_lbl;
        private System.Windows.Forms.TextBox time_txtbox;
        private System.Windows.Forms.Label s_lbl;
        private System.Windows.Forms.Label time_lbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer shaker;
    }
}

