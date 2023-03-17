namespace CozifyWindows
{
    partial class FormRuuvi
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.timerInit = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxMqttBroker = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAddRuuviSensor = new System.Windows.Forms.TextBox();
            this.buttonAddRuuviSensor = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonDeleteSensor = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxDeviceAction = new System.Windows.Forms.ComboBox();
            this.comboBoxDeviceList = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDoNotTouch = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "RuuviTag Sensors / MQTT topics";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(16, 277);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(268, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // timerInit
            // 
            this.timerInit.Enabled = true;
            this.timerInit.Interval = 1;
            this.timerInit.Tick += new System.EventHandler(this.timerInit_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "MQTT server address:port";
            // 
            // textBoxMqttBroker
            // 
            this.textBoxMqttBroker.Location = new System.Drawing.Point(16, 251);
            this.textBoxMqttBroker.Name = "textBoxMqttBroker";
            this.textBoxMqttBroker.Size = new System.Drawing.Size(268, 20);
            this.textBoxMqttBroker.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(312, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Sensor (MQTT topic)";
            // 
            // textBoxAddRuuviSensor
            // 
            this.textBoxAddRuuviSensor.Location = new System.Drawing.Point(315, 32);
            this.textBoxAddRuuviSensor.Name = "textBoxAddRuuviSensor";
            this.textBoxAddRuuviSensor.Size = new System.Drawing.Size(214, 20);
            this.textBoxAddRuuviSensor.TabIndex = 6;
            // 
            // buttonAddRuuviSensor
            // 
            this.buttonAddRuuviSensor.Location = new System.Drawing.Point(315, 157);
            this.buttonAddRuuviSensor.Name = "buttonAddRuuviSensor";
            this.buttonAddRuuviSensor.Size = new System.Drawing.Size(214, 23);
            this.buttonAddRuuviSensor.TabIndex = 7;
            this.buttonAddRuuviSensor.Text = "Save sensor";
            this.buttonAddRuuviSensor.UseVisualStyleBackColor = true;
            this.buttonAddRuuviSensor.Click += new System.EventHandler(this.buttonAddRuuviSensor_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(16, 32);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(264, 160);
            this.listBox1.TabIndex = 8;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // buttonDeleteSensor
            // 
            this.buttonDeleteSensor.Location = new System.Drawing.Point(16, 198);
            this.buttonDeleteSensor.Name = "buttonDeleteSensor";
            this.buttonDeleteSensor.Size = new System.Drawing.Size(264, 23);
            this.buttonDeleteSensor.TabIndex = 9;
            this.buttonDeleteSensor.Text = "Delete";
            this.buttonDeleteSensor.UseVisualStyleBackColor = true;
            this.buttonDeleteSensor.Click += new System.EventHandler(this.buttonDeleteSensor_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(315, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "When temperature";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(455, 66);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(101, 20);
            this.textBox1.TabIndex = 11;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "<",
            ">"});
            this.comboBox1.Location = new System.Drawing.Point(416, 65);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(33, 21);
            this.comboBox1.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(318, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "turn device";
            // 
            // comboBoxDeviceAction
            // 
            this.comboBoxDeviceAction.FormattingEnabled = true;
            this.comboBoxDeviceAction.Items.AddRange(new object[] {
            "ON",
            "OFF"});
            this.comboBoxDeviceAction.Location = new System.Drawing.Point(562, 96);
            this.comboBoxDeviceAction.Name = "comboBoxDeviceAction";
            this.comboBoxDeviceAction.Size = new System.Drawing.Size(44, 21);
            this.comboBoxDeviceAction.TabIndex = 14;
            // 
            // comboBoxDeviceList
            // 
            this.comboBoxDeviceList.FormattingEnabled = true;
            this.comboBoxDeviceList.Items.AddRange(new object[] {
            "ON",
            "OFF"});
            this.comboBoxDeviceList.Location = new System.Drawing.Point(384, 96);
            this.comboBoxDeviceList.Name = "comboBoxDeviceList";
            this.comboBoxDeviceList.Size = new System.Drawing.Size(172, 21);
            this.comboBoxDeviceList.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(321, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "and do not touch it within";
            // 
            // textBoxDoNotTouch
            // 
            this.textBoxDoNotTouch.Location = new System.Drawing.Point(442, 131);
            this.textBoxDoNotTouch.Name = "textBoxDoNotTouch";
            this.textBoxDoNotTouch.Size = new System.Drawing.Size(101, 20);
            this.textBoxDoNotTouch.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(549, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "seconds";
            // 
            // FormRuuvi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 308);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxDoNotTouch);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxDeviceList);
            this.Controls.Add(this.comboBoxDeviceAction);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonDeleteSensor);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonAddRuuviSensor);
            this.Controls.Add(this.textBoxAddRuuviSensor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxMqttBroker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label1);
            this.Name = "FormRuuvi";
            this.Text = "RuuviTag sensors";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Timer timerInit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxMqttBroker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAddRuuviSensor;
        private System.Windows.Forms.Button buttonAddRuuviSensor;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button buttonDeleteSensor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxDeviceAction;
        private System.Windows.Forms.ComboBox comboBoxDeviceList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxDoNotTouch;
        private System.Windows.Forms.Label label7;
    }
}