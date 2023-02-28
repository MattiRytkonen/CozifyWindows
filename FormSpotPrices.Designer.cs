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
            this.buttonRemoveSensor = new System.Windows.Forms.Button();
            this.buttonAddSensor = new System.Windows.Forms.Button();
            this.listBoxSelectedDevices = new System.Windows.Forms.ListBox();
            this.listBoxAvailableDevices = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSpotHours = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTimerSpotPrice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonRemoveSensor
            // 
            this.buttonRemoveSensor.Location = new System.Drawing.Point(205, 99);
            this.buttonRemoveSensor.Name = "buttonRemoveSensor";
            this.buttonRemoveSensor.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveSensor.TabIndex = 13;
            this.buttonRemoveSensor.Text = "<-";
            this.buttonRemoveSensor.UseVisualStyleBackColor = true;
            this.buttonRemoveSensor.Click += new System.EventHandler(this.buttonRemoveSensor_Click);
            // 
            // buttonAddSensor
            // 
            this.buttonAddSensor.Location = new System.Drawing.Point(205, 70);
            this.buttonAddSensor.Name = "buttonAddSensor";
            this.buttonAddSensor.Size = new System.Drawing.Size(75, 23);
            this.buttonAddSensor.TabIndex = 12;
            this.buttonAddSensor.Text = "->";
            this.buttonAddSensor.UseVisualStyleBackColor = true;
            this.buttonAddSensor.Click += new System.EventHandler(this.buttonAddSensor_Click);
            // 
            // listBoxSelectedDevices
            // 
            this.listBoxSelectedDevices.FormattingEnabled = true;
            this.listBoxSelectedDevices.Location = new System.Drawing.Point(286, 49);
            this.listBoxSelectedDevices.Name = "listBoxSelectedDevices";
            this.listBoxSelectedDevices.Size = new System.Drawing.Size(186, 95);
            this.listBoxSelectedDevices.TabIndex = 11;
            // 
            // listBoxAvailableDevices
            // 
            this.listBoxAvailableDevices.FormattingEnabled = true;
            this.listBoxAvailableDevices.Location = new System.Drawing.Point(12, 49);
            this.listBoxAvailableDevices.Name = "listBoxAvailableDevices";
            this.listBoxAvailableDevices.Size = new System.Drawing.Size(186, 95);
            this.listBoxAvailableDevices.TabIndex = 10;
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
            this.label3.Size = new System.Drawing.Size(200, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Keep device ON in cheapest hours: 0-24";
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
            this.label4.Location = new System.Drawing.Point(15, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Toggle devices on/off every ";
            // 
            // textBoxTimerSpotPrice
            // 
            this.textBoxTimerSpotPrice.Location = new System.Drawing.Point(236, 204);
            this.textBoxTimerSpotPrice.Name = "textBoxTimerSpotPrice";
            this.textBoxTimerSpotPrice.Size = new System.Drawing.Size(100, 20);
            this.textBoxTimerSpotPrice.TabIndex = 17;
            this.textBoxTimerSpotPrice.TextChanged += new System.EventHandler(this.textBoxTimerSpotPrice_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(353, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "seconds";
            // 
            // FormSpotPrices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxTimerSpotPrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxSpotHours);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonRemoveSensor);
            this.Controls.Add(this.buttonAddSensor);
            this.Controls.Add(this.listBoxSelectedDevices);
            this.Controls.Add(this.listBoxAvailableDevices);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormSpotPrices";
            this.Text = "FormSpotPrices";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRemoveSensor;
        private System.Windows.Forms.Button buttonAddSensor;
        private System.Windows.Forms.ListBox listBoxSelectedDevices;
        private System.Windows.Forms.ListBox listBoxAvailableDevices;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSpotHours;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTimerSpotPrice;
        private System.Windows.Forms.Label label5;
    }
}