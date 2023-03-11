using MQTTnet.Server;
using MQTTnet;
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
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MQTTnet.Diagnostics;
using MQTTnet.Client;
using System.Xml;

namespace CozifyWindows
{
    public partial class Form1 : Form
    {
        const string dataSeparator = "§'.'§";
        const string linefeed = "-§linefeed§-";
        private string selected_hubkey;
        private string lan_ip;
        private string api_version;
        public static List<Device> deviceList;
        public static string temperatureLogFile;
        public static int temperature_log_seconds;
        public static List<SpotPrice> spotPriceList;
        private static string settingsfile;
        public static string email;
        public static string log_string;
        public static Queue<string> mqtt_payload = new Queue<string>();

        public Form1()
        {
            InitializeComponent();
        }

        public static string readSetting(string key)
        {
            if (string.IsNullOrWhiteSpace(settingsfile))
            {
                getSettingsFile();
            }
            if (string.IsNullOrWhiteSpace(settingsfile))
            {
                return "";
            }
            if (File.Exists(settingsfile) == false)
            {
                return "";
            }
            var rows = File.ReadAllLines(settingsfile);
            string searchstring = key + "=";
            foreach (var row in rows)
            {
                if (row.StartsWith(searchstring))
                {
                    return row.Substring(searchstring.Length).Replace(linefeed, Environment.NewLine);
                }
            }
            return "";
        }

        public static void saveSetting(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(settingsfile))
            {
                getSettingsFile();
            }
            if (string.IsNullOrWhiteSpace(settingsfile))
            {
                return;
            }
            value = value.Replace("\n", linefeed);
            value = value.Replace("\r", "");

            string searchstring = key + "=";

            var sb = new StringBuilder();
            if (File.Exists(settingsfile) == true)
            {
                var rows = File.ReadAllLines(settingsfile);

                foreach (var row in rows)
                {
                    if (row.StartsWith(searchstring) == false)
                    {
                        sb.AppendLine(row);
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(value) == false)
            {
                sb.AppendLine(searchstring + value);
            }

            System.IO.File.WriteAllText(settingsfile, sb.ToString());

            return;
        }
        public static void getSettingsFile()
        {

            StringBuilder settingsfilename = new StringBuilder();

            var not_allowed_characters = Path.GetInvalidFileNameChars();

            if (string.IsNullOrWhiteSpace(email) == true)
            {
                return;
            }

            foreach (char x in email)
            {
                if (not_allowed_characters.Contains(x) == false)
                {
                    settingsfilename.Append(x);
                }
            }
            settingsfile = Path.Combine(Application.StartupPath, "settings-" + settingsfilename.ToString() + ".txt");
        }

        private string getEmail()
        {
            foreach (var arg in Environment.GetCommandLineArgs())
            {
                string searchString = "email=";
                if (arg.StartsWith(searchString) == true)
                {
                    var email = arg.Replace(searchString, "");
                    return email;
                }
            }

            var emailfile = Path.Combine(Application.StartupPath, "email.txt");
            if (File.Exists(emailfile))
            {
                var email_address = System.IO.File.ReadAllText(emailfile);
                email_address = email_address.Replace("\r", "").Replace("\n", "").Trim();
                return email_address;
            }
            return null;
        }

        public void saveEmail(string email)
        {
            var emailfile = Path.Combine(Application.StartupPath, "email.txt");
            System.IO.File.WriteAllText(emailfile, email);
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            email = getEmail();

            if (string.IsNullOrWhiteSpace(email) == true)
            {
                MessageBox.Show("please enter email address!");
                return;
            }

            textBoxEmail.Text = email;
            getSettingsFile();

            deviceList = new List<Device>();
            spotPriceList = new List<SpotPrice>();


            int item_counter = 0;
            foreach (var item in comboBox1.Items)
            {
                if (item.ToString() == readSetting("ip_address_mode"))
                {
                    comboBox1.SelectedIndex = item_counter;
                }
                item_counter++;
            }

            if (comboBox1.SelectedIndex == -1)
            {
                comboBox1.SelectedIndex = 0;
            }


            var hubs = readSetting("hubs").Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var hubdata in hubs)
            {
                var hub = hubdata.Split(':');
                listBox1.Items.Add(hub[0]);
            }

            selectHub(readSetting("selected_hub"));


            bool autoclose = false;


            var device_id = getCommandArg("device_id");
            var action = getCommandArg("action");



            if (string.IsNullOrWhiteSpace(device_id) == false)
            {
                int errCounter = 0;
                string result = null;
                while (errCounter < 3)
                {
                    if (action.ToUpper() == "ON")
                    {
                        result = await deviceON(device_id);
                        autoclose = true;
                    }
                    else if (action.ToUpper() == "OFF")
                    {
                        result = await deviceOFF(device_id);
                        autoclose = true;
                    }
                    if (string.IsNullOrWhiteSpace(result) == true)
                    {
                        errCounter++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (autoclose)
                {
                    int exitCode = 0;
                    if (string.IsNullOrWhiteSpace(result) == true)
                    {
                        exitCode = 255;
                    }
                    log("Exit with code " + exitCode.ToString());
                    Environment.Exit(exitCode);
                    return;
                }

            }









            parseSpotPrices(readSetting("spot_price_list"));

            textBoxDeviceControlId.Text = readSetting("selected_test_device");

            temperatureLogFile = Path.Combine(Application.StartupPath, "temperatures.txt");

            var devices_setting = readSetting("devices");
            if (string.IsNullOrWhiteSpace(devices_setting) == false)
            {
                parsedevicelist(devices_setting);
            }

            var temperature_log_seconds_string = readSetting("temperature_log_seconds");
            int.TryParse(temperature_log_seconds_string, out int temperature_log_seconds);

            if (temperature_log_seconds > 0)
            {
                timerTemperatureLogging.Interval = temperature_log_seconds * 1000;
            }
        }

        public string getCommandArg(string key)
        {
            foreach (var arg in Environment.GetCommandLineArgs())
            {
                string searchString = key + "=";
                if (arg.StartsWith(searchString) == true)
                {
                    return arg.Substring(searchString.Length);
                }
            }
            return null;
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            string json = @"{""email"": """ + textBoxEmail.Text + @""", ""password"": """ + textBoxPassword.Text + @"""}";
            var token = await HttpPost("https://api.cozify.fi/ui/0.2/user/emaillogin", json);
            if (string.IsNullOrWhiteSpace(token) == false)
            {
                saveSetting("token", token);
                log("We have got token.");
                textBoxPassword.Text = "";
            }
        }
        public void log(string text)
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



        private async void buttonTemporaryPassword_Click(object sender, EventArgs e)
        {
            var url = "https://api.cozify.fi/ui/0.2/user/requestlogin?email=" + textBoxEmail.Text;
            await HttpPost(url, "");
            MessageBox.Show("Please check your email");
        }

        private async Task<string> HttpPost(string url, string postData, string method = "POST")
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
                    var token = readSetting("token");
                    req.Headers.Add("X-Hub-Key", selected_hubkey);
                    req.Headers.Add("Authorization", "Bearer " + token);
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

                HttpWebResponse resp = await req.GetResponseAsync() as HttpWebResponse;
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
                log("HttpPost ERROR url: " + url);
                string msg = "HttpPost ERROR " + ex.ToString();
                log(msg);
                return null;
            }
            finally { }
        }

        private async Task<string> HttpGet(string url)
        {
            try
            {
                log("HttpGet " + url);
                UTF8Encoding encoding = new UTF8Encoding();

                HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;



                if (string.IsNullOrWhiteSpace(lan_ip) == false && string.IsNullOrWhiteSpace(lan_ip) == false && url.Contains(lan_ip) == true && string.IsNullOrWhiteSpace(selected_hubkey) == false)
                {
                    req.Headers.Add("Authorization", selected_hubkey);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(selected_hubkey) == false)
                    {
                        req.Headers.Add("X-Hub-Key", selected_hubkey);
                    }
                    var token = readSetting("token");
                    req.Headers.Add("Authorization", "Bearer " + token);
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




                HttpWebResponse resp = await req.GetResponseAsync() as HttpWebResponse;
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

        private void buttonHubKeys_Click(object sender, EventArgs e)
        {
            loadHubs();
        }

        private async void loadHubs()
        {
            var token = readSetting("token");
            if (string.IsNullOrWhiteSpace(token))
            {
                return;
            }

            var data1 = await HttpGet("https://api.cozify.fi/ui/0.2/user/hubkeys");
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

            saveSetting("hubs", hubs);
        }

        private async void buttonRefreshToken_Click(object sender, EventArgs e)
        {
            await refreshToken();
        }

        private async Task refreshToken()
        {
            var newtoken = await HttpGet("https://api.cozify.fi/ui/0.2/user/refreshsession");
            if (string.IsNullOrWhiteSpace(newtoken))
            {
                return;
            }
            saveSetting("token", newtoken);
            log("token refreshed");
        }

        private async void buttonGetDevices_Click(object sender, EventArgs e)
        {
            await getDeviceList();
            await getHubApiVersion();
            await getDeviceList();
        }
        private async Task getDeviceList()
        {
            deviceList = new List<Device>();
            if (string.IsNullOrWhiteSpace(api_version))
            {
                await getHubApiVersion();
            }

            var hubs_1 = readSetting("hubs");

            if (string.IsNullOrWhiteSpace(hubs_1))
            {
                loadHubs();
            }

            if (listBox1.Items.Count == 0)
            {
                var hub_string = readSetting("hubs");
                var hubs = hub_string.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
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

            var json = await HttpGet(url);

            if (string.IsNullOrWhiteSpace(json))
            {
                return;
            }

            saveSetting("devices", json);
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
            var hubs1 = readSetting("hubs");
            if (string.IsNullOrWhiteSpace(hubs1))
            {
                loadHubs();
            }

            hubs1 = readSetting("hubs");
            if (string.IsNullOrWhiteSpace(hubs1))
            {
                return;
            }

            if (listBox1.Items.Contains(hub_id))
            {
                listBox1.SelectedItem = hub_id;
            }

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

            saveSetting("selected_hub", listBox1.SelectedItem.ToString());
        }

        private async Task getHubApiVersion()
        {
            api_version = readSetting("hub_api_version");
            lan_ip = readSetting("lan_ip");
            if (string.IsNullOrWhiteSpace(api_version) == false && string.IsNullOrWhiteSpace(lan_ip) == false)
            {
                return;
            }

            var ip_list = new List<string>();

            if (comboBox1.SelectedItem.ToString() != "API")
            {
                ip_list = await getLanIpList();
            }

            var ip = ip_list.FirstOrDefault();

            string url = "https://api.cozify.fi/ui/0.2/hub/remote/hub";

            if (string.IsNullOrWhiteSpace(ip) == false)
            {
                lan_ip = ip;
                url = "http://" + lan_ip + ":8893/hub";
            }
            var hub_info = await HttpGet(url);


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
            lan_ip = ip;
            saveSetting("hub_api_version", api_version);
            saveSetting("lan_ip", lan_ip);

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

        private async void buttonGetApiVersion_Click(object sender, EventArgs e)
        {
            saveSetting("hub_api_version", "");
            await getHubApiVersion();
        }

        private async void buttonGetLanIp_Click(object sender, EventArgs e)
        {
            var ip_list = await getLanIpList();
            foreach (var ip in ip_list)
            {
                log("lan ip: " + ip);
            }

        }
        private async Task<List<string>> getLanIpList()
        {
            var data = await HttpGet("https://api.cozify.fi/ui/0.2/hub/lan_ip");
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
        private async Task<string> deviceON(string id)
        {
            if (listBox1.Items.Count == 0)
            {
                await getDeviceList();
            }

            if (string.IsNullOrWhiteSpace(api_version))
            {
                await getHubApiVersion();
            }
            string data = "[{\"id\":\"" + id + "\", \"type\": \"CMD_DEVICE_ON\"}]";
            string url = "https://api.cozify.fi/ui/0.2/hub/remote/cc/" + api_version + "/devices/command";

            if (string.IsNullOrWhiteSpace(lan_ip) == false)
            {
                url = "http://" + lan_ip + ":8893/cc/" + api_version + "/devices/command";
            }
            log("deviceON:" + id);
            return await HttpPost(url, data, "PUT");
        }
        private async Task<string> deviceOFF(string id)
        {
            if (listBox1.Items.Count == 0)
            {
                await getDeviceList();
            }
            if (string.IsNullOrWhiteSpace(api_version))
            {
                await getHubApiVersion();
            }
            string data = "[{\"id\":\"" + id + "\", \"type\": \"CMD_DEVICE_OFF\"}]";
            string url = "https://api.cozify.fi/ui/0.2/hub/remote/cc/" + api_version + "/devices/command";

            if (string.IsNullOrWhiteSpace(lan_ip) == false)
            {
                url = "http://" + lan_ip + ":8893/cc/" + api_version + "/devices/command";
            }
            log("deviceOFF:" + id);
            return await HttpPost(url, data, "PUT");
        }
        private async void buttonDeviceOn_Click(object sender, EventArgs e)
        {
            await deviceON(textBoxDeviceControlId.Text);
        }

        private async void buttonDeviceOff_Click(object sender, EventArgs e)
        {
            await deviceOFF(textBoxDeviceControlId.Text);
        }

        private void buttonTempSensors_Click(object sender, EventArgs e)
        {
            var form2 = new FormTempSensors();
            form2.Show();
        }

        private async void timerTemperatureLogging_Tick(object sender, EventArgs e)
        {
            if (temperature_log_seconds == 0)
            {
                timerTemperatureLogging.Interval = 1000;
                return;
            }

            timerTemperatureLogging.Interval = temperature_log_seconds * 1000;
            var selected_temp_sensors_list = readSetting("selected_temp_sensors").Split('§');

            await getDeviceList();

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
            saveSetting("spot_price_list", data);
            return data;
        }

        private void buttonElectricityPrices_Click(object sender, EventArgs e)
        {
            var prices = downloadSpotPrices();
            parseSpotPrices(prices);
        }

        private async void timerSpotPrices_Tick(object sender, EventArgs e)
        {
            var timer_spot_price_seconds_string = readSetting("timer_spot_price_seconds");

            int.TryParse(timer_spot_price_seconds_string, out int timer_spot_price_seconds);
            if (timer_spot_price_seconds == 0)
            {
                return;
            }

            var spot_price_controlled_devices = readSetting("spot_price_controlled_devices");
            if (string.IsNullOrWhiteSpace(spot_price_controlled_devices))
            {
                return;
            }

            timerSpotPrices.Enabled = false;

            timerSpotPrices.Interval = timer_spot_price_seconds * 1000;

            var spot_device_list = spot_price_controlled_devices.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);


            foreach (var row in spot_device_list)
            {
                if (string.IsNullOrWhiteSpace(row) == true)
                {
                    continue;
                }

                var data = row.Split(new string[] { dataSeparator }, StringSplitOptions.None);
                var devicename = data[0];

                var cheapesthours = 0;
                double maxprice = 0;

                if (string.IsNullOrWhiteSpace(data[1]) == false)
                {
                    cheapesthours = Convert.ToInt32(data[1]);
                }

                if (string.IsNullOrWhiteSpace(data[2]) == false)
                {
                    maxprice = Convert.ToDouble(data[2]);
                    maxprice = maxprice / 100;
                }

                if (maxprice <= 0 && cheapesthours <= 0)
                {
                    continue;
                }

                var dev = (from c1 in deviceList where c1.name == devicename select c1).FirstOrDefault();
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

                    bool turnOffByCheapestHours = false;
                    bool turnOffByPrice = false;

                    if (cheapesthours > 0)
                    {
                        if (spotquery.rank > cheapesthours)
                        {
                            turnOffByCheapestHours = true;
                        }
                    }
                    if (maxprice > 0)
                    {
                        if (spotquery.PriceWithTax > maxprice)
                        {
                            turnOffByPrice = true;
                        }
                    }
                    if (turnOffByCheapestHours)
                    {
                        log("Turn device [" + dev.name + "] off based on spot rank [" + spotquery.rank.ToString() + "]");
                    }

                    if (turnOffByPrice)
                    {
                        log("Turn device [" + dev.name + "] off based on spot price [" + spotquery.PriceWithTax.ToString() + "]");
                    }


                    if (turnOffByCheapestHours || turnOffByPrice)
                    {
                        await deviceOFF(dev.id);
                    }
                    else
                    {
                        log("Turn device [" + dev.name + "] on.");
                        if (maxprice > 0)
                        {
                            log("Turn device [" + dev.name + "] on based on spot price [" + spotquery.PriceWithTax.ToString() + "]");
                        }
                        if (cheapesthours > 0)
                        {
                            log("Turn device [" + dev.name + "] on based on spot rank [" + spotquery.rank.ToString() + "]");
                        }
                        await deviceON(dev.id);
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
            if (readSetting("ip_address_mode") != comboBox1.SelectedItem.ToString())
            {
                saveSetting("ip_address_mode", comboBox1.SelectedItem.ToString());
            }
        }
        private void textBoxEmail_Leave_1(object sender, EventArgs e)
        {
            saveEmail(textBoxEmail.Text);
        }

        private void textBoxDeviceControlId_Leave(object sender, EventArgs e)
        {
            saveSetting("selected_test_device", textBoxDeviceControlId.Text);
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            textBoxPassword.Text = textBoxPassword.Text.Trim();
        }

        private async Task MQTTserver()
        {
            var mqttFactory = new MqttFactory();
            //          var mqttFactory = new MqttFactory(new ConsoleLogger());


            // The port for the default endpoint is 1883.
            var mqttServerOptions = new MqttServerOptionsBuilder().WithDefaultEndpoint().Build();
            var mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions);
            await mqttServer.StartAsync();
        }

        private async void buttonStartMqttServer_Click(object sender, EventArgs e)
        {
            await MQTTserver();
            timerLogging.Enabled = true;
        }

        private void timerLogging_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(log_string) == false)
            {
                log(log_string);
                log_string = "";
            }

            while (mqtt_payload.Count > 0)
            {
                var item = mqtt_payload.Dequeue();
                var data1 = item.Split('}');
                var data2 = data1[0].Split(new string[] { "\"data\":\t\"" }, StringSplitOptions.None);
                var data3 = data2[1].Split('"');
                var y = data3[0];
                var data = y.Replace("0201061BFF990405", "");
                var sensor_id = data.Substring(34);
                data = data.Substring(0, 4);
                var z = Convert.ToInt16(data, 16);
                double temperature = z * 0.005;
              var data20 = data1[0].Split(new string[] { "\"ts\":\t\"" }, StringSplitOptions.None);
                var data30 = data20[1].Split('"');
                var unix = Convert.ToDouble(data30[0]);
                var datetime = UnixTimeStampToDateTime(unix);
                string timestamp = datetime.Year.ToString() + "-" + datetime.Month.ToString("00") + "-" + datetime.Day.ToString("00") + " " + datetime.Hour.ToString() + ":" + datetime.Minute.ToString("00") + ":" + datetime.Second.ToString("00");
                log(@"Received payload
   timestamp: " + timestamp + @"
   temperature: " + temperature+@"
   sensor id: "+sensor_id);

            }        }

        private async void buttonStartMqttClient_Click(object sender, EventArgs e)
        {
            timerLogging.Enabled = true;

            //var mqttFactory = new MqttFactory(new ConsoleLogger());
            var mqttFactory = new MqttFactory();

var broker=            readSetting("mqtt_broker");
            if(string.IsNullOrWhiteSpace(broker) == true)
            {
                MessageBox.Show("missing configuration: mqtt server address");
                return;
            }
            int? broker_port = null;
            var broker_address = broker;
            if (broker.Contains(":")) {
                var splitted = broker.Split(':');
                broker_address = splitted[0]    ;
                if (int.TryParse(splitted[1],out int port_number) == true)
                {
                    broker_port=port_number; 
                }
            }
            var mqttClient = mqttFactory.CreateMqttClient();
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(broker_address,broker_port)
                .Build();
            mqttClient.ApplicationMessageReceivedAsync += ee =>
            {
                if (string.IsNullOrWhiteSpace(log_string))
                {
                    log_string = "";
                }

                log_string += "Received application message." + Environment.NewLine;

                var mqtt_message = Encoding.UTF8.GetString(ee.ApplicationMessage.Payload, 0, ee.ApplicationMessage.Payload.Length);

                log_string += mqtt_message + Environment.NewLine;
                mqtt_payload.Enqueue(mqtt_message);
                return Task.CompletedTask;
            };

            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            var ruuvisensors = readSetting("mqtt_topics");

            var sensors = ruuvisensors.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach(var sensor in sensors)
            {
                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(
                    f =>
                    {
                        f.WithTopic(sensor);
                    })
                .Build();

                var response = await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
            }
            
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var formRuuvi = new FormRuuvi();
            formRuuvi.Show();
        }
    }
    class ConsoleLogger : IMqttNetLogger
    {
        public bool IsEnabled => true;

        public void Publish(MqttNetLogLevel logLevel, string source, string message, object[] parameters = null, Exception exception = null)
        {
            if (parameters != null && parameters.Length > 0)
            {
                message = string.Format(message, parameters);
            }

            if (Form1.log_string == null)
            {
                Form1.log_string = "";
            }
            Form1.log_string += message + Environment.NewLine;
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
