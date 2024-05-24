namespace Bomb
{
    partial class FullScreenForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.explode_lbl = new System.Windows.Forms.Label();
            this.countdown_lbl = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.shaker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // explode_lbl
            // 
            this.explode_lbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.explode_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 100F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.explode_lbl.ForeColor = System.Drawing.Color.White;
            this.explode_lbl.Location = new System.Drawing.Point(0, 0);
            this.explode_lbl.Name = "explode_lbl";
            this.explode_lbl.Size = new System.Drawing.Size(800, 321);
            this.explode_lbl.TabIndex = 0;
            this.explode_lbl.Text = "Explode";
            this.explode_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // countdown_lbl
            // 
            this.countdown_lbl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.countdown_lbl.ForeColor = System.Drawing.Color.White;
            this.countdown_lbl.Location = new System.Drawing.Point(0, 358);
            this.countdown_lbl.Name = "countdown_lbl";
            this.countdown_lbl.Size = new System.Drawing.Size(800, 122);
            this.countdown_lbl.TabIndex = 1;
            this.countdown_lbl.Text = "label2";
            this.countdown_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // shaker
            // 
            this.shaker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // FullScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.countdown_lbl);
            this.Controls.Add(this.explode_lbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FullScreenForm";
            this.Text = "Bomb";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FullScreenForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label explode_lbl;
        private System.Windows.Forms.Label countdown_lbl;
        private System.Windows.Forms.Timer timer;
        private System.ComponentModel.BackgroundWorker shaker;
    }
}