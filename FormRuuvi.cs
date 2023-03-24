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

        private void saveSensors()
        {
            var sensorsStringBuilder = new StringBuilder();
            foreach (string item in listBoxRuuviTagSensors.Items)
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
                listBoxRuuviTagSensors.Items.Add(sensor);
            }
            textBoxMqttBroker.Text = Form1.readSetting("mqtt_broker");
            if (string.IsNullOrWhiteSpace(textBoxMqttBroker.Text) == true)
            {
                textBoxMqttBroker.Text = "localhost";
                Form1.saveSetting("mqtt_broker", textBoxMqttBroker.Text);
            }

            if (Form1.deviceList != null)
            {
                foreach (var device in Form1.deviceList)
                {
                    if (device.temperature.HasValue)
                    {
                        listBoxCozifySensors.Items.Add(device.name);
                    }
                }
            }
        }

        private void buttonDeleteSensor_Click(object sender, EventArgs e)
        {
            if (listBoxRuuviTagSensors.SelectedIndex == -1)
            {
                return;
            }
            listBoxRuuviTagSensors.Items.RemoveAt(listBoxRuuviTagSensors.SelectedIndex);
            saveSensors();
        }
        private void listBoxRuuviTagSensosors_Action()
        {
            textBoxAddRuuviSensor.Text = listBoxRuuviTagSensors.SelectedItem.ToString();
            listBoxCozifySensors.SelectedIndex = -1;
            var settingname = createSettingName();
            settingname += "RULES";
            var rules = Form1.readSetting(settingname);
            var rows = rules.Split(new string[] { Form1.dataSeparator }, StringSplitOptions.RemoveEmptyEntries);
            listBoxSensorRuleList.Items.Clear();
            foreach (string row in rows)
            {
                listBoxSensorRuleList.Items.Add(row);
            }
        }
        private void listBoxRuuviTagSensors_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonSaveRule_Click(object sender, EventArgs e)
        {
            if (listBoxSensorRuleList.SelectedIndex == -1)
            {
                return;
            }

            var settingname = createSettingName();
            settingname += listBoxSensorRuleList.SelectedItem.ToString();
            settingname += "_";

            //delete old settings
            deleteSettingsForRule(settingname);

            try
            {
                listBoxSensorRuleList.Items.Remove(listBoxSensorRuleList.SelectedItem);
            }
            catch { }
            finally { }

            settingname = createSettingName();

            //handle rule name change
            settingname += textBoxRuleName.Text;
            listBoxSensorRuleList.Items.Add(textBoxRuleName.Text);
            settingname += "_";


            Form1.saveSetting(settingname + "value1", comboBox1.SelectedItem.ToString());

            textBoxTemperatureValue.Text = textBoxTemperatureValue.Text.Replace(",", ".");

            double.TryParse(textBoxTemperatureValue.Text, out double temperature);
            textBoxTemperatureValue.Text = temperature.ToString();
            Form1.saveSetting(settingname + "temperature", textBoxTemperatureValue.Text);
            if (comboBoxDeviceList.SelectedIndex != -1)
            {
                var query = (from c1 in Form1.deviceList where c1.name == comboBoxDeviceList.SelectedItem.ToString() select c1).FirstOrDefault();
                if (query != null)
                {
                    Form1.saveSetting(settingname + "device", query.id);
                }
            }

            int.TryParse(textBoxDoNotTouch.Text, out int doNotTouchSeconds);
            textBoxDoNotTouch.Text = doNotTouchSeconds.ToString();
            Form1.saveSetting(settingname + "donottouch", textBoxDoNotTouch.Text);

            Form1.saveSetting(settingname + "deviceaction", comboBoxDeviceAction.SelectedItem.ToString());


            if (string.IsNullOrWhiteSpace(textBoxTimeStart.Text) == true)
            {
                textBoxTimeStart.Text = "0:0";
            }

            if (textBoxTimeStart.Text.Contains(":") == false)
            {
                textBoxTimeStart.Text = "0:0";
            }

            var timestart_data = textBoxTimeStart.Text.Split(':');
            int.TryParse(timestart_data[0], out int hour_start);
            if (hour_start < 0)
            {
                hour_start = 0;
            }
            if (hour_start > 24)
            {
                hour_start = 24;
            }

            int.TryParse(timestart_data[1], out int minute_start);
            if (minute_start < 0)
            {
                minute_start = 0;
            }
            if (minute_start > 59)
            {
                minute_start = 59;
            }

            textBoxTimeStart.Text = hour_start.ToString("00") + ":" + minute_start.ToString("00");
            Form1.saveSetting(settingname + "timestart", textBoxTimeStart.Text);




            if (string.IsNullOrWhiteSpace(textBoxTimeEnd.Text) == true)
            {
                textBoxTimeEnd.Text = "0:0";
            }

            if (textBoxTimeStart.Text.Contains(":") == false)
            {
                textBoxTimeEnd.Text = "0:0";
            }

            var timeend_data = textBoxTimeEnd.Text.Split(':');
            int.TryParse(timeend_data[0], out int hour_end);
            if (hour_end < 0)
            {
                hour_end = 0;
            }
            if (hour_end > 24)
            {
                hour_end = 24;
            }

            int.TryParse(timeend_data[1], out int minute_end);
            if (minute_end < 0)
            {
                minute_end = 0;
            }
            if (minute_end > 59)
            {
                minute_end = 59;
            }

            textBoxTimeEnd.Text = hour_end.ToString("00") + ":" + minute_end.ToString("00");
            Form1.saveSetting(settingname + "timeend", textBoxTimeEnd.Text);

            string MO = "";
            if (checkBoxMO.Checked)
            {
                MO = "1";
            }
            string TU = "";
            if (checkBoxTU.Checked)
            {
                TU = "1";
            }
            string WE = "";
            if (checkBoxWE.Checked)
            {
                WE = "1";
            }
            string TH = "";
            if (checkBoxTH.Checked)
            {
                TH = "1";
            }
            string FR = "";
            if (checkBoxFR.Checked)
            {
                FR = "1";
            }
            string SA = "";
            if (checkBoxSA.Checked)
            {
                SA = "1";
            }
            string SU = "";
            if (checkBoxSU.Checked)
            {
                SU = "1";
            }


            Form1.saveSetting(settingname + "MO", MO);
            Form1.saveSetting(settingname + "TU", TU);
            Form1.saveSetting(settingname + "WE", WE);
            Form1.saveSetting(settingname + "TH", TH);
            Form1.saveSetting(settingname + "FR", FR);
            Form1.saveSetting(settingname + "SA", SA);
            Form1.saveSetting(settingname + "SU", SU);


            saveSensors();


            var settingname2 = createSettingName();

            string setting_rules_name = settingname2 + "RULES";

            StringBuilder sensorRuleList = new StringBuilder();
            foreach (string listItem in listBoxSensorRuleList.Items)
            {
                sensorRuleList.Append(listItem + Form1.dataSeparator);
            }

            Form1.saveSetting(setting_rules_name, sensorRuleList.ToString());









        }

        private void buttonDeleteRule_Click(object sender, EventArgs e)
        {
            string settingname = "";
            if (listBoxCozifySensors.SelectedIndex != -1)
            {
                settingname = "cozifysensor_" + listBoxCozifySensors.SelectedItem.ToString();
            }
            else if (listBoxRuuviTagSensors.SelectedIndex != -1)
            {
                settingname = "ruuvitag_" + listBoxRuuviTagSensors.SelectedItem.ToString();
            }
            else
            {
                return;
            }
            settingname += "_";
            string setting_rules_name = settingname + "RULES";

            if (listBoxSensorRuleList.SelectedIndex == -1)
            {
                return;
            }
            settingname += listBoxSensorRuleList.SelectedItem.ToString();
            settingname += "_";


            //delete all settings that belong to this rule
            deleteSettingsForRule(settingname);



            listBoxSensorRuleList.Items.RemoveAt(listBoxSensorRuleList.SelectedIndex);
            var settingsdata = new StringBuilder();
            foreach (string item in listBoxSensorRuleList.Items)
            {
                settingsdata.AppendLine(item);
            }

            Form1.saveSetting(setting_rules_name, settingsdata.ToString());
        }
        private void deleteSettingsForRule(string settingname)
        {
            var rows = System.IO.File.ReadAllLines(Form1.settingsfile);
            var settingsdata = new StringBuilder();
            foreach (var row in rows)
            {
                if (string.IsNullOrWhiteSpace(row) == false && row.StartsWith(settingname) == false)
                {
                    settingsdata.AppendLine(row);
                }
            }
            System.IO.File.WriteAllText(Form1.settingsfile, settingsdata.ToString());

        }

        private void FormRuuvi_Load(object sender, EventArgs e)
        {

        }

        private void listBoxCozifySensors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxCozifySensors.SelectedIndex == -1) {
                return;
            }
            textBoxAddRuuviSensor.Text = listBoxCozifySensors.SelectedItem.ToString();
            listBoxRuuviTagSensors.SelectedIndex = -1;
            var settingname = createSettingName();
            settingname += "RULES";
            var rules = Form1.readSetting(settingname);
            var rows = rules.Split(new string[] { Form1.dataSeparator }, StringSplitOptions.RemoveEmptyEntries);
            listBoxSensorRuleList.Items.Clear();
            foreach (string row in rows)
            {
                listBoxSensorRuleList.Items.Add(row);
            }
        }
        private string createSettingName()
        {
            string settingname = "";
            if (listBoxCozifySensors.SelectedIndex != -1)
            {
                var q = (from c1 in Form1.deviceList where c1.name == listBoxCozifySensors.SelectedItem.ToString() select c1).FirstOrDefault();
                if (q == null)
                {
                    return null;
                }

                settingname = "cozifysensor_" + q.id;
            }
            else if (listBoxRuuviTagSensors.SelectedIndex != -1)
            {
                settingname = "ruuvitag_" + listBoxRuuviTagSensors.SelectedItem.ToString();
            }
            else
            {
                return null;
            }
            settingname += "_";
            return settingname;
            //            string setting_rules_name = settingname + "RULES";


        }
        private string getDeviceIdByName(string name)
        {
            return (from c1 in Form1.deviceList where c1.name == name select c1.id).FirstOrDefault();
        }
        private void buttonAddRule_Click(object sender, EventArgs e)
        {
            var settingname = createSettingName();
            if (settingname == null)
            {
                return;
            }

            string setting_rules_name = settingname + "RULES";

            //if (listBoxCozifySensors.SelectedIndex != -1)
            //{
            //    var device_id = getDeviceIdByName(listBoxCozifySensors.SelectedItem.ToString());
            //    if (device_id == null)
            //    {
            //        return;
            //    }
            //}

            int rule_number = 0;
            string rule_name = "New Rule";
            while (listBoxSensorRuleList.Items.Contains(rule_name))
            {
                rule_number++;
                rule_name = "New Rule " + rule_number.ToString();
            }
            listBoxSensorRuleList.Items.Add(rule_name);

            StringBuilder sensorRuleList = new StringBuilder();
            foreach (string listItem in listBoxSensorRuleList.Items)
            {
                sensorRuleList.Append(listItem + Form1.dataSeparator);
            }

            Form1.saveSetting(setting_rules_name, sensorRuleList.ToString());
        }
        private void listBoxSensorRuleListAction()
        {
            comboBox1.SelectedIndex = 0;
            textBoxTemperatureValue.Text = "";
            comboBoxDeviceList.SelectedIndex = -1;
            comboBoxDeviceAction.SelectedIndex = 0;
            textBoxDoNotTouch.Text = "";


            if (listBoxRuuviTagSensors.SelectedIndex == -1 && listBoxCozifySensors.SelectedIndex == -1)
            {
                return;
            }


            if (listBoxSensorRuleList.SelectedIndex == -1)
            {
                return;
            }

            var settingname = createSettingName();
            if (settingname == null)
            {
                return;
            }


            settingname += listBoxSensorRuleList.SelectedItem.ToString();
            settingname += "_";

            textBoxRuleName.Text = listBoxSensorRuleList.SelectedItem.ToString();
            if (comboBox1.SelectedIndex == -1)
            {
                comboBox1.SelectedIndex = 0;
            }

            var value1 = Form1.readSetting(settingname + "value1");
            if (value1 == "<" || value1 == ">")
            {
                comboBox1.SelectedItem = value1;
            }

            textBoxTemperatureValue.Text = Form1.readSetting(settingname + "temperature");
            comboBoxDeviceList.Items.Clear();
            foreach (var dev in Form1.deviceList)
            {
                if (dev.type == "POWER_SOCKET")
                {
                    comboBoxDeviceList.Items.Add(dev.name);
                }
            }

            var selectedDevice = Form1.readSetting(settingname + "device");
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
            textBoxDoNotTouch.Text = Form1.readSetting(settingname + "donottouch");

            comboBoxDeviceAction.SelectedItem = "ON";
            try
            {
                comboBoxDeviceAction.SelectedItem = Form1.readSetting(settingname + "deviceaction");
            }
            catch { }
            finally { }

            textBoxTimeStart.Text = Form1.readSetting(settingname + "timestart");
            textBoxTimeEnd.Text = Form1.readSetting(settingname + "timeend");


            checkBoxMO.Checked = !string.IsNullOrWhiteSpace(Form1.readSetting(settingname + "MO"));
            checkBoxTU.Checked = !string.IsNullOrWhiteSpace(Form1.readSetting(settingname + "TU"));
            checkBoxWE.Checked = !string.IsNullOrWhiteSpace(Form1.readSetting(settingname + "WE"));
            checkBoxTH.Checked = !string.IsNullOrWhiteSpace(Form1.readSetting(settingname + "TH"));
            checkBoxFR.Checked = !string.IsNullOrWhiteSpace(Form1.readSetting(settingname + "FR"));
            checkBoxSA.Checked = !string.IsNullOrWhiteSpace(Form1.readSetting(settingname + "SA"));
            checkBoxSU.Checked = !string.IsNullOrWhiteSpace(Form1.readSetting(settingname + "SU"));

        }
        private void listBoxSensorRuleList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonAddRuuviTag_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddRuuviSensor.Text) == true)
            {
                return;
            }
            bool exists = false;
            foreach (string item in listBoxRuuviTagSensors.Items)
            {
                if (item == textBoxAddRuuviSensor.Text)
                {
                    exists = true;
                }
            }

            if (exists == false)
            {
                listBoxRuuviTagSensors.Items.Add(textBoxAddRuuviSensor.Text);
                saveSensors();
            }

        }

        private void listBoxSensorRuleList_KeyPress(object sender, KeyPressEventArgs e)
        {
            listBoxSensorRuleListAction();
        }

        private void listBoxSensorRuleList_Click(object sender, EventArgs e)
        {
            listBoxSensorRuleListAction();
        }

        private void listBoxRuuviTagSensors_MouseClick(object sender, MouseEventArgs e)
        {
            listBoxRuuviTagSensosors_Action();
        }

        private void listBoxRuuviTagSensors_KeyPress(object sender, KeyPressEventArgs e)
        {
            listBoxRuuviTagSensosors_Action();
        }
    }
}
