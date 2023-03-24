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

            var devicename = listBoxAvailableDevices.SelectedItem.ToString();

            var device_id = (from c1 in Form1.deviceList where c1.name == devicename select c1.id).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(device_id) == true)
            {
                device_id = "";
            }

            var sb = new StringBuilder();

            var spot_price_controlled_devices = Form1.readSetting("spot_price_controlled_devices");

            foreach (var row in spot_price_controlled_devices.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (row.StartsWith(device_id + dataSeparator) == false)
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

            int.TryParse(textBoxTimerSpotPrice.Text, out int TimerSpotPrice);
            if (TimerSpotPrice == 0)
            {
                TimerSpotPrice = 60;
            }
            textBoxTimerSpotPrice.Text = TimerSpotPrice.ToString();

            var newrow = device_id + dataSeparator + textBoxSpotHours.Text + dataSeparator + textBoxMaxPrice.Text + dataSeparator + textBoxTimerSpotPrice.Text;
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
            textBoxTimerSpotPrice.Text = "";

            foreach (var row in spot_price_controlled_devices.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var data = row.Split(new string[] { dataSeparator }, StringSplitOptions.None);
                var device_id = data[0];
                var devicename = (from c1 in Form1.deviceList where c1.id == device_id select c1.name).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(devicename))
                {
                    devicename = "";
                }
                var cheapesthours = data[1];
                var maxprice = data[2];
                string do_not_touch_timer = "";
                try
                {
                    do_not_touch_timer = data[3];
                }
                catch
                {
                }
                finally { }

                if (devicename == listBoxAvailableDevices.SelectedItem.ToString())
                {
                    textBoxSpotHours.Text = cheapesthours;
                    textBoxMaxPrice.Text = maxprice;
                    textBoxTimerSpotPrice.Text = do_not_touch_timer;
                }
            }
        }

        private void textBoxMaxPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBoxAvailableDevices_Action()
        {

        }

        private void listBoxAvailableDevices_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void listBoxAvailableDevices_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
