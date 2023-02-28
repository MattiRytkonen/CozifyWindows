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
    public partial class FormSpotPrices : Form
    {
        public FormSpotPrices()
        {
            InitializeComponent();
        }

        private void buttonAddSensor_Click(object sender, EventArgs e)
        {
            string selected_item = listBoxAvailableDevices .SelectedItem.ToString();
            bool found = false;
            foreach (string item in  listBoxSelectedDevices .Items)
            {
                if (item == selected_item)
                {
                    found = true;
                }
            }
            if (found == false)
            {
                listBoxSelectedDevices.Items.Add(selected_item);
            }
            saveSelectedDevices();
        }

        private void buttonRemoveSensor_Click(object sender, EventArgs e)
        {
            if (listBoxSelectedDevices.SelectedIndex == -1)
            {
                return;
            }

            listBoxSelectedDevices.Items.RemoveAt(listBoxSelectedDevices.SelectedIndex);


            saveSelectedDevices();
        }

        private void saveSelectedDevices()
        {
            string selected_spot_devices = "";
            foreach (string item in listBoxSelectedDevices.Items)
            {
                selected_spot_devices += item + "§";
            }
            Properties.Settings.Default.selected_spot_devices = selected_spot_devices;
            Properties.Settings.Default.Save();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
           textBoxTimerSpotPrice.Text = Properties.Settings.Default.timer_spot_price_seconds.ToString();

            textBoxSpotHours.Text = Properties.Settings.Default.spot_hours.ToString();

            var selected_spot_devices = Properties.Settings.Default.selected_spot_devices.Split('§');
            if (Form1.deviceList != null)
            {
                foreach (var device in Form1.deviceList)
                {
                    if (device.type== "POWER_SOCKET")
                    {
                        listBoxAvailableDevices  .Items.Add(device.name);
                        foreach (var row in selected_spot_devices)
                        {
                            if (string.IsNullOrWhiteSpace(row))
                            {
                                continue;
                            }
                            if (row == device.name)
                            {
                                listBoxSelectedDevices.Items.Add(device.name);
                            }
                        }
                    }
                }
            }
        }

        private void textBoxSpotHours_TextChanged(object sender, EventArgs e)
        {
            if(int.TryParse(textBoxSpotHours.Text,out int spothours))
            {
                Properties.Settings.Default.spot_hours = spothours;
                Properties.Settings.Default.Save();
            }                      
        }

      
        private void textBoxTimerSpotPrice_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxTimerSpotPrice.Text, out int number))
            {
                Properties.Settings.Default.timer_spot_price_seconds = number;
                Properties.Settings.Default.Save();
            }
        }
    }
}
