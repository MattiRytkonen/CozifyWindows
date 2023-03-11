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
            this.textBoxRuuviSensors = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.timerInit = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxMqttBroker = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxRuuviSensors
            // 
            this.textBoxRuuviSensors.Location = new System.Drawing.Point(12, 32);
            this.textBoxRuuviSensors.Multiline = true;
            this.textBoxRuuviSensors.Name = "textBoxRuuviSensors";
            this.textBoxRuuviSensors.Size = new System.Drawing.Size(268, 172);
            this.textBoxRuuviSensors.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "MQTT topics: one per line";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(12, 273);
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
            this.label2.Location = new System.Drawing.Point(13, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "MQTT server address:port";
            // 
            // textBoxMqttBroker
            // 
            this.textBoxMqttBroker.Location = new System.Drawing.Point(12, 238);
            this.textBoxMqttBroker.Name = "textBoxMqttBroker";
            this.textBoxMqttBroker.Size = new System.Drawing.Size(268, 20);
            this.textBoxMqttBroker.TabIndex = 4;
            // 
            // FormRuuvi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 308);
            this.Controls.Add(this.textBoxMqttBroker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxRuuviSensors);
            this.Name = "FormRuuvi";
            this.Text = "RuuviTag sensors";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxRuuviSensors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Timer timerInit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxMqttBroker;
    }
}