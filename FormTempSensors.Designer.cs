namespace CozifyWindows
{
    partial class FormTempSensors
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
            this.listBoxAvailableTempSensors = new System.Windows.Forms.ListBox();
            this.listBoxSelectedTempSensors = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonAddSensor = new System.Windows.Forms.Button();
            this.buttonRemoveSensor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTemperatureLogSeconds = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Available temp sensors";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Log temperatures from these sensors";
            // 
            // listBoxAvailableTempSensors
            // 
            this.listBoxAvailableTempSensors.FormattingEnabled = true;
            this.listBoxAvailableTempSensors.Location = new System.Drawing.Point(13, 48);
            this.listBoxAvailableTempSensors.Name = "listBoxAvailableTempSensors";
            this.listBoxAvailableTempSensors.Size = new System.Drawing.Size(186, 95);
            this.listBoxAvailableTempSensors.TabIndex = 2;
            this.listBoxAvailableTempSensors.SelectedIndexChanged += new System.EventHandler(this.listBoxAvailableTempSensors_SelectedIndexChanged);
            // 
            // listBoxSelectedTempSensors
            // 
            this.listBoxSelectedTempSensors.FormattingEnabled = true;
            this.listBoxSelectedTempSensors.Location = new System.Drawing.Point(287, 48);
            this.listBoxSelectedTempSensors.Name = "listBoxSelectedTempSensors";
            this.listBoxSelectedTempSensors.Size = new System.Drawing.Size(186, 95);
            this.listBoxSelectedTempSensors.TabIndex = 5;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonAddSensor
            // 
            this.buttonAddSensor.Location = new System.Drawing.Point(206, 69);
            this.buttonAddSensor.Name = "buttonAddSensor";
            this.buttonAddSensor.Size = new System.Drawing.Size(75, 23);
            this.buttonAddSensor.TabIndex = 6;
            this.buttonAddSensor.Text = "->";
            this.buttonAddSensor.UseVisualStyleBackColor = true;
            this.buttonAddSensor.Click += new System.EventHandler(this.buttonAddSensor_Click);
            // 
            // buttonRemoveSensor
            // 
            this.buttonRemoveSensor.Location = new System.Drawing.Point(206, 98);
            this.buttonRemoveSensor.Name = "buttonRemoveSensor";
            this.buttonRemoveSensor.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveSensor.TabIndex = 7;
            this.buttonRemoveSensor.Text = "<-";
            this.buttonRemoveSensor.UseVisualStyleBackColor = true;
            this.buttonRemoveSensor.Click += new System.EventHandler(this.buttonRemoveSensor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Write log after every ";
            // 
            // textBoxTemperatureLogSeconds
            // 
            this.textBoxTemperatureLogSeconds.Location = new System.Drawing.Point(127, 173);
            this.textBoxTemperatureLogSeconds.MaxLength = 4;
            this.textBoxTemperatureLogSeconds.Name = "textBoxTemperatureLogSeconds";
            this.textBoxTemperatureLogSeconds.Size = new System.Drawing.Size(100, 20);
            this.textBoxTemperatureLogSeconds.TabIndex = 9;
            this.textBoxTemperatureLogSeconds.TextChanged += new System.EventHandler(this.textBoxTemperatureLogSeconds_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(233, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "seconds. 0 = disabled";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "label5";
            // 
            // FormTempSensors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 278);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxTemperatureLogSeconds);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonRemoveSensor);
            this.Controls.Add(this.buttonAddSensor);
            this.Controls.Add(this.listBoxSelectedTempSensors);
            this.Controls.Add(this.listBoxAvailableTempSensors);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormTempSensors";
            this.Text = "FormTempSensors";
            this.Load += new System.EventHandler(this.FormTempSensors_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxAvailableTempSensors;
        private System.Windows.Forms.ListBox listBoxSelectedTempSensors;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonAddSensor;
        private System.Windows.Forms.Button buttonRemoveSensor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTemperatureLogSeconds;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}