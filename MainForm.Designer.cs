namespace TourTrader
{
    partial class MainForm
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
            this.Profit = new System.Windows.Forms.Label();
            this.CancelAll = new System.Windows.Forms.Button();
            this.Variance = new System.Windows.Forms.Label();
            this.Kurtosis = new System.Windows.Forms.Label();
            this.Sprint = new System.Windows.Forms.Label();
            this.Punch = new System.Windows.Forms.Label();
            this.LongRange = new System.Windows.Forms.Label();
            this.LastTrade = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // winst
            // 
            this.Profit.AutoSize = true;
            this.Profit.Location = new System.Drawing.Point(12, 9);
            this.Profit.Name = "winst";
            this.Profit.Size = new System.Drawing.Size(33, 13);
            this.Profit.TabIndex = 0;
            this.Profit.Text = "mean";
            // 
            // button1
            // 
            this.CancelAll.BackColor = System.Drawing.Color.Red;
            this.CancelAll.ForeColor = System.Drawing.Color.Black;
            this.CancelAll.Location = new System.Drawing.Point(860, 4);
            this.CancelAll.Name = "button1";
            this.CancelAll.Size = new System.Drawing.Size(93, 23);
            this.CancelAll.TabIndex = 1;
            this.CancelAll.Text = "button1";
            this.CancelAll.UseVisualStyleBackColor = false;
            this.CancelAll.Click += new System.EventHandler(this.clickCancelAll);
            // 
            // variance
            // 
            this.Variance.AutoSize = true;
            this.Variance.Location = new System.Drawing.Point(64, 9);
            this.Variance.Name = "variance";
            this.Variance.Size = new System.Drawing.Size(48, 13);
            this.Variance.TabIndex = 3;
            this.Variance.Text = "variance";
            // 
            // kurtosis
            // 
            this.Kurtosis.AutoSize = true;
            this.Kurtosis.Location = new System.Drawing.Point(129, 9);
            this.Kurtosis.Name = "kurtosis";
            this.Kurtosis.Size = new System.Drawing.Size(43, 13);
            this.Kurtosis.TabIndex = 4;
            this.Kurtosis.Text = "kurtosis";
            // 
            // sprint
            // 
            this.Sprint.AutoSize = true;
            this.Sprint.Location = new System.Drawing.Point(306, 9);
            this.Sprint.Name = "sprint";
            this.Sprint.Size = new System.Drawing.Size(32, 13);
            this.Sprint.TabIndex = 5;
            this.Sprint.Text = "sprint";
            // 
            // punch
            // 
            this.Punch.AutoSize = true;
            this.Punch.Location = new System.Drawing.Point(399, 9);
            this.Punch.Name = "punch";
            this.Punch.Size = new System.Drawing.Size(37, 13);
            this.Punch.TabIndex = 6;
            this.Punch.Text = "punch";
            // 
            // long_range
            // 
            this.LongRange.AutoSize = true;
            this.LongRange.Location = new System.Drawing.Point(512, 9);
            this.LongRange.Name = "long_range";
            this.LongRange.Size = new System.Drawing.Size(60, 13);
            this.LongRange.TabIndex = 7;
            this.LongRange.Text = "long_range";
            // 
            // Last_trade
            // 
            this.LastTrade.AutoSize = true;
            this.LastTrade.Location = new System.Drawing.Point(664, 9);
            this.LastTrade.Name = "Last_trade";
            this.LastTrade.Size = new System.Drawing.Size(54, 13);
            this.LastTrade.TabIndex = 8;
            this.LastTrade.Text = "Last trade";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1001, 500);
            this.Controls.Add(this.LastTrade);
            this.Controls.Add(this.LongRange);
            this.Controls.Add(this.Punch);
            this.Controls.Add(this.Sprint);
            this.Controls.Add(this.Kurtosis);
            this.Controls.Add(this.Variance);
            this.Controls.Add(this.CancelAll);
            this.Controls.Add(this.Profit);
            this.Name = "Form1";
            this.Text = "";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label Profit;
        private System.Windows.Forms.Button CancelAll;
        public System.Windows.Forms.Label Variance;
        public System.Windows.Forms.Label Kurtosis;
        public System.Windows.Forms.Label Sprint;
        public System.Windows.Forms.Label Punch;
        public System.Windows.Forms.Label LongRange;
        public System.Windows.Forms.Label LastTrade;
    }
}