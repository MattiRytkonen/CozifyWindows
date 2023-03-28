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
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxDeviceAction = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxDeviceList = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox12 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxDeviceList2 = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.comboBoxDeviceAction2 = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
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
            this.buttonSaveSettings.Location = new System.Drawing.Point(12, 415);
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
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 286);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "1. Keep device always";
            // 
            // comboBoxDeviceAction
            // 
            this.comboBoxDeviceAction.FormattingEnabled = true;
            this.comboBoxDeviceAction.Items.AddRange(new object[] {
            "ON",
            "OFF"});
            this.comboBoxDeviceAction.Location = new System.Drawing.Point(138, 283);
            this.comboBoxDeviceAction.Name = "comboBoxDeviceAction";
            this.comboBoxDeviceAction.Size = new System.Drawing.Size(44, 21);
            this.comboBoxDeviceAction.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(188, 286);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "when device ";
            // 
            // comboBoxDeviceList
            // 
            this.comboBoxDeviceList.FormattingEnabled = true;
            this.comboBoxDeviceList.Location = new System.Drawing.Point(265, 283);
            this.comboBoxDeviceList.Name = "comboBoxDeviceList";
            this.comboBoxDeviceList.Size = new System.Drawing.Size(172, 21);
            this.comboBoxDeviceList.TabIndex = 28;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(443, 286);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "is";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "ON",
            "OFF"});
            this.comboBox1.Location = new System.Drawing.Point(463, 283);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(44, 21);
            this.comboBox1.TabIndex = 30;
            // 
            // comboBox12
            // 
            this.comboBox12.FormattingEnabled = true;
            this.comboBox12.Items.AddRange(new object[] {
            "ON",
            "OFF"});
            this.comboBox12.Location = new System.Drawing.Point(463, 325);
            this.comboBox12.Name = "comboBox12";
            this.comboBox12.Size = new System.Drawing.Size(44, 21);
            this.comboBox12.TabIndex = 36;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(443, 328);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 13);
            this.label13.TabIndex = 35;
            this.label13.Text = "is";
            // 
            // comboBoxDeviceList2
            // 
            this.comboBoxDeviceList2.FormattingEnabled = true;
            this.comboBoxDeviceList2.Location = new System.Drawing.Point(265, 325);
            this.comboBoxDeviceList2.Name = "comboBoxDeviceList2";
            this.comboBoxDeviceList2.Size = new System.Drawing.Size(172, 21);
            this.comboBoxDeviceList2.TabIndex = 34;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(188, 328);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 13);
            this.label14.TabIndex = 33;
            this.label14.Text = "when device ";
            // 
            // comboBoxDeviceAction2
            // 
            this.comboBoxDeviceAction2.FormattingEnabled = true;
            this.comboBoxDeviceAction2.Items.AddRange(new object[] {
            "ON",
            "OFF"});
            this.comboBoxDeviceAction2.Location = new System.Drawing.Point(138, 325);
            this.comboBoxDeviceAction2.Name = "comboBoxDeviceAction2";
            this.comboBoxDeviceAction2.Size = new System.Drawing.Size(44, 21);
            this.comboBoxDeviceAction2.TabIndex = 32;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(18, 328);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(114, 13);
            this.label15.TabIndex = 31;
            this.label15.Text = "2. Keep device always";
            // 
            // FormSpotPrices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBox12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.comboBoxDeviceList2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.comboBoxDeviceAction2);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.comboBoxDeviceList);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.comboBoxDeviceAction);
            this.Controls.Add(this.label10);
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
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxDeviceAction;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxDeviceList;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxDeviceList2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBoxDeviceAction2;
        private System.Windows.Forms.Label label15;
    }
}