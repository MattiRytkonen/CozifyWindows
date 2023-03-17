using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
            saveSensors();
            MessageBox.Show("OK");
        }

        private async void saveSensors()
        {
            var sensorsStringBuilder = new StringBuilder();
            foreach (string item in listBox1.Items)
            {
                sensorsStringBuilder.AppendLine(item);
            }
            Form1.saveSetting("mqtt_topics", sensorsStringBuilder.ToString());
            Form1.saveSetting("mqtt_broker", textBoxMqttBroker.Text);
        }

        private void timerInit_Tick(object sender, EventArgs e)
        {
            timerInit.Enabled = false;
            var sensors_string = Form1.readSetting("mqtt_topics");

            var sensors = sensors_string.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var sensor in sensors)
            {
                listBox1.Items.Add(sensor);
            }
            textBoxMqttBroker.Text = Form1.readSetting("mqtt_broker");
            if (string.IsNullOrWhiteSpace(textBoxMqttBroker.Text) == true)
            {
                textBoxMqttBroker.Text = "localhost";
                Form1.saveSetting("mqtt_broker", textBoxMqttBroker.Text);
            }
        }

        private void buttonDeleteSensor_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                return;
            }
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            saveSensors();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                comboBox1.SelectedIndex = 0;
            }
            if (listBox1.SelectedIndex == -1)
            {
                return;
            }
            textBoxAddRuuviSensor.Text = listBox1.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(textBoxAddRuuviSensor.Text) == true)
            {
                return;
            }

            var value1 = Form1.readSetting("ruuvitag" + textBoxAddRuuviSensor.Text + "value1");
            if (value1 == "<" || value1 == ">")
            {
                comboBox1.SelectedItem = value1;
            }

            textBox1.Text = Form1.readSetting("ruuvitag" + textBoxAddRuuviSensor.Text + "temperature");
            comboBoxDeviceList.Items.Clear();
            foreach (var dev in Form1.deviceList)
            {
                if (dev.type == "POWER_SOCKET")
                {
                    comboBoxDeviceList.Items.Add(dev.name);
                }
            }

            var selectedDevice = Form1.readSetting("ruuvitag" + textBoxAddRuuviSensor.Text + "device");
            var device = (from c1 in Form1.deviceList where c1.id == selectedDevice select c1).FirstOrDefault();
            if (device != null)
            {
                try
                {
                    comboBoxDeviceList.SelectedItem = device.name;
                }
                catch { }
                finally { }
            }
            textBoxDoNotTouch.Text = Form1.readSetting("ruuvitag" + textBoxAddRuuviSensor.Text + "donottouch");

            comboBoxDeviceAction.SelectedItem = "ON";
            try
            {
                comboBoxDeviceAction.SelectedItem=            Form1.readSetting("ruuvitag" + textBoxAddRuuviSensor.Text + "deviceaction");
            }
            catch { }
            finally { }
        }

        private void buttonAddRuuviSensor_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddRuuviSensor.Text) == true)
            {
                return;
            }
            string removable = "";
            foreach (string item in listBox1.Items)
            {
                if (item == textBoxAddRuuviSensor.Text)
                {
                    removable = item;
                }
            }

            if (string.IsNullOrWhiteSpace(removable) == false)
            {
                listBox1.Items.Remove(removable);
            }

            listBox1.Items.Add(textBoxAddRuuviSensor.Text);

            Form1.saveSetting("ruuvitag" + textBoxAddRuuviSensor.Text + "value1", comboBox1.SelectedItem.ToString());

            textBox1.Text = textBox1.Text.Replace(",", ".");

            double.TryParse(textBox1.Text, out double temperature);
            textBox1.Text = temperature.ToString();
            Form1.saveSetting("ruuvitag" + textBoxAddRuuviSensor.Text + "temperature", textBox1.Text);
            if (comboBoxDeviceList.SelectedIndex != -1)
            {
                var query = (from c1 in Form1.deviceList where c1.name == comboBoxDeviceList.SelectedItem.ToString() select c1).FirstOrDefault();
                if (query != null) {
                    Form1.saveSetting("ruuvitag" + textBoxAddRuuviSensor.Text + "device", query.id);
                }
                
            }

            int.TryParse(textBoxDoNotTouch.Text, out int doNotTouchSeconds);
            textBoxDoNotTouch.Text = doNotTouchSeconds.ToString();
            Form1.saveSetting("ruuvitag" + textBoxAddRuuviSensor.Text + "donottouch", textBoxDoNotTouch.Text);

         Form1.saveSetting("ruuvitag" + textBoxAddRuuviSensor.Text + "deviceaction", comboBoxDeviceAction.SelectedItem.ToString());

            saveSensors();
        }
    }
}
