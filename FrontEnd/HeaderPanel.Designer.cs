namespace TourTrader
{
    partial class HeaderPanel
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
            this.expectedvalueLabel = new System.Windows.Forms.Label();
            this.standarddeviationLabel = new System.Windows.Forms.Label();
            this.kurtosisLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.latesttransactionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // expectedvalueLabel
            // 
            this.expectedvalueLabel.AutoSize = true;
            this.expectedvalueLabel.Location = new System.Drawing.Point(7, 6);
            this.expectedvalueLabel.Name = "expectedvalueLabel";
            this.expectedvalueLabel.Size = new System.Drawing.Size(14, 13);
            this.expectedvalueLabel.TabIndex = 0;
            this.expectedvalueLabel.Text = "E";
            // 
            // standarddeviationLabel
            // 
            this.standarddeviationLabel.AutoSize = true;
            this.standarddeviationLabel.Location = new System.Drawing.Point(62, 6);
            this.standarddeviationLabel.Name = "standarddeviationLabel";
            this.standarddeviationLabel.Size = new System.Drawing.Size(14, 13);
            this.standarddeviationLabel.TabIndex = 1;
            this.standarddeviationLabel.Text = "V";
            // 
            // kurtosisLabel
            // 
            this.kurtosisLabel.AutoSize = true;
            this.kurtosisLabel.Location = new System.Drawing.Point(126, 6);
            this.kurtosisLabel.Name = "kurtosisLabel";
            this.kurtosisLabel.Size = new System.Drawing.Size(14, 13);
            this.kurtosisLabel.TabIndex = 2;
            this.kurtosisLabel.Text = "K";
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Red;
            this.cancelButton.Location = new System.Drawing.Point(418, 1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(89, 24);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel All";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.click_CancelButton);
            // 
            // latesttransactionLabel
            // 
            this.latesttransactionLabel.AutoSize = true;
            this.latesttransactionLabel.Location = new System.Drawing.Point(205, 6);
            this.latesttransactionLabel.Name = "latesttransactionLabel";
            this.latesttransactionLabel.Size = new System.Drawing.Size(27, 13);
            this.latesttransactionLabel.TabIndex = 4;
            this.latesttransactionLabel.Text = "Last";
            // 
            // MainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.latesttransactionLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.kurtosisLabel);
            this.Controls.Add(this.standarddeviationLabel);
            this.Controls.Add(this.expectedvalueLabel);
            this.Name = "MainPanel";
            this.Size = new System.Drawing.Size(1441, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label expectedvalueLabel;
        private System.Windows.Forms.Label standarddeviationLabel;
        private System.Windows.Forms.Label kurtosisLabel;
        private System.Windows.Forms.Label latesttransactionLabel;
        private System.Windows.Forms.Button cancelButton;
        
    }
}
