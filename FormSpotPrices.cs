using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CozifyWindows
{
    public partial class FormSpotPrices : Form
    {
        const string dataSeparator = "§'.'§";
        public FormSpotPrices()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            var timer_spot_price_seconds_string = Form1.readSetting("timer_spot_price_seconds");

            textBoxTimerSpotPrice.Text = timer_spot_price_seconds_string;

            if (Form1.deviceList != null)
            {
                foreach (var device in Form1.deviceList)
                {
                    if (device.type == "POWER_SOCKET")
                    {
                        listBoxAvailableDevices.Items.Add(device.name);
                    }
                }
            }
        }

        private void textBoxSpotHours_TextChanged(object sender, EventArgs e)
        {

        }


        private void textBoxTimerSpotPrice_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxTimerSpotPrice.Text, out int number))
            {
                Form1.saveSetting("timer_spot_price_seconds", number.ToString());
            }
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            if (listBoxAvailableDevices.SelectedIndex == -1)
            {
                return;
            }

            var devicename = listBoxAvailableDevices.SelectedItem.ToString().Replace(dataSeparator, "").Replace(Environment.NewLine, "");

            var sb = new StringBuilder();

            var spot_price_controlled_devices = Form1.readSetting("spot_price_controlled_devices");

            foreach (var row in spot_price_controlled_devices.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (row.StartsWith(listBoxAvailableDevices.SelectedItem.ToString() + dataSeparator) == false)
                {
                    sb.AppendLine(row);
                }
            }

            if (string.IsNullOrWhiteSpace(textBoxSpotHours.Text) == false)
            {
                int.TryParse(textBoxSpotHours.Text, out int cheapesthours);
                textBoxSpotHours.Text = cheapesthours.ToString();
            }
            else
            {
                textBoxSpotHours.Text = "";
            }

            if (string.IsNullOrWhiteSpace(textBoxMaxPrice.Text) == false)
            {
                textBoxMaxPrice.Text = textBoxMaxPrice.Text.Replace(",", ".");
                double.TryParse(textBoxMaxPrice.Text, out double maxprice);
                textBoxMaxPrice.Text = maxprice.ToString();
            }
            else
            {
                textBoxMaxPrice.Text = "";
            }

            var newrow = devicename + dataSeparator + textBoxSpotHours.Text + dataSeparator + textBoxMaxPrice.Text;
            sb.AppendLine(newrow);
            Form1.saveSetting("spot_price_controlled_devices", sb.ToString());
        }

        private void listBoxAvailableDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            var spot_price_controlled_devices = Form1.readSetting("spot_price_controlled_devices");

            if (string.IsNullOrWhiteSpace(spot_price_controlled_devices))
            {
                return;
            }
            textBoxSpotHours.Text = "";
            textBoxMaxPrice.Text = "";

            foreach (var row in spot_price_controlled_devices.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var data = row.Split(new string[] { dataSeparator }, StringSplitOptions.None);
                var devicename = data[0];
                var cheapesthours = data[1];
                var maxprice = data[2];


                if (devicename == listBoxAvailableDevices.SelectedItem.ToString())
                {
                    textBoxSpotHours.Text = cheapesthours;
                    textBoxMaxPrice.Text = maxprice;
                }
            }
        }

        private void textBoxMaxPrice_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
