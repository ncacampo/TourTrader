namespace TourTrader
{
    partial class PricingForm
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
            this.ButtonLay = new System.Windows.Forms.Button();
            this.ButtonBack = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.Label();
            this.minimumprice = new System.Windows.Forms.TextBox();
            this.startprice = new System.Windows.Forms.TextBox();
            this.maximumprice = new System.Windows.Forms.TextBox();
            this.autobet = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.amountBox2 = new System.Windows.Forms.TextBox();
            this.Graph = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonLay
            // 
            this.ButtonLay.Location = new System.Drawing.Point(88, 49);
            this.ButtonLay.Name = "ButtonLay";
            this.ButtonLay.Size = new System.Drawing.Size(75, 23);
            this.ButtonLay.TabIndex = 19;
            this.ButtonLay.Text = "Lay";
            this.ButtonLay.UseVisualStyleBackColor = true;
            this.ButtonLay.Click += new System.EventHandler(this.clickLayButton);
            // 
            // ButtonBack
            // 
            this.ButtonBack.Location = new System.Drawing.Point(633, 49);
            this.ButtonBack.Name = "ButtonBack";
            this.ButtonBack.Size = new System.Drawing.Size(75, 23);
            this.ButtonBack.TabIndex = 18;
            this.ButtonBack.Text = "Back";
            this.ButtonBack.UseVisualStyleBackColor = true;
            this.ButtonBack.Click += new System.EventHandler(this.clickButtonBack);
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Location = new System.Drawing.Point(324, 1);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(33, 13);
            this.name.TabIndex = 3;
            this.name.Text = "name";
            // 
            // minimumprice
            // 
            this.minimumprice.Location = new System.Drawing.Point(184, 52);
            this.minimumprice.Name = "minimumprice";
            this.minimumprice.Size = new System.Drawing.Size(69, 20);
            this.minimumprice.TabIndex = 9;
            this.minimumprice.Text = "1.01";
            // 
            // startprice
            // 
            this.startprice.Location = new System.Drawing.Point(306, 52);
            this.startprice.Name = "startprice";
            this.startprice.Size = new System.Drawing.Size(69, 20);
            this.startprice.TabIndex = 10;
            // 
            // maximumprice
            // 
            this.maximumprice.Location = new System.Drawing.Point(434, 52);
            this.maximumprice.Name = "maximumprice";
            this.maximumprice.Size = new System.Drawing.Size(69, 20);
            this.maximumprice.TabIndex = 11;
            // 
            // autobet
            // 
            this.autobet.Location = new System.Drawing.Point(-2, 49);
            this.autobet.Name = "autobet";
            this.autobet.Size = new System.Drawing.Size(75, 23);
            this.autobet.TabIndex = 12;
            this.autobet.Text = "Autobet";
            this.autobet.UseVisualStyleBackColor = true;
            this.autobet.Click += new System.EventHandler(this.clickEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(183, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "minimumprice";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(317, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "startprice";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(430, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "maximumprice";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(574, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "amount";
            // 
            // amountBox2
            // 
            this.amountBox2.Location = new System.Drawing.Point(558, 52);
            this.amountBox2.Name = "amountBox2";
            this.amountBox2.Size = new System.Drawing.Size(69, 20);
            this.amountBox2.TabIndex = 12;
            this.amountBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Enter);
            // 
            // pictureBox1
            // 
            this.Graph.Location = new System.Drawing.Point(127, 95);
            this.Graph.Name = "pictureBox1";
            this.Graph.Size = new System.Drawing.Size(427, 308);
            this.Graph.TabIndex = 20;
            this.Graph.TabStop = false;
            
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 415);
            this.Controls.Add(this.Graph);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.amountBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.autobet);
            this.Controls.Add(this.maximumprice);
            this.Controls.Add(this.startprice);
            this.Controls.Add(this.minimumprice);
            this.Controls.Add(this.name);
            this.Controls.Add(this.ButtonBack);
            this.Controls.Add(this.ButtonLay);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button ButtonLay;
        public System.Windows.Forms.Button ButtonBack;
        public System.Windows.Forms.Label name;
        public System.Windows.Forms.TextBox minimumprice;
        public System.Windows.Forms.TextBox startprice;
        public System.Windows.Forms.TextBox maximumprice;
        private System.Windows.Forms.Button autobet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox amountBox2;
        private System.Windows.Forms.PictureBox Graph;
    }
}