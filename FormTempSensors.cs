using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozifyWindows
{
    public partial class FormTempSensors : Form
    {
        public FormTempSensors()
        {
            InitializeComponent();
        }

        private void FormTempSensors_Load(object sender, EventArgs e)
        {

        }


        private void listBoxAvailableTempSensors_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            label5.Text = "Log file: " + Form1.temperatureLogFile;
            var selected_temp_sensors = Form1.readSetting("selected_temp_sensors");
            var selected_temp_sensors_list = selected_temp_sensors.Split('§');

            textBoxTemperatureLogSeconds.Text = Form1.readSetting("temperature_log_seconds");

            if (Form1.deviceList != null)
            {
                foreach (var device in Form1.deviceList)
                {
                    if (device.temperature.HasValue)
                    {
                        listBoxAvailableTempSensors.Items.Add(device.name);
                        foreach (var row in selected_temp_sensors_list)
                        {
                            if (string.IsNullOrWhiteSpace(row))
                            {
                                continue;
                            }
                            if (row == device.id)
                            {
                                listBoxSelectedTempSensors.Items.Add(device.name);
                            }
                        }
                    }
                }
            }
        }

        private void saveSelectedTempSensors()
        {
            string selected_temp_sensors = "";
            foreach (string item in listBoxSelectedTempSensors.Items)
            {
                var query = (from c in Form1.deviceList where c.name == item select c).FirstOrDefault();
                if (query != null)
                {
                    selected_temp_sensors += query.id + "§";
                }

            }
            Form1.saveSetting("selected_temp_sensors", selected_temp_sensors);
        }

        private void buttonAddSensor_Click(object sender, EventArgs e)
        {
            string selected_item = listBoxAvailableTempSensors.SelectedItem.ToString();
            bool found = false;
            foreach (string item in listBoxSelectedTempSensors.Items)
            {
                if (item == selected_item)
                {
                    found = true;
                }
            }
            if (found == false)
            {
                listBoxSelectedTempSensors.Items.Add(selected_item);
            }

            saveSelectedTempSensors();
        }

        private void buttonRemoveSensor_Click(object sender, EventArgs e)
        {
            if (listBoxSelectedTempSensors.SelectedIndex == -1)
            {
                return;
            }

            listBoxSelectedTempSensors.Items.RemoveAt(listBoxSelectedTempSensors.SelectedIndex);

            saveSelectedTempSensors();
        }

        private void textBoxTemperatureLogSeconds_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxTemperatureLogSeconds.Text, out int seconds) == true)
            {
                Form1.saveSetting("temperature_log_seconds", seconds.ToString());
                Form1.temperature_log_seconds = seconds;
            }
            textBoxTemperatureLogSeconds.Text = Form1.readSetting("temperature_log_seconds");
        }
    }
}
