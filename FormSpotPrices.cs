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
                        comboBoxDeviceList.Items.Add(device.name);
                        comboBoxDeviceList2.Items.Add(device.name);
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



            string deviceaction = "";
            if (comboBoxDeviceAction.SelectedIndex != -1)
            {
                deviceaction = comboBoxDeviceAction.SelectedItem.ToString();
            }
            string selecteddevice = "";
            if (comboBoxDeviceList.SelectedIndex != -1)
            {
                var q = (from c1 in Form1.deviceList where c1.name == comboBoxDeviceList.SelectedItem.ToString() select c1).FirstOrDefault();
                if (q != null)
                {
                    selecteddevice = q.id;
                }
            }
            string selecteddeviceAction = "";
            if (comboBox1.SelectedIndex != -1)
            {
                selecteddeviceAction = comboBox1.SelectedItem.ToString();
            }





            string deviceaction2 = "";
            if (comboBoxDeviceAction2.SelectedIndex != -1)
            {
                deviceaction2 = comboBoxDeviceAction2.SelectedItem.ToString();
            }
            string selecteddevice2 = "";
            if (comboBoxDeviceList2.SelectedIndex != -1)
            {
                var q = (from c1 in Form1.deviceList where c1.name == comboBoxDeviceList2.SelectedItem.ToString() select c1).FirstOrDefault();
                if (q != null)
                {
                    selecteddevice2 = q.id;
                }
            }
            string selecteddeviceAction2 = "";
            if (comboBox12.SelectedIndex != -1)
            {
                selecteddeviceAction2 = comboBox12.SelectedItem.ToString();
            }

            var newrow = device_id
                + dataSeparator
                + textBoxSpotHours.Text
                + dataSeparator
                + textBoxMaxPrice.Text
                + dataSeparator
                + textBoxTimerSpotPrice.Text
                + dataSeparator
                + deviceaction
                + dataSeparator
                + selecteddevice
                + dataSeparator
                + selecteddeviceAction
                + dataSeparator
                + deviceaction2
                + dataSeparator
                + selecteddevice2
                + dataSeparator
                + selecteddeviceAction2
                + dataSeparator
                + textBoxTelldusDeviceId.Text
                ;



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

            comboBoxDeviceAction.SelectedIndex = -1;
            comboBoxDeviceList.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;

            comboBoxDeviceAction2.SelectedIndex = -1;
            comboBoxDeviceList2.SelectedIndex = -1;
            comboBox12.SelectedIndex = -1;


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



                if (devicename == listBoxAvailableDevices.SelectedItem.ToString())
                {
                    textBoxSpotHours.Text = cheapesthours;
                    textBoxMaxPrice.Text = maxprice;
                    try
                    {
                        textBoxTimerSpotPrice.Text = data[3];
                    }
                    catch { }
                    finally { }

                    try
                    {
                        comboBoxDeviceAction.SelectedItem = data[4];
                    }
                    catch { }
                    finally { }

                    try
                    {
                        var q = (from c1 in Form1.deviceList where c1.id == data[5] select c1).FirstOrDefault();
                        comboBoxDeviceList.SelectedItem = q.name;
                    }
                    catch { }
                    finally { }

                    try
                    {
                        comboBox1.SelectedItem = data[6];
                    }
                    catch { }
                    finally { }

                    try
                    {
                        comboBoxDeviceAction2.SelectedItem = data[7];
                    }
                    catch { }
                    finally { }

                    try
                    {
                        var q = (from c1 in Form1.deviceList where c1.id == data[8] select c1).FirstOrDefault();
                        comboBoxDeviceList2.SelectedItem = q.name;
                    }
                    catch { }
                    finally { }

                    try
                    {
                        comboBox12.SelectedItem = data[9];
                    }
                    catch { }
                    finally { }

                    try
                    {
                        textBoxTelldusDeviceId.Text = data[10];
                    }
                    catch { }
                    finally { }
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
