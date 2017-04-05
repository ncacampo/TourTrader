namespace TourTrader
{
    partial class RiderPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StatusButton = new System.Windows.Forms.Button();
            this.PNL = new System.Windows.Forms.Label();
            this.rider = new System.Windows.Forms.Label();
            this.discard = new System.Windows.Forms.Label();
            this.price = new System.Windows.Forms.Label();
            this.overround = new System.Windows.Forms.Label();
            this.omzet = new System.Windows.Forms.Label();
            this.meback = new System.Windows.Forms.Label();
            this.back = new System.Windows.Forms.Label();
            this.lay = new System.Windows.Forms.Label();
            this.melay = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.StatusButton.BackColor = System.Drawing.Color.Red;
            this.StatusButton.Location = new System.Drawing.Point(33, -1);
            this.StatusButton.Name = "button1";
            this.StatusButton.Size = new System.Drawing.Size(22, 20);
            this.StatusButton.TabIndex = 1;
            this.StatusButton.UseVisualStyleBackColor = false;
            this.StatusButton.Click += new System.EventHandler(this.clickStatusButton);
            // 
            // PL
            // 
            this.PNL.AutoSize = true;
            this.PNL.Location = new System.Drawing.Point(3, 3);
            this.PNL.Name = "PL";
            this.PNL.Size = new System.Drawing.Size(51, 13);
            this.PNL.TabIndex = 6;
            this.PNL.Text = "profit-loss";
            // 
            // rider
            // 
            this.rider.AutoSize = true;
            this.rider.Location = new System.Drawing.Point(59, 3);
            this.rider.Name = "rider";
            this.rider.Size = new System.Drawing.Size(86, 13);
            this.rider.TabIndex = 0;
            this.rider.Text = "Alberto Contador\r\n";
            this.rider.Click += new System.EventHandler(this.clickRider);
            // 
            // discard
            // 
            this.discard.BackColor = System.Drawing.Color.White;
            this.discard.Location = new System.Drawing.Point(417, 3);
            this.discard.Name = "discard";
            this.discard.Size = new System.Drawing.Size(14, 15);
            this.discard.TabIndex = 8;
            this.discard.Text = "X";
            this.discard.Click += new System.EventHandler(this.clickDiscard);
            this.discard.DoubleClick += new System.EventHandler(this.doubleclickDiscard);
            // 
            // price
            // 
            this.price.AutoSize = true;
            this.price.Location = new System.Drawing.Point(340, 3);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(25, 13);
            this.price.TabIndex = 9;
            this.price.Text = "prijs";
            // 
            // overround
            // 
            this.overround.AutoSize = true;
            this.overround.Location = new System.Drawing.Point(301, 3);
            this.overround.Name = "overround";
            this.overround.Size = new System.Drawing.Size(28, 13);
            this.overround.TabIndex = 11;
            this.overround.Text = "over";
            // 
            // omzet
            // 
            this.omzet.AutoSize = true;
            this.omzet.Location = new System.Drawing.Point(374, 3);
            this.omzet.Name = "omzet";
            this.omzet.Size = new System.Drawing.Size(46, 13);
            this.omzet.TabIndex = 13;
            this.omzet.Text = "turnover";
            // 
            // meback
            // 
            this.meback.AutoSize = true;
            this.meback.Location = new System.Drawing.Point(250, 3);
            this.meback.Name = "meback";
            this.meback.Size = new System.Drawing.Size(45, 13);
            this.meback.TabIndex = 14;
            this.meback.Text = "meback";
            // 
            // back
            // 
            this.back.AutoSize = true;
            this.back.Location = new System.Drawing.Point(177, 3);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(31, 13);
            this.back.TabIndex = 15;
            this.back.Text = "back";
            this.back.Click += new System.EventHandler(this.clickBack);
            // 
            // lay
            // 
            this.lay.AutoSize = true;
            this.lay.Location = new System.Drawing.Point(151, 3);
            this.lay.Name = "lay";
            this.lay.Size = new System.Drawing.Size(20, 13);
            this.lay.TabIndex = 16;
            this.lay.Text = "lay";
            this.lay.Click += new System.EventHandler(this.clickLay);
            // 
            // melay
            // 
            this.melay.AutoSize = true;
            this.melay.Location = new System.Drawing.Point(214, 3);
            this.melay.Name = "melay";
            this.melay.Size = new System.Drawing.Size(34, 13);
            this.melay.TabIndex = 17;
            this.melay.Text = "melay";
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.melay);
            this.Controls.Add(this.lay);
            this.Controls.Add(this.back);
            this.Controls.Add(this.meback);
            this.Controls.Add(this.omzet);
            this.Controls.Add(this.overround);
            this.Controls.Add(this.price);
            this.Controls.Add(this.discard);
            this.Controls.Add(this.rider);
            this.Controls.Add(this.PNL);
            this.Controls.Add(this.StatusButton);
            this.Size = new System.Drawing.Size(438, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button StatusButton;
        public System.Windows.Forms.Label PNL;
        public System.Windows.Forms.Label rider;
        public System.Windows.Forms.Label discard;
        public System.Windows.Forms.Label price;
        public System.Windows.Forms.Label overround;
        public System.Windows.Forms.Label meback;
        public System.Windows.Forms.Label back;
        public System.Windows.Forms.Label lay;
        public System.Windows.Forms.Label melay;
        public System.Windows.Forms.Label omzet;
    }
}
