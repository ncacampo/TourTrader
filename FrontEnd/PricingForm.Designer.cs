using System.Windows.Forms;

namespace TourTrader
{
    partial class PricingForm
    {
        private Button ButtonLay;
        private Button ButtonBack;
        private Label NameLabel;
        private TextBox MinpriceBox;
        private TextBox StartpriceBox;
        private TextBox MaxPriceBox;
        private Button AutoOrder;
        private Label minimumpriceLabel;
        private Label startpriceLabel;
        private Label maximumPrice;
        private Label amount;
        private TextBox SizeBox;
        private PictureBox Graph;
        private Panel RiderProperties;

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
            BackEnd.clock.Start();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void addRiderlabels()
        {
            addRiderlabel("cumulative",Riders.AtID(ID).overround.ToString(),0);
            addRiderlabel("has_autoorder",Riders.AtID(ID).hasAutoOrder.ToString(),15);
            addRiderlabel("isinthemoney",Riders.AtID(ID).isInthemoney.ToString(),30);
            addRiderlabel("isOpen", Riders.AtID(ID).isOpen.ToString(),45);
            addRiderlabel("latestMarketPrice", Riders.AtID(ID).latestMarketprice.ToString(),60);
            addRiderlabel("marketAsk", Riders.AtID(ID).marketAsk.ToString(),90);
            addRiderlabel("marketBid", Riders.AtID(ID).marketBid.ToString(),105);
            addRiderlabel("maxPrice", Riders.AtID(ID).maxPrice.ToString(),120);
            addRiderlabel("minPrice", Riders.AtID(ID).minPrice.ToString(),135);
            addRiderlabel("myAsk", Riders.AtID(ID).myAsk.ToString(),150);
            addRiderlabel("myBid", Riders.AtID(ID).myBid.ToString(),165);
            addRiderlabel("name", Riders.AtID(ID).name.ToString(),180);
            addRiderlabel("pnl", Riders.AtID(ID).pnl.ToString(),195);
            addRiderlabel("totalMarketAmount", Riders.AtID(ID).totalmarketamount.ToString(),240);
            addRiderlabel("turnover", Riders.AtID(ID).turnover.ToString(),270);
        }

        private void addRiderlabel(string propertyName,dynamic propertyValue,int y_coord)
        {   
            Label label = new Label();
            label.Size = new System.Drawing.Size(200, 15);
            label.Location = new System.Drawing.Point(0,y_coord);
            label.Text = propertyName + "   " + propertyValue;
            RiderProperties.Controls.Add(label); 
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RiderProperties = new Panel();
            this.addRiderlabels();
            this.ButtonLay = new Button();
            this.ButtonBack = new Button();
            this.NameLabel = new Label();
            this.MinpriceBox = new TextBox();
            this.StartpriceBox = new TextBox();
            this.MaxPriceBox = new TextBox();
            this.AutoOrder = new Button();
            this.minimumpriceLabel = new Label();
            this.startpriceLabel = new Label();
            this.maximumPrice = new Label();
            this.amount = new Label();
            this.SizeBox = new TextBox();
            this.Graph = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).BeginInit();
            this.SuspendLayout();
            //
            // Labels
            //
            this.RiderProperties.Location = new System.Drawing.Point(500,100);
            this.RiderProperties.Size = new System.Drawing.Size(200,500);
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
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(324, 1);
            this.NameLabel.Name = "name";
            this.NameLabel.Size = new System.Drawing.Size(33, 13);
            this.NameLabel.TabIndex = 3;
            this.NameLabel.Text = "name";
            // 
            // minimumprice
            // 
            this.MinpriceBox.Location = new System.Drawing.Point(184, 52);
            this.MinpriceBox.Name = "minimumprice";
            this.MinpriceBox.Size = new System.Drawing.Size(69, 20);
            this.MinpriceBox.TabIndex = 9;
            this.MinpriceBox.Text = "1.01";
            // 
            // startprice
            // 
            this.StartpriceBox.Location = new System.Drawing.Point(306, 52);
            this.StartpriceBox.Name = "startprice";
            this.StartpriceBox.Size = new System.Drawing.Size(69, 20);
            this.StartpriceBox.TabIndex = 10;
            // 
            // maximumprice
            // 
            this.MaxPriceBox.Location = new System.Drawing.Point(434, 52);
            this.MaxPriceBox.Name = "maximumprice";
            this.MaxPriceBox.Size = new System.Drawing.Size(69, 20);
            this.MaxPriceBox.TabIndex = 11;
            // 
            // autobet
            // 
            this.AutoOrder.Location = new System.Drawing.Point(-2, 49);
            this.AutoOrder.Name = "autobet";
            this.AutoOrder.Size = new System.Drawing.Size(75, 23);
            this.AutoOrder.TabIndex = 12;
            this.AutoOrder.Text = "Autobet";
            this.AutoOrder.UseVisualStyleBackColor = true;
            this.AutoOrder.Click += new System.EventHandler(this.clickEnter);
            // 
            // label1
            // 
            this.minimumpriceLabel.AutoSize = true;
            this.minimumpriceLabel.Location = new System.Drawing.Point(183, 20);
            this.minimumpriceLabel.Name = "label1";
            this.minimumpriceLabel.Size = new System.Drawing.Size(70, 13);
            this.minimumpriceLabel.TabIndex = 13;
            this.minimumpriceLabel.Text = "minimumprice";
            // 
            // label2
            // 
            this.startpriceLabel.AutoSize = true;
            this.startpriceLabel.Location = new System.Drawing.Point(317, 20);
            this.startpriceLabel.Name = "label2";
            this.startpriceLabel.Size = new System.Drawing.Size(50, 13);
            this.startpriceLabel.TabIndex = 14;
            this.startpriceLabel.Text = "startprice";
            // 
            // label3
            // 
            this.maximumPrice.AutoSize = true;
            this.maximumPrice.Location = new System.Drawing.Point(430, 20);
            this.maximumPrice.Name = "label3";
            this.maximumPrice.Size = new System.Drawing.Size(73, 13);
            this.maximumPrice.TabIndex = 15;
            this.maximumPrice.Text = "maximumprice";
            // 
            // label4
            // 
            this.amount.AutoSize = true;
            this.amount.Location = new System.Drawing.Point(574, 20);
            this.amount.Name = "label4";
            this.amount.Size = new System.Drawing.Size(42, 13);
            this.amount.TabIndex = 17;
            this.amount.Text = "amount";
            // 
            // amountBox2
            // 
            this.SizeBox.Location = new System.Drawing.Point(558, 52);
            this.SizeBox.Name = "amountBox2";
            this.SizeBox.Size = new System.Drawing.Size(69, 20);
            this.SizeBox.TabIndex = 12;
            this.SizeBox.KeyPress += new KeyPressEventHandler(this.Enter);
            // 
            // pictureBox1
            // 
            this.Graph.Location = new System.Drawing.Point(127, 95);
            this.Graph.Name = "pictureBox1";
            this.Graph.Size = new System.Drawing.Size(427, 308);
            this.Graph.TabIndex = 20;
            this.Graph.TabStop = false;
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 415);
            this.Controls.Add(this.RiderProperties);
            this.Controls.Add(this.Graph);
            this.Controls.Add(this.amount);
            this.Controls.Add(this.SizeBox);
            this.Controls.Add(this.maximumPrice);
            this.Controls.Add(this.startpriceLabel);
            this.Controls.Add(this.minimumpriceLabel);
            this.Controls.Add(this.AutoOrder);
            this.Controls.Add(this.MaxPriceBox);
            this.Controls.Add(this.StartpriceBox);
            this.Controls.Add(this.MinpriceBox);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.ButtonBack);
            this.Controls.Add(this.ButtonLay);
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
    }
}