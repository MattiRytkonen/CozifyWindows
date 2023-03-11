using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozifyWindows
{
    public partial class FormRuuvi : Form
    {
        public FormRuuvi()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Form1.saveSetting("mqtt_topics", textBoxRuuviSensors.Text);
            Form1.saveSetting("mqtt_broker", textBoxMqttBroker.Text);
            MessageBox.Show("OK");
        }

        private void timerInit_Tick(object sender, EventArgs e)
        {
            timerInit.Enabled = false;
            textBoxRuuviSensors.Text = Form1.readSetting("mqtt_topics");
            textBoxMqttBroker.Text= Form1.readSetting("mqtt_broker");
            if(string.IsNullOrWhiteSpace(textBoxMqttBroker.Text) == true)
            {
                textBoxMqttBroker.Text = "localhost";
                Form1.saveSetting("mqtt_broker", textBoxMqttBroker.Text);
            }
        }
    }
}
