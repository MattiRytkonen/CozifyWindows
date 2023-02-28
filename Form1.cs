using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozifyWindows
{

    public partial class Form1 : Form
    {
        private string selected_hubkey;
        private string lan_ip;
        private string api_version;
        public static List<Device> deviceList;
        public static string temperatureLogFile;
        public static int temperature_log_seconds;
        public static List<SpotPrice> spotPriceList;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            deviceList = new List<Device>();
            spotPriceList = new List<SpotPrice>();
            
            
            int item_counter = 0;
            foreach(var item in comboBox1.Items)
            {
                if(item.ToString() == Properties.Settings.Default.ip_address_mode)
                {
                    comboBox1.SelectedIndex = item_counter;
                }                
                    item_counter++;
            }

            Properties.Settings.Default.Save();


            parseSpotPrices(Properties.Settings.Default.spot_price_list);

            textBoxDeviceControlId.Text = Properties.Settings.Default.selected_test_device;

            temperatureLogFile = Path.Combine(Application.StartupPath, "temperatures.txt");

            textBoxEmail.Text = Properties.Settings.Default.email;
            textBoxPassword.Text = Properties.Settings.Default.password;

            var hubs = Properties.Settings.Default.hubs.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var hubdata in hubs)
            {
                var hub = hubdata.Split(':');
                listBox1.Items.Add(hub[0]);
            }

            selectHub(Properties.Settings.Default.selected_hub);

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.devices) == false)
            {
                parsedevicelist(Properties.Settings.Default.devices);
            }

            if (Properties.Settings.Default.temperature_log_seconds > 0)
            {
                temperature_log_seconds = Properties.Settings.Default.temperature_log_seconds;
                timerTemperatureLogging.Interval = Properties.Settings.Default.temperature_log_seconds * 1000;
            }


        }


        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string json = @"{""email"": """ + textBoxEmail.Text + @""", ""password"": """ + textBoxPassword.Text + @"""}";
            var token = HttpPost("https://api.cozify.fi/ui/0.2/user/emaillogin", json);
            if (string.IsNullOrWhiteSpace(token) == false)
            {
                Properties.Settings.Default.token = token;
                Properties.Settings.Default.Save();
                log("We have got token.");
                textBoxPassword.Text = "";
            }
        }
        private void log(string text)
        {
            if (textBoxLog.Text.Length > 60000)
            {
                textBoxLog.Text = textBoxLog.Text.Substring(50000);
            }
            string logline = DateTime.Now.ToString() + " : " + text + Environment.NewLine;
            textBoxLog.AppendText(logline);
            string logfile = Path.Combine(Application.StartupPath, "log.txt");
            try
            {
                System.IO.File.AppendAllText(logfile, logline);
            }
            catch (Exception ex)
            {
                textBoxLog.AppendText("log exception: " + ex.ToString() + Environment.NewLine);
            }
            finally { }

        }
        private void buttonTemporaryPassword_Click(object sender, EventArgs e)
        {
            var url = "https://api.cozify.fi/ui/0.2/user/requestlogin?email=" + textBoxEmail.Text;
            HttpPost(url, "");
            MessageBox.Show("Please check your email");
        }

        private string HttpPost(string url, string postData, string method = "POST")
        {
            try
            {
                log("HttpPost " + url);
                UTF8Encoding encoding = new UTF8Encoding();
                string postData1 = postData;
                byte[] data = encoding.GetBytes(postData1);

                //            log("postdata:" + postData1);

                HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
                //req.Headers.Add("X-Hub-Key", selected_hubkey);
                //req.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.token);
                //              log("selected hubkey:" + selected_hubkey);
                //                req.Headers.Add("Authorization", selected_hubkey);

                if (string.IsNullOrWhiteSpace(lan_ip) == false && url.Contains(lan_ip) == true)
                {
                    req.Headers.Add("Authorization", selected_hubkey);
                }
                else
                {
                    req.Headers.Add("X-Hub-Key", selected_hubkey);
                    req.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.token);
                }



                req.PreAuthenticate = true;
                req.Timeout = 1000;
                req.ReadWriteTimeout = 1000;
                req.Method = method;
                req.ContentType = "application/json";

                string result = null;
                Stream newStream = req.GetRequestStream();
                //Stream s = req.GetRequestStream();
                newStream.Write(data, 0, data.Length);

                newStream.Close();

                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
                if (result == null)
                {
                    result = "[OK]";
                }
                log("HttpPost result: " + result);
                return result;
            }
            catch (Exception ex)
            {
                string msg = "HttpPost ERROR " + ex.ToString();
                log(msg);
                return null;
            }
            finally { }
        }

        private string HttpGet(string url)
        {
            try
            {
                log("HttpGet " + url);
                UTF8Encoding encoding = new UTF8Encoding();

                HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;



                if (string.IsNullOrWhiteSpace(lan_ip) == false && string.IsNullOrWhiteSpace (lan_ip)==false && url.Contains(lan_ip) == true && string.IsNullOrWhiteSpace(selected_hubkey) == false)
                {
                    req.Headers.Add("Authorization", selected_hubkey);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(selected_hubkey) == false)
                    {
                        req.Headers.Add("X-Hub-Key", selected_hubkey);
                    }
                    req.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.token);
                }


                req.PreAuthenticate = true;
                req.Timeout = 1000;
                req.ReadWriteTimeout = 1000;
                req.Method = "GET";
                req.ContentType = "application/json";
                string postData1 = "";
                byte[] data = encoding.GetBytes(postData1);
                string result = null;
                //Stream newStream = req.GetRequestStream();

                //newStream.Write(data, 0, data.Length);

                // newStream.Close();
                // Stream s = req.GetRequestStream();




                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
                if (result == null)
                {
                    result = "[OK]";
                }
                log("HttpGet result: " + result);
                return result;
            }
            catch (Exception ex)
            {
                string msg = "ERROR " + ex.ToString();
                log(msg);
                return null;
            }
            finally { }
        }


        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.email = textBoxEmail.Text;
            Properties.Settings.Default.Save();
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            textBoxPassword.Text = textBoxPassword.Text.Trim();
            Properties.Settings.Default.password = textBoxPassword.Text;
            Properties.Settings.Default.Save();
        }

        private void buttonHubKeys_Click(object sender, EventArgs e)
        {
            loadHubs();
        }

        private void loadHubs()
        {
            if(string.IsNullOrWhiteSpace(Properties.Settings.Default.token))
            {
                return;
            }


            var data1 = HttpGet("https://api.cozify.fi/ui/0.2/user/hubkeys");
            if (string.IsNullOrWhiteSpace(data1))
            {
                return;
            }

            string hubs = "";

            var splitted1 = data1.Split(',');
            foreach (var data in splitted1)
            {
                var data2 = data.Replace("}", "");
                data2 = data2.Replace("{", "");
                data2 = data2.Replace("\"", "");
                var splitted = data2.Split(':');
                hubs += splitted[0] + ":" + splitted[1] + Environment.NewLine;
            }

            Properties.Settings.Default.hubs = hubs;
            Properties.Settings.Default.Save();
        }

        private void buttonRefreshToken_Click(object sender, EventArgs e)
        {
            refreshToken();
        }

        private void refreshToken()
        {

            var newtoken = HttpGet("https://api.cozify.fi/ui/0.2/user/refreshsession");
            if (string.IsNullOrWhiteSpace(newtoken))
            {
                return;
            }

            Properties.Settings.Default.token = newtoken;
            Properties.Settings.Default.Save();

            log("token refreshed");
        }

        private void buttonGetDevices_Click(object sender, EventArgs e)
        {
            getDeviceList();
            getHubApiVersion();
            getDeviceList();
        }
        private void getDeviceList()
        {
            deviceList = new List<Device>();
            if (string.IsNullOrWhiteSpace(api_version))
            {
                getHubApiVersion();
            }

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.hubs))
            {
                loadHubs();
            }

            if (listBox1.Items.Count == 0)
            {
                var hubs = Properties.Settings.Default.hubs.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var hubdata in hubs)
                {
                    var hub = hubdata.Split(':');
                    listBox1.Items.Add(hub[0]);
                }              
            }
            if (listBox1.Items.Count > 0)
            {
                selectHub(listBox1.Items[0].ToString());
            }


            string url = "https://api.cozify.fi/ui/0.2/hub/remote/cc/" + api_version + "/devices";
            if (string.IsNullOrWhiteSpace(lan_ip) == false)
            {
                url = "http://" + lan_ip + ":8893/cc/" + api_version + "/devices";
            }

            var json = HttpGet(url);

            if (string.IsNullOrWhiteSpace(json))
            {
                return;
            }

            Properties.Settings.Default.devices = json;
            Properties.Settings.Default.Save();
            parsedevicelist(json);

        }
        private void parsedevicelist(string json)
        {
            deviceList = new List<Device>();
            dynamic array = JsonConvert.DeserializeObject(json);
            foreach (var cozify_devices in array)
            {
                foreach (var cozify_device in cozify_devices)
                {
                    var device = new Device();
                    device.id = cozify_device.id;
                    device.name = cozify_device.name;
                    device.type = cozify_device.type;


                    device.reachable = cozify_device.state.reachable;
                    device.lastSeen = cozify_device.state.lastSeen;
                    device.state_type = cozify_device.state.type;

                    if (device.type == "MULTI_SENSOR")
                    {
                        device.temperature = cozify_device.state.temperature;
                        device.humidity = cozify_device.state.humidity;
                    }

                    var capabilities = new List<string>();

                    foreach (string capability in cozify_device.capabilities.values)
                    {
                        capabilities.Add(capability);
                    }
                    device.capabilities = capabilities.ToArray();



                    deviceList.Add(device);

                }
            }
        }
        private void selectHub(string hub_id)
        {
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.hubs))
            {
                loadHubs();
            }

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.hubs))
            {
                return;
            }

            if (listBox1.Items.Contains(hub_id))
            {
                listBox1.SelectedItem = hub_id;
            }
            var hubs1 = Properties.Settings.Default.hubs;

            var hubs = hubs1.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var hubdata in hubs)
            {
                var hub = hubdata.Split(':');
                if (hub[0] == hub_id)
                {
                    selected_hubkey = hub[1];
                }
            }


            if (listBox1.Items.Count == 0)
            {
                foreach (var hubdata in hubs)
                {
                    var hub = hubdata.Split(':');
                    listBox1.Items.Add(hub[0]);
                }
            }
            if (listBox1.SelectedIndex == -1)
            {
                listBox1.SelectedIndex = 0;
            }

            Properties.Settings.Default.selected_hub = listBox1.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        private void getHubApiVersion()
        {
            var ip_list = new List<string>();

            if (comboBox1.SelectedItem.ToString() != "API") {
                  ip_list = getLanIpList();
            }
           

            var ip = ip_list.FirstOrDefault();

            string url = "https://api.cozify.fi/ui/0.2/hub/remote/hub";

            if (string.IsNullOrWhiteSpace(ip) == false)
            {
                lan_ip = ip;
                url = "http://" + lan_ip + ":8893/hub";
            }
            var hub_info = HttpGet(url);


            if (string.IsNullOrWhiteSpace(hub_info))
            {
                return;
            }

            dynamic item = JsonConvert.DeserializeObject(hub_info);

            string hubId = item.hubId;
            string name = item.name;
            string version = item.version;
            string state = item.state;

            var data3 = version.Split('.');
            var short_version = data3[0] + "." + data3[1];

            api_version = short_version;

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                return;
            }
            if (listBox1.SelectedIndex == -1)
            {
                listBox1.SelectedIndex = 0;
            }
            selectHub(listBox1.SelectedItem.ToString());
        }

        private void buttonGetApiVersion_Click(object sender, EventArgs e)
        {
            getHubApiVersion();
        }

        private void buttonGetLanIp_Click(object sender, EventArgs e)
        {
            var ip_list = getLanIpList();
            foreach (var ip in ip_list)
            {
                log("lan ip: " + ip);
            }

        }
        private List<string> getLanIpList()
        {
            var data = HttpGet("https://api.cozify.fi/ui/0.2/hub/lan_ip");
            if (string.IsNullOrWhiteSpace(data))
            {
                return null;
            }
            var ip_list = new List<string>();
            var slices = data.Split(',');
            foreach (var slice in slices)
            {
                var ip = slice.Replace(@"""", "");
                ip = ip.Replace("]", "");
                ip = ip.Replace("[", "");
                ip_list.Add(ip);
            }

            return ip_list;

        }
        private void deviceON(string id)
        {
            if (string.IsNullOrWhiteSpace(api_version))
            {
                getHubApiVersion();
            }
            string data = "[{\"id\":\"" + id + "\", \"type\": \"CMD_DEVICE_ON\"}]";
            string url = "https://api.cozify.fi/ui/0.2/hub/remote/cc/" + api_version + "/devices/command";

            if (string.IsNullOrWhiteSpace(lan_ip) == false)
            {
                url = "http://" + lan_ip + ":8893/cc/" + api_version + "/devices/command";
            }

            HttpPost(url, data, "PUT");
        }
        private void deviceOFF(string id)
        {
            if (string.IsNullOrWhiteSpace(api_version))
            {
                getHubApiVersion();
            }
            string data = "[{\"id\":\"" + id + "\", \"type\": \"CMD_DEVICE_OFF\"}]";
            string url = "https://api.cozify.fi/ui/0.2/hub/remote/cc/" + api_version + "/devices/command";

            if (string.IsNullOrWhiteSpace(lan_ip) == false)
            {
                url = "http://" + lan_ip + ":8893/cc/" + api_version + "/devices/command";
            }
            HttpPost(url, data, "PUT");
        }
        private void buttonDeviceOn_Click(object sender, EventArgs e)
        {
            deviceON(textBoxDeviceControlId.Text);
        }

        private void buttonDeviceOff_Click(object sender, EventArgs e)
        {
            deviceOFF(textBoxDeviceControlId.Text);
        }

        private void buttonTempSensors_Click(object sender, EventArgs e)
        {
            var form2 = new FormTempSensors();
            form2.Show();
        }

        private void timerTemperatureLogging_Tick(object sender, EventArgs e)
        {
            if (temperature_log_seconds == 0)
            {
                timerTemperatureLogging.Interval = 1000;
                return;
            }

            timerTemperatureLogging.Interval = temperature_log_seconds * 1000;
            var selected_temp_sensors_list = Properties.Settings.Default.selected_temp_sensors.Split('§');

            getDeviceList();

            string date = dateString();

            var sb = new StringBuilder();

            foreach (var sensor in selected_temp_sensors_list)
            {
                if (string.IsNullOrWhiteSpace(sensor) == true)
                {
                    continue;
                }
                var dev = (from c1 in deviceList where c1.name == sensor select c1).FirstOrDefault();
                if (dev != null && dev.temperature.HasValue)
                {
                    sb.AppendLine(date + "\t" + dev.name + "\t" + dev.temperature.ToString());
                }

            }


            System.IO.File.AppendAllText(temperatureLogFile, sb.ToString());

            log("Writing temperature log.");
        }

        private string dateString()
        {
            return DateTime.Now.Year.ToString("0000") + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00") + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
        }

        private void textBoxDeviceControlId_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.selected_test_device = textBoxDeviceControlId.Text;
            Properties.Settings.Default.Save();
        }

        private void parseSpotPrices(string json)
        {
            if (string.IsNullOrWhiteSpace(json) == true)
            {
                return;
            }

            var list = new List<SpotPrice>();
            dynamic array = JsonConvert.DeserializeObject(json);
            foreach (var item in array)
            {
                var spot = new SpotPrice();
                spot.rank = item.Rank;
                spot.date = Convert.ToDateTime(item.DateTime);
                spot.PriceNoTax = item.PriceNoTax;
                spot.PriceWithTax = item.PriceWithTax;
                list.Add(spot);
            }

            spotPriceList = list;
        }

        private string downloadSpotPrices()
        {
            var w = new WebClient();
            var data = w.DownloadString("https://api.spot-hinta.fi/TodayAndDayForward");
            log("downloadSpotPrices response: " + data);
            Properties.Settings.Default.spot_price_list = data;
            Properties.Settings.Default.Save();
            return data;
        }

        private void buttonElectricityPrices_Click(object sender, EventArgs e)
        {
            var prices = downloadSpotPrices();
            parseSpotPrices(prices);
        }

        private void timerSpotPrices_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.timer_spot_price_seconds == 0)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.selected_spot_devices))
            {
                return;
            }
            if (Properties.Settings.Default.spot_hours == 0)
            {
                return;
            }


            timerSpotPrices.Enabled = false;

            timerSpotPrices.Interval = Properties.Settings.Default.timer_spot_price_seconds * 1000;

            var spot_device_list = Properties.Settings.Default.selected_spot_devices.Split('§');


            foreach (var device in spot_device_list)
            {
                if (string.IsNullOrWhiteSpace(device) == true)
                {
                    continue;
                }
                var dev = (from c1 in deviceList where c1.name == device select c1).FirstOrDefault();
                if (dev != null && dev.type == "POWER_SOCKET")
                {
                    var spotquery = (from c1 in spotPriceList
                                     where
                                     c1.date.Year == DateTime.Now.Year
                                     && c1.date.Month == DateTime.Now.Month
                                     && c1.date.Day == DateTime.Now.Day
                                     && c1.date.Hour == DateTime.Now.Hour
                                     select c1).FirstOrDefault();

                    if (spotquery == null)
                    {
                        var prices = downloadSpotPrices();
                        parseSpotPrices(prices);
                        timerSpotPrices.Enabled = true;
                        return;
                    }


                    if (spotquery.rank <= Properties.Settings.Default.spot_hours)
                    {
                        log("Turn device [" + dev.name + "] on based on spot rank [" + spotquery.rank.ToString() + "]");
                        deviceON(dev.id);
                    }
                    else
                    {
                        log("Turn device [" + dev.name + "] off based on spot rank [" + spotquery.rank.ToString() + "]");
                        deviceOFF(dev.id);
                    }

                }

            }


            timerSpotPrices.Enabled = true;
        }

        private void buttonSpotPriceControl_Click(object sender, EventArgs e)
        {
            var form3 = new FormSpotPrices();
            form3.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ip_address_mode = comboBox1.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }
    }

    public class Device
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool reachable { get; set; }
        public long lastSeen { get; set; }
        public double? temperature { get; set; }
        public double? humidity { get; set; }
        public string state_type { get; set; }
        public string type { get; set; }
        public string[] capabilities { get; set; }
    }
    public class SpotPrice
    {
        public int rank { get; set; }
        public DateTime date { get; set; }
        public double PriceNoTax { get; set; }
        public double PriceWithTax { get; set; }
    }
}
