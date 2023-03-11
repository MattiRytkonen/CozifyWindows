namespace CozifyWindows
{
    partial class Form1
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonTemporaryPassword = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonHubKeys = new System.Windows.Forms.Button();
            this.buttonRefreshToken = new System.Windows.Forms.Button();
            this.buttonGetDevices = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonGetApiVersion = new System.Windows.Forms.Button();
            this.buttonGetLanIp = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDeviceControlId = new System.Windows.Forms.TextBox();
            this.buttonDeviceOn = new System.Windows.Forms.Button();
            this.buttonDeviceOff = new System.Windows.Forms.Button();
            this.buttonTempSensors = new System.Windows.Forms.Button();
            this.timerTemperatureLogging = new System.Windows.Forms.Timer(this.components);
            this.buttonElectricityPrices = new System.Windows.Forms.Button();
            this.timerSpotPrices = new System.Windows.Forms.Timer(this.components);
            this.buttonSpotPriceControl = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonStartMqttServer = new System.Windows.Forms.Button();
            this.timerLogging = new System.Windows.Forms.Timer(this.components);
            this.buttonStartMqttClient = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "email";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "temporary password";
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(121, 10);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(295, 20);
            this.textBoxEmail.TabIndex = 10;
            this.textBoxEmail.Leave += new System.EventHandler(this.textBoxEmail_Leave_1);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(121, 35);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(295, 20);
            this.textBoxPassword.TabIndex = 30;
            this.textBoxPassword.Leave += new System.EventHandler(this.textBoxPassword_Leave);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(422, 33);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 23);
            this.buttonLogin.TabIndex = 40;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonTemporaryPassword
            // 
            this.buttonTemporaryPassword.Location = new System.Drawing.Point(503, 8);
            this.buttonTemporaryPassword.Name = "buttonTemporaryPassword";
            this.buttonTemporaryPassword.Size = new System.Drawing.Size(141, 23);
            this.buttonTemporaryPassword.TabIndex = 20;
            this.buttonTemporaryPassword.Text = "Order temporary password";
            this.buttonTemporaryPassword.UseVisualStyleBackColor = true;
            this.buttonTemporaryPassword.Click += new System.EventHandler(this.buttonTemporaryPassword_Click);
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(457, 80);
            this.textBoxLog.MaxLength = 0;
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(331, 364);
            this.textBoxLog.TabIndex = 5;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonHubKeys
            // 
            this.buttonHubKeys.Location = new System.Drawing.Point(285, 80);
            this.buttonHubKeys.Name = "buttonHubKeys";
            this.buttonHubKeys.Size = new System.Drawing.Size(98, 23);
            this.buttonHubKeys.TabIndex = 90;
            this.buttonHubKeys.Text = "Get hub keys";
            this.buttonHubKeys.UseVisualStyleBackColor = true;
            this.buttonHubKeys.Click += new System.EventHandler(this.buttonHubKeys_Click);
            // 
            // buttonRefreshToken
            // 
            this.buttonRefreshToken.Location = new System.Drawing.Point(503, 33);
            this.buttonRefreshToken.Name = "buttonRefreshToken";
            this.buttonRefreshToken.Size = new System.Drawing.Size(96, 23);
            this.buttonRefreshToken.TabIndex = 50;
            this.buttonRefreshToken.Text = "Refresh Token";
            this.buttonRefreshToken.UseVisualStyleBackColor = true;
            this.buttonRefreshToken.Click += new System.EventHandler(this.buttonRefreshToken_Click);
            // 
            // buttonGetDevices
            // 
            this.buttonGetDevices.Location = new System.Drawing.Point(13, 80);
            this.buttonGetDevices.Name = "buttonGetDevices";
            this.buttonGetDevices.Size = new System.Drawing.Size(75, 23);
            this.buttonGetDevices.TabIndex = 60;
            this.buttonGetDevices.Text = "Get devices";
            this.buttonGetDevices.UseVisualStyleBackColor = true;
            this.buttonGetDevices.Click += new System.EventHandler(this.buttonGetDevices_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 343);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.TabIndex = 170;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 327);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Select hub";
            // 
            // buttonGetApiVersion
            // 
            this.buttonGetApiVersion.Location = new System.Drawing.Point(95, 79);
            this.buttonGetApiVersion.Name = "buttonGetApiVersion";
            this.buttonGetApiVersion.Size = new System.Drawing.Size(103, 23);
            this.buttonGetApiVersion.TabIndex = 70;
            this.buttonGetApiVersion.Text = "Get Api version";
            this.buttonGetApiVersion.UseVisualStyleBackColor = true;
            this.buttonGetApiVersion.Click += new System.EventHandler(this.buttonGetApiVersion_Click);
            // 
            // buttonGetLanIp
            // 
            this.buttonGetLanIp.Location = new System.Drawing.Point(204, 80);
            this.buttonGetLanIp.Name = "buttonGetLanIp";
            this.buttonGetLanIp.Size = new System.Drawing.Size(75, 23);
            this.buttonGetLanIp.TabIndex = 80;
            this.buttonGetLanIp.Text = "Get LAN IP";
            this.buttonGetLanIp.UseVisualStyleBackColor = true;
            this.buttonGetLanIp.Click += new System.EventHandler(this.buttonGetLanIp_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "device id:";
            // 
            // textBoxDeviceControlId
            // 
            this.textBoxDeviceControlId.Location = new System.Drawing.Point(72, 125);
            this.textBoxDeviceControlId.Name = "textBoxDeviceControlId";
            this.textBoxDeviceControlId.Size = new System.Drawing.Size(100, 20);
            this.textBoxDeviceControlId.TabIndex = 100;
            this.textBoxDeviceControlId.Leave += new System.EventHandler(this.textBoxDeviceControlId_Leave);
            // 
            // buttonDeviceOn
            // 
            this.buttonDeviceOn.Location = new System.Drawing.Point(178, 123);
            this.buttonDeviceOn.Name = "buttonDeviceOn";
            this.buttonDeviceOn.Size = new System.Drawing.Size(75, 23);
            this.buttonDeviceOn.TabIndex = 110;
            this.buttonDeviceOn.Text = "ON";
            this.buttonDeviceOn.UseVisualStyleBackColor = true;
            this.buttonDeviceOn.Click += new System.EventHandler(this.buttonDeviceOn_Click);
            // 
            // buttonDeviceOff
            // 
            this.buttonDeviceOff.Location = new System.Drawing.Point(259, 122);
            this.buttonDeviceOff.Name = "buttonDeviceOff";
            this.buttonDeviceOff.Size = new System.Drawing.Size(75, 23);
            this.buttonDeviceOff.TabIndex = 120;
            this.buttonDeviceOff.Text = "OFF";
            this.buttonDeviceOff.UseVisualStyleBackColor = true;
            this.buttonDeviceOff.Click += new System.EventHandler(this.buttonDeviceOff_Click);
            // 
            // buttonTempSensors
            // 
            this.buttonTempSensors.Location = new System.Drawing.Point(13, 210);
            this.buttonTempSensors.Name = "buttonTempSensors";
            this.buttonTempSensors.Size = new System.Drawing.Size(107, 23);
            this.buttonTempSensors.TabIndex = 140;
            this.buttonTempSensors.Text = "Temp sensors";
            this.buttonTempSensors.UseVisualStyleBackColor = true;
            this.buttonTempSensors.Click += new System.EventHandler(this.buttonTempSensors_Click);
            // 
            // timerTemperatureLogging
            // 
            this.timerTemperatureLogging.Enabled = true;
            this.timerTemperatureLogging.Interval = 1000;
            this.timerTemperatureLogging.Tick += new System.EventHandler(this.timerTemperatureLogging_Tick);
            // 
            // buttonElectricityPrices
            // 
            this.buttonElectricityPrices.Location = new System.Drawing.Point(13, 168);
            this.buttonElectricityPrices.Name = "buttonElectricityPrices";
            this.buttonElectricityPrices.Size = new System.Drawing.Size(101, 23);
            this.buttonElectricityPrices.TabIndex = 130;
            this.buttonElectricityPrices.Text = "Get Spot Prices";
            this.buttonElectricityPrices.UseVisualStyleBackColor = true;
            this.buttonElectricityPrices.Click += new System.EventHandler(this.buttonElectricityPrices_Click);
            // 
            // timerSpotPrices
            // 
            this.timerSpotPrices.Enabled = true;
            this.timerSpotPrices.Interval = 5000;
            this.timerSpotPrices.Tick += new System.EventHandler(this.timerSpotPrices_Tick);
            // 
            // buttonSpotPriceControl
            // 
            this.buttonSpotPriceControl.Location = new System.Drawing.Point(129, 210);
            this.buttonSpotPriceControl.Name = "buttonSpotPriceControl";
            this.buttonSpotPriceControl.Size = new System.Drawing.Size(192, 23);
            this.buttonSpotPriceControl.TabIndex = 150;
            this.buttonSpotPriceControl.Text = "Spot price controlled power outlets";
            this.buttonSpotPriceControl.UseVisualStyleBackColor = true;
            this.buttonSpotPriceControl.Click += new System.EventHandler(this.buttonSpotPriceControl_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Automatic",
            "Local",
            "API"});
            this.comboBox1.Location = new System.Drawing.Point(13, 267);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 160;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 248);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "ip address:";
            // 
            // buttonStartMqttServer
            // 
            this.buttonStartMqttServer.Location = new System.Drawing.Point(285, 294);
            this.buttonStartMqttServer.Name = "buttonStartMqttServer";
            this.buttonStartMqttServer.Size = new System.Drawing.Size(116, 23);
            this.buttonStartMqttServer.TabIndex = 171;
            this.buttonStartMqttServer.Text = "Start MQTT Server";
            this.buttonStartMqttServer.UseVisualStyleBackColor = true;
            this.buttonStartMqttServer.Click += new System.EventHandler(this.buttonStartMqttServer_Click);
            // 
            // timerLogging
            // 
            this.timerLogging.Interval = 700;
            this.timerLogging.Tick += new System.EventHandler(this.timerLogging_Tick);
            // 
            // buttonStartMqttClient
            // 
            this.buttonStartMqttClient.Location = new System.Drawing.Point(285, 265);
            this.buttonStartMqttClient.Name = "buttonStartMqttClient";
            this.buttonStartMqttClient.Size = new System.Drawing.Size(116, 23);
            this.buttonStartMqttClient.TabIndex = 172;
            this.buttonStartMqttClient.Text = "Start MQTT Client";
            this.buttonStartMqttClient.UseVisualStyleBackColor = true;
            this.buttonStartMqttClient.Click += new System.EventHandler(this.buttonStartMqttClient_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(327, 210);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 23);
            this.button3.TabIndex = 175;
            this.button3.Text = "RuuviTag Sensors";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 461);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonStartMqttClient);
            this.Controls.Add(this.buttonStartMqttServer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.buttonSpotPriceControl);
            this.Controls.Add(this.buttonElectricityPrices);
            this.Controls.Add(this.buttonTempSensors);
            this.Controls.Add(this.buttonDeviceOff);
            this.Controls.Add(this.buttonDeviceOn);
            this.Controls.Add(this.textBoxDeviceControlId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonGetLanIp);
            this.Controls.Add(this.buttonGetApiVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonGetDevices);
            this.Controls.Add(this.buttonRefreshToken);
            this.Controls.Add(this.buttonHubKeys);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.buttonTemporaryPassword);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "CozifyWindows 1.0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonTemporaryPassword;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonHubKeys;
        private System.Windows.Forms.Button buttonRefreshToken;
        private System.Windows.Forms.Button buttonGetDevices;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonGetApiVersion;
        private System.Windows.Forms.Button buttonGetLanIp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDeviceControlId;
        private System.Windows.Forms.Button buttonDeviceOn;
        private System.Windows.Forms.Button buttonDeviceOff;
        private System.Windows.Forms.Button buttonTempSensors;
        private System.Windows.Forms.Timer timerTemperatureLogging;
        private System.Windows.Forms.Button buttonElectricityPrices;
        private System.Windows.Forms.Timer timerSpotPrices;
        private System.Windows.Forms.Button buttonSpotPriceControl;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonStartMqttServer;
        private System.Windows.Forms.Timer timerLogging;
        private System.Windows.Forms.Button buttonStartMqttClient;
        private System.Windows.Forms.Button button3;
    }
}

