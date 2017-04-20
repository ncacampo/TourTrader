using System.Windows.Forms;

namespace TourTrader
{
    partial class Login
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ApiKeyTextBox = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.DropDownMenu = new System.Windows.Forms.ComboBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.ConnectionMessage = new System.Windows.Forms.Label();
            this.ChooseMarket = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ApiKeyTextBox
            // 
            this.ApiKeyTextBox.Location = new System.Drawing.Point(12, 12);
            this.ApiKeyTextBox.Name = "ApiKeyTextBox";
            this.ApiKeyTextBox.Size = new System.Drawing.Size(174, 20);
            this.ApiKeyTextBox.TabIndex = 0;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(205, 10);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButtonClick);
            // 
            // DropDownMenu
            // 
            this.DropDownMenu.FormattingEnabled = true;
            this.DropDownMenu.Location = new System.Drawing.Point(47, 137);
            this.DropDownMenu.Name = "DropDownMenu";
            this.DropDownMenu.Size = new System.Drawing.Size(121, 21);
            this.DropDownMenu.TabIndex = 2;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(195, 135);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // ConnectionMessage
            // 
            this.ConnectionMessage.AutoSize = true;
            this.ConnectionMessage.Location = new System.Drawing.Point(80, 78);
            this.ConnectionMessage.Name = "ConnectionMessage";
            this.ConnectionMessage.Size = new System.Drawing.Size(0, 13);
            this.ConnectionMessage.TabIndex = 4;
            // 
            // ChooseMarket
            // 
            this.ChooseMarket.AutoSize = true;
            this.ChooseMarket.Location = new System.Drawing.Point(45, 118);
            this.ChooseMarket.Name = "ChooseMarket";
            this.ChooseMarket.Size = new System.Drawing.Size(0, 13);
            this.ChooseMarket.TabIndex = 5;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.ChooseMarket);
            this.Controls.Add(this.ConnectionMessage);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.DropDownMenu);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.ApiKeyTextBox);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();
            this.FormClosing += Login_FormClosing;
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                System.Environment.Exit(1);
        }

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label ConnectionMessage;
        private System.Windows.Forms.Label ChooseMarket;
    }
}
