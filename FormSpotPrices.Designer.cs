namespace CozifyWindows
{
    partial class FormSpotPrices
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
            this.listBoxAvailableDevices = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSpotHours = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTimerSpotPrice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxMaxPrice = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonSaveSettings = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBoxAvailableDevices
            // 
            this.listBoxAvailableDevices.FormattingEnabled = true;
            this.listBoxAvailableDevices.Location = new System.Drawing.Point(12, 49);
            this.listBoxAvailableDevices.Name = "listBoxAvailableDevices";
            this.listBoxAvailableDevices.Size = new System.Drawing.Size(186, 95);
            this.listBoxAvailableDevices.TabIndex = 10;
            this.listBoxAvailableDevices.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBoxAvailableDevices_MouseClick);
            this.listBoxAvailableDevices.SelectedIndexChanged += new System.EventHandler(this.listBoxAvailableDevices_SelectedIndexChanged);
            this.listBoxAvailableDevices.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBoxAvailableDevices_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Selected Power Outlets";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Available Power Outlets";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "in cheapest hours: 0-24";
            // 
            // textBoxSpotHours
            // 
            this.textBoxSpotHours.Location = new System.Drawing.Point(236, 166);
            this.textBoxSpotHours.Name = "textBoxSpotHours";
            this.textBoxSpotHours.Size = new System.Drawing.Size(100, 20);
            this.textBoxSpotHours.TabIndex = 15;
            this.textBoxSpotHours.TextChanged += new System.EventHandler(this.textBoxSpotHours_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "And do not touch it within (seconds)";
            // 
            // textBoxTimerSpotPrice
            // 
            this.textBoxTimerSpotPrice.Location = new System.Drawing.Point(236, 235);
            this.textBoxTimerSpotPrice.Name = "textBoxTimerSpotPrice";
            this.textBoxTimerSpotPrice.Size = new System.Drawing.Size(100, 20);
            this.textBoxTimerSpotPrice.TabIndex = 17;
            this.textBoxTimerSpotPrice.TextChanged += new System.EventHandler(this.textBoxTimerSpotPrice_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(342, 238);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "seconds";
            // 
            // textBoxMaxPrice
            // 
            this.textBoxMaxPrice.Location = new System.Drawing.Point(236, 200);
            this.textBoxMaxPrice.Name = "textBoxMaxPrice";
            this.textBoxMaxPrice.Size = new System.Drawing.Size(100, 20);
            this.textBoxMaxPrice.TabIndex = 20;
            this.textBoxMaxPrice.TextChanged += new System.EventHandler(this.textBoxMaxPrice_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 203);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "when price <=";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(342, 203);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "c/kWh";
            // 
            // buttonSaveSettings
            // 
            this.buttonSaveSettings.Location = new System.Drawing.Point(18, 287);
            this.buttonSaveSettings.Name = "buttonSaveSettings";
            this.buttonSaveSettings.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveSettings.TabIndex = 22;
            this.buttonSaveSettings.Text = "Save";
            this.buttonSaveSettings.UseVisualStyleBackColor = true;
            this.buttonSaveSettings.Click += new System.EventHandler(this.buttonSaveSettings_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 187);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "OR";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Keep device ON";
            // 
            // FormSpotPrices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.buttonSaveSettings);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxMaxPrice);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxTimerSpotPrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxSpotHours);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBoxAvailableDevices);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormSpotPrices";
            this.Text = "FormSpotPrices";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listBoxAvailableDevices;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSpotHours;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTimerSpotPrice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxMaxPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonSaveSettings;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}