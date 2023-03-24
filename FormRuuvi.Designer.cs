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
            this.buttonSaveRule = new System.Windows.Forms.Button();
            this.listBoxRuuviTagSensors = new System.Windows.Forms.ListBox();
            this.buttonDeleteSensor = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTemperatureValue = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxDeviceAction = new System.Windows.Forms.ComboBox();
            this.comboBoxDeviceList = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDoNotTouch = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listBoxCozifySensors = new System.Windows.Forms.ListBox();
            this.listBoxSensorRuleList = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxRuleName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonAddRule = new System.Windows.Forms.Button();
            this.buttonDeleteRule = new System.Windows.Forms.Button();
            this.buttonAddRuuviTag = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxTimeStart = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxTimeEnd = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.checkBoxMO = new System.Windows.Forms.CheckBox();
            this.checkBoxTU = new System.Windows.Forms.CheckBox();
            this.checkBoxWE = new System.Windows.Forms.CheckBox();
            this.checkBoxTH = new System.Windows.Forms.CheckBox();
            this.checkBoxFR = new System.Windows.Forms.CheckBox();
            this.checkBoxSA = new System.Windows.Forms.CheckBox();
            this.checkBoxSU = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 234);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "RuuviTag Sensors / MQTT topics";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(12, 498);
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
            this.label2.Location = new System.Drawing.Point(9, 456);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "MQTT server address:port";
            // 
            // textBoxMqttBroker
            // 
            this.textBoxMqttBroker.Location = new System.Drawing.Point(12, 472);
            this.textBoxMqttBroker.Name = "textBoxMqttBroker";
            this.textBoxMqttBroker.Size = new System.Drawing.Size(268, 20);
            this.textBoxMqttBroker.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(312, 375);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Sensor or MQTT topic";
            // 
            // textBoxAddRuuviSensor
            // 
            this.textBoxAddRuuviSensor.Location = new System.Drawing.Point(315, 392);
            this.textBoxAddRuuviSensor.Name = "textBoxAddRuuviSensor";
            this.textBoxAddRuuviSensor.Size = new System.Drawing.Size(214, 20);
            this.textBoxAddRuuviSensor.TabIndex = 6;
            // 
            // buttonSaveRule
            // 
            this.buttonSaveRule.Location = new System.Drawing.Point(544, 224);
            this.buttonSaveRule.Name = "buttonSaveRule";
            this.buttonSaveRule.Size = new System.Drawing.Size(214, 23);
            this.buttonSaveRule.TabIndex = 7;
            this.buttonSaveRule.Text = "Save Rule";
            this.buttonSaveRule.UseVisualStyleBackColor = true;
            this.buttonSaveRule.Click += new System.EventHandler(this.buttonSaveRule_Click);
            // 
            // listBoxRuuviTagSensors
            // 
            this.listBoxRuuviTagSensors.FormattingEnabled = true;
            this.listBoxRuuviTagSensors.Location = new System.Drawing.Point(12, 253);
            this.listBoxRuuviTagSensors.Name = "listBoxRuuviTagSensors";
            this.listBoxRuuviTagSensors.Size = new System.Drawing.Size(264, 160);
            this.listBoxRuuviTagSensors.TabIndex = 8;
            this.listBoxRuuviTagSensors.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBoxRuuviTagSensors_MouseClick);
            this.listBoxRuuviTagSensors.SelectedIndexChanged += new System.EventHandler(this.listBoxRuuviTagSensors_SelectedIndexChanged);
            this.listBoxRuuviTagSensors.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBoxRuuviTagSensors_KeyPress);
            // 
            // buttonDeleteSensor
            // 
            this.buttonDeleteSensor.Location = new System.Drawing.Point(12, 419);
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
            this.label4.Location = new System.Drawing.Point(538, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "When temperature";
            // 
            // textBoxTemperatureValue
            // 
            this.textBoxTemperatureValue.Location = new System.Drawing.Point(678, 52);
            this.textBoxTemperatureValue.Name = "textBoxTemperatureValue";
            this.textBoxTemperatureValue.Size = new System.Drawing.Size(101, 20);
            this.textBoxTemperatureValue.TabIndex = 11;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "<",
            ">"});
            this.comboBox1.Location = new System.Drawing.Point(639, 51);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(33, 21);
            this.comboBox1.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(538, 85);
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
            this.comboBoxDeviceAction.Location = new System.Drawing.Point(785, 82);
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
            this.comboBoxDeviceList.Location = new System.Drawing.Point(607, 82);
            this.comboBoxDeviceList.Name = "comboBoxDeviceList";
            this.comboBoxDeviceList.Size = new System.Drawing.Size(172, 21);
            this.comboBoxDeviceList.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(546, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "and do not touch it within";
            // 
            // textBoxDoNotTouch
            // 
            this.textBoxDoNotTouch.Location = new System.Drawing.Point(667, 185);
            this.textBoxDoNotTouch.Name = "textBoxDoNotTouch";
            this.textBoxDoNotTouch.Size = new System.Drawing.Size(101, 20);
            this.textBoxDoNotTouch.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(774, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "seconds";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Cozify Sensors";
            // 
            // listBoxCozifySensors
            // 
            this.listBoxCozifySensors.FormattingEnabled = true;
            this.listBoxCozifySensors.Location = new System.Drawing.Point(12, 25);
            this.listBoxCozifySensors.Name = "listBoxCozifySensors";
            this.listBoxCozifySensors.Size = new System.Drawing.Size(264, 160);
            this.listBoxCozifySensors.TabIndex = 20;
            this.listBoxCozifySensors.SelectedIndexChanged += new System.EventHandler(this.listBoxCozifySensors_SelectedIndexChanged);
            // 
            // listBoxSensorRuleList
            // 
            this.listBoxSensorRuleList.FormattingEnabled = true;
            this.listBoxSensorRuleList.Location = new System.Drawing.Point(315, 25);
            this.listBoxSensorRuleList.Name = "listBoxSensorRuleList";
            this.listBoxSensorRuleList.Size = new System.Drawing.Size(154, 82);
            this.listBoxSensorRuleList.TabIndex = 21;
            this.listBoxSensorRuleList.Click += new System.EventHandler(this.listBoxSensorRuleList_Click);
            this.listBoxSensorRuleList.SelectedIndexChanged += new System.EventHandler(this.listBoxSensorRuleList_SelectedIndexChanged);
            this.listBoxSensorRuleList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBoxSensorRuleList_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(538, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "rule name";
            // 
            // textBoxRuleName
            // 
            this.textBoxRuleName.Location = new System.Drawing.Point(639, 22);
            this.textBoxRuleName.Name = "textBoxRuleName";
            this.textBoxRuleName.Size = new System.Drawing.Size(190, 20);
            this.textBoxRuleName.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(312, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Rules";
            // 
            // buttonAddRule
            // 
            this.buttonAddRule.Location = new System.Drawing.Point(313, 113);
            this.buttonAddRule.Name = "buttonAddRule";
            this.buttonAddRule.Size = new System.Drawing.Size(75, 23);
            this.buttonAddRule.TabIndex = 25;
            this.buttonAddRule.Text = "Add rule";
            this.buttonAddRule.UseVisualStyleBackColor = true;
            this.buttonAddRule.Click += new System.EventHandler(this.buttonAddRule_Click);
            // 
            // buttonDeleteRule
            // 
            this.buttonDeleteRule.Location = new System.Drawing.Point(394, 113);
            this.buttonDeleteRule.Name = "buttonDeleteRule";
            this.buttonDeleteRule.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteRule.TabIndex = 26;
            this.buttonDeleteRule.Text = "Delete rule";
            this.buttonDeleteRule.UseVisualStyleBackColor = true;
            this.buttonDeleteRule.Click += new System.EventHandler(this.buttonDeleteRule_Click);
            // 
            // buttonAddRuuviTag
            // 
            this.buttonAddRuuviTag.Location = new System.Drawing.Point(315, 418);
            this.buttonAddRuuviTag.Name = "buttonAddRuuviTag";
            this.buttonAddRuuviTag.Size = new System.Drawing.Size(214, 23);
            this.buttonAddRuuviTag.TabIndex = 27;
            this.buttonAddRuuviTag.Text = "Add";
            this.buttonAddRuuviTag.UseVisualStyleBackColor = true;
            this.buttonAddRuuviTag.Click += new System.EventHandler(this.buttonAddRuuviTag_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(540, 152);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "time (hh:mm)";
            // 
            // textBoxTimeStart
            // 
            this.textBoxTimeStart.Location = new System.Drawing.Point(620, 149);
            this.textBoxTimeStart.Name = "textBoxTimeStart";
            this.textBoxTimeStart.Size = new System.Drawing.Size(52, 20);
            this.textBoxTimeStart.TabIndex = 29;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(674, 152);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(10, 13);
            this.label12.TabIndex = 30;
            this.label12.Text = "-";
            // 
            // textBoxTimeEnd
            // 
            this.textBoxTimeEnd.Location = new System.Drawing.Point(690, 149);
            this.textBoxTimeEnd.Name = "textBoxTimeEnd";
            this.textBoxTimeEnd.Size = new System.Drawing.Size(52, 20);
            this.textBoxTimeEnd.TabIndex = 31;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(536, 118);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "weekdays";
            // 
            // checkBoxMO
            // 
            this.checkBoxMO.AutoSize = true;
            this.checkBoxMO.Location = new System.Drawing.Point(603, 117);
            this.checkBoxMO.Name = "checkBoxMO";
            this.checkBoxMO.Size = new System.Drawing.Size(40, 17);
            this.checkBoxMO.TabIndex = 34;
            this.checkBoxMO.Text = "mo";
            this.checkBoxMO.UseVisualStyleBackColor = true;
            // 
            // checkBoxTU
            // 
            this.checkBoxTU.AutoSize = true;
            this.checkBoxTU.Location = new System.Drawing.Point(649, 117);
            this.checkBoxTU.Name = "checkBoxTU";
            this.checkBoxTU.Size = new System.Drawing.Size(35, 17);
            this.checkBoxTU.TabIndex = 35;
            this.checkBoxTU.Text = "tu";
            this.checkBoxTU.UseVisualStyleBackColor = true;
            // 
            // checkBoxWE
            // 
            this.checkBoxWE.AutoSize = true;
            this.checkBoxWE.Location = new System.Drawing.Point(682, 117);
            this.checkBoxWE.Name = "checkBoxWE";
            this.checkBoxWE.Size = new System.Drawing.Size(40, 17);
            this.checkBoxWE.TabIndex = 36;
            this.checkBoxWE.Text = "we";
            this.checkBoxWE.UseVisualStyleBackColor = true;
            // 
            // checkBoxTH
            // 
            this.checkBoxTH.AutoSize = true;
            this.checkBoxTH.Location = new System.Drawing.Point(723, 117);
            this.checkBoxTH.Name = "checkBoxTH";
            this.checkBoxTH.Size = new System.Drawing.Size(35, 17);
            this.checkBoxTH.TabIndex = 37;
            this.checkBoxTH.Text = "th";
            this.checkBoxTH.UseVisualStyleBackColor = true;
            // 
            // checkBoxFR
            // 
            this.checkBoxFR.AutoSize = true;
            this.checkBoxFR.Location = new System.Drawing.Point(764, 117);
            this.checkBoxFR.Name = "checkBoxFR";
            this.checkBoxFR.Size = new System.Drawing.Size(32, 17);
            this.checkBoxFR.TabIndex = 38;
            this.checkBoxFR.Text = "fr";
            this.checkBoxFR.UseVisualStyleBackColor = true;
            // 
            // checkBoxSA
            // 
            this.checkBoxSA.AutoSize = true;
            this.checkBoxSA.Location = new System.Drawing.Point(796, 117);
            this.checkBoxSA.Name = "checkBoxSA";
            this.checkBoxSA.Size = new System.Drawing.Size(37, 17);
            this.checkBoxSA.TabIndex = 39;
            this.checkBoxSA.Text = "sa";
            this.checkBoxSA.UseVisualStyleBackColor = true;
            // 
            // checkBoxSU
            // 
            this.checkBoxSU.AutoSize = true;
            this.checkBoxSU.Location = new System.Drawing.Point(828, 117);
            this.checkBoxSU.Name = "checkBoxSU";
            this.checkBoxSU.Size = new System.Drawing.Size(37, 17);
            this.checkBoxSU.TabIndex = 40;
            this.checkBoxSU.Text = "su";
            this.checkBoxSU.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(767, 152);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 13);
            this.label14.TabIndex = 41;
            this.label14.Text = "00:00 - 24:00";
            // 
            // FormRuuvi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 527);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.checkBoxSU);
            this.Controls.Add(this.checkBoxSA);
            this.Controls.Add(this.checkBoxFR);
            this.Controls.Add(this.checkBoxTH);
            this.Controls.Add(this.checkBoxWE);
            this.Controls.Add(this.checkBoxTU);
            this.Controls.Add(this.checkBoxMO);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBoxTimeEnd);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBoxTimeStart);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.buttonAddRuuviTag);
            this.Controls.Add(this.buttonDeleteRule);
            this.Controls.Add(this.buttonAddRule);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxRuleName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.listBoxSensorRuleList);
            this.Controls.Add(this.listBoxCozifySensors);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxDoNotTouch);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxDeviceList);
            this.Controls.Add(this.comboBoxDeviceAction);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBoxTemperatureValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonDeleteSensor);
            this.Controls.Add(this.listBoxRuuviTagSensors);
            this.Controls.Add(this.buttonSaveRule);
            this.Controls.Add(this.textBoxAddRuuviSensor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxMqttBroker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label1);
            this.Name = "FormRuuvi";
            this.Text = "RuuviTag sensors";
            this.Load += new System.EventHandler(this.FormRuuvi_Load);
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
        private System.Windows.Forms.Button buttonSaveRule;
        private System.Windows.Forms.ListBox listBoxRuuviTagSensors;
        private System.Windows.Forms.Button buttonDeleteSensor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTemperatureValue;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxDeviceAction;
        private System.Windows.Forms.ComboBox comboBoxDeviceList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxDoNotTouch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listBoxCozifySensors;
        private System.Windows.Forms.ListBox listBoxSensorRuleList;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxRuleName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonAddRule;
        private System.Windows.Forms.Button buttonDeleteRule;
        private System.Windows.Forms.Button buttonAddRuuviTag;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxTimeStart;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxTimeEnd;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox checkBoxMO;
        private System.Windows.Forms.CheckBox checkBoxTU;
        private System.Windows.Forms.CheckBox checkBoxWE;
        private System.Windows.Forms.CheckBox checkBoxTH;
        private System.Windows.Forms.CheckBox checkBoxFR;
        private System.Windows.Forms.CheckBox checkBoxSA;
        private System.Windows.Forms.CheckBox checkBoxSU;
        private System.Windows.Forms.Label label14;
    }
}