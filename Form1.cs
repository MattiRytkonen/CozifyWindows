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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Security.Cryptography;

namespace CozifyWindows
{
    public partial class Form1 : Form
    {
        public const string dataSeparator = "§'.'§";
        public const string linefeed = "-§linefeed§-";
        private string selected_hubkey;
        private string lan_ip;
        private string api_version;
        public static List<Device> deviceList;
        public static string temperatureLogFile;
        public static int temperature_log_seconds;
        public static List<SpotPrice> spotPriceList;
        public static string settingsfile;
        public static string email;
        public static string log_string;
        public static Queue<string> mqtt_payload = new Queue<string>();
        public Dictionary<string, DateTime> ruuviSensorLastTouch;
        public DateTime lastTemperatureLogTime;
        public Dictionary<string, DateTime> lastSpotPriceExecute = new Dictionary<string, DateTime>();
        public Dictionary<string, DateTime> lastSpotPriceExecute1 = new Dictionary<string, DateTime>();
        private DateTime lastLogFileCheck;
        private string logfile;
        private Dictionary<string, string> deviceLastStatus = new Dictionary<string, string>();
        private DateTime lastDeviceListGet;
        private DateTime lastSpotPriceDownload;
        private int spotErrorCounter;

        private static List<string> settingsData = null;
        private static DateTime settingsDataSaved = DateTime.MinValue;

        public static bool settingsSaveInProgress = false;

        private static Dictionary<string, bool> deviceStatusMemory = new Dictionary<string, bool>();

        private static DateTime getDeviceListLastExecuted = DateTime.MinValue;

        public Queue<string> logQueue = new Queue<string>();
        private DateTime logLastWriteTime = DateTime.MinValue;

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


            if (settingsData == null)
            {
                if (File.Exists(settingsfile) == false)
                {
                    return "";
                }
                settingsData = File.ReadAllLines(settingsfile).ToList();
            }
            if (string.IsNullOrWhiteSpace(key) == true)
            {
                return "";
            }

            string searchstring = key + "=";
            foreach (var row in settingsData)
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
            int counter = 20;
            while (settingsSaveInProgress == true)
            {
                System.Threading.Thread.Sleep(100);
                counter--;
                if (counter < 1)
                {
                    break;
                }
            }
            settingsSaveInProgress = true;
            if (string.IsNullOrWhiteSpace(settingsfile))
            {
                getSettingsFile();
            }
            if (string.IsNullOrWhiteSpace(settingsfile))
            {
                return;
            }

            if (settingsData == null)
            {
                readSetting(null);
            }


            if (string.IsNullOrWhiteSpace(key) == false)
            {
                value = value.Replace("\n", linefeed);
                value = value.Replace("\r", "");

                string searchstring = key + "=";
                var q = (from c1 in settingsData where c1.StartsWith(searchstring) select c1).FirstOrDefault();
                if (q != null)
                {
                    settingsData.Remove(q);
                }


                //if (File.Exists(settingsfile) == true)
                //{
                //    var rows = File.ReadAllLines(settingsfile);

                //    foreach (var row in rows)
                //    {
                //        if (row.StartsWith(searchstring) == false)
                //        {
                //            sb.AppendLine(row);
                //        }
                //    }
                //}
                if (string.IsNullOrWhiteSpace(value) == false)
                {
                    settingsData.Add(searchstring + value);
                    //sb.AppendLine(searchstring + value);
                }
            }
            if (settingsDataSaved < DateTime.Now.AddMinutes(-10))
            {
                var sb = new StringBuilder();
                foreach (var row in settingsData)
                {
                    sb.AppendLine(row);
                }
                File.WriteAllText(settingsfile, sb.ToString());
                settingsDataSaved = DateTime.Now;
            }
            settingsSaveInProgress = false;
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

            logfile = Path.Combine(Application.StartupPath, "cozifyWindows.log");
            lastLogFileCheck = DateTime.MinValue;

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
            ruuviSensorLastTouch = new Dictionary<string, DateTime>();

            var ip_address_mode = readSetting("ip_address_mode");

            int item_counter = 0;
            foreach (var item in comboBoxIpAddressMode.Items)
            {
                if (item.ToString() == ip_address_mode)
                {
                    comboBoxIpAddressMode.SelectedIndex = item_counter;
                }
                item_counter++;
            }

            if (comboBoxIpAddressMode.SelectedIndex == -1)
            {
                comboBoxIpAddressMode.SelectedIndex = 0;
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

            temperatureLogFile = Path.Combine(Application.StartupPath, "temperatures.txt");

            var devices_setting = readSetting("devices");
            if (string.IsNullOrWhiteSpace(devices_setting) == false)
            {
                parsedevicelist(devices_setting);
            }

            var temperature_log_seconds_string = readSetting("temperature_log_seconds");
            int.TryParse(temperature_log_seconds_string, out temperature_log_seconds);

            var ruuvisensors = readSetting("mqtt_topics");
            //ruuvi/XX:XX:XX:XX:XX:XX
            var broker = readSetting("mqtt_broker");
            if (string.IsNullOrWhiteSpace(ruuvisensors) == false && string.IsNullOrWhiteSpace(broker) == false)
            {
                await StartMqttClient();
            }

            timer2.Enabled = true;
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
                await getDeviceList();
                await getHubApiVersion();
                await getDeviceList();
            }
        }

        public void log(string text, bool printScreen = true)
        {
            string logline = DateTime.Now.ToString() + " : " + text + Environment.NewLine;

            if (printScreen)
            {
                textBoxLog.AppendText(logline);
            }
            logQueue.Enqueue(logline);



            if (logLastWriteTime > DateTime.Now.AddSeconds(-10))
            {
                return;
            }

            var sb = new StringBuilder();
            while (logQueue.Count > 0)
            {
                sb.AppendLine(logQueue.Dequeue());
            }


            try
            {
                System.IO.File.AppendAllText(logfile, sb.ToString());
                logLastWriteTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                textBoxLog.AppendText("log exception: " + ex.ToString() + Environment.NewLine);
            }
            finally { }

            //truncate log file
            if (lastLogFileCheck < DateTime.Now.AddHours(-1))
            {

                var fi = new FileInfo(logfile);
                if (fi.Length > 100 * 1024 * 1024)
                {
                    var old_logfile = logfile + ".old.log";
                    if (File.Exists(old_logfile))
                    {
                        File.Delete(old_logfile);
                    }
                    File.Move(logfile, old_logfile);
                    lastLogFileCheck = DateTime.Now;
                }

                if (textBoxLog.Text.Length > 60000)
                {
                    textBoxLog.Text = textBoxLog.Text.Substring(50000);
                }
            }

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
                log("HttpPost " + url, false);
                UTF8Encoding encoding = new UTF8Encoding();
                string postData1 = postData;
                byte[] data = encoding.GetBytes(postData1);

                HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;

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

                log("HttpPost result: " + result, false);
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

        private async Task<string> HttpGet(string url, bool logging = true)
        {
            try
            {
                if (logging)
                {
                    log("HttpGet " + url, false);
                }

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
                if (logging)
                {
                    log("HttpGet result: " + result, false);
                }
                return result;
            }
            catch (Exception ex)
            {
                string msg = "ERROR " + ex.ToString();
                log("HttpGet:" + msg);
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
            if (getDeviceListLastExecuted > DateTime.Now.AddSeconds(-30))
            {
                return;
            }
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

            var json = await HttpGet(url, false);

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
                    if (cozify_device.state.isOn != null)
                    {
                        device.isOn = cozify_device.state.isOn;
                    }

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

            if (comboBoxIpAddressMode.SelectedItem.ToString() != "API")
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
            return await deviceControl(id, true);
        }

        private async Task<string> deviceOFF(string id)
        {
            return await deviceControl(id, false);
        }

        private async Task<string> deviceControl(string id, bool DEVICE_ON)
        {
            if (listBox1.Items.Count == 0)
            {
                await getDeviceList();
            }

            if (string.IsNullOrWhiteSpace(api_version))
            {
                await getHubApiVersion();
            }

            string device_cmd = "CMD_DEVICE_ON";
            if (DEVICE_ON == false)
            {
                device_cmd = "CMD_DEVICE_OFF";
            }

            string data = "[{\"id\":\"" + id + "\",\"type\":\"" + device_cmd + "\"}]";
            string url = "https://api.cozify.fi/ui/0.2/hub/remote/cc/" + api_version + "/devices/command";

            if (string.IsNullOrWhiteSpace(lan_ip) == false)
            {
                url = "http://" + lan_ip + ":8893/cc/" + api_version + "/devices/command";
            }

            log("deviceControl: " + device_cmd + ": " + id);

            string device_name = id;
            var q = (from c1 in deviceList where c1.id == id select c1).FirstOrDefault();
            if (q != null)
            {
                device_name = q.name;
            }

            deviceLastStatus.TryGetValue(id, out string laststatus);
            if (laststatus != device_cmd)
            {
                deviceLastStatus.Remove(id);
                deviceLastStatus.Add(id, device_cmd);
            }
            return await HttpPost(url, data, "PUT");
        }

        private void buttonTempSensors_Click(object sender, EventArgs e)
        {
            var form2 = new FormTempSensors();
            form2.Show();
        }

        private string dateString(DateTime? date1 = null)
        {
            if (date1 == null)
            {
                date1 = DateTime.Now;
            }
            return date1.Value.Year.ToString("0000") + "-" + date1.Value.Month.ToString("00") + "-" + date1.Value.Day.ToString("00") + " " + date1.Value.Hour.ToString("00") + ":" + date1.Value.Minute.ToString("00") + ":" + date1.Value.Second.ToString("00");
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
            if (lastSpotPriceDownload > DateTime.Now.AddMinutes(-30))
            {
                log("downloadSpotPrices last downloaded " + lastSpotPriceDownload.ToString() + ". Wait at least 30 min before retry");
                return "";
            }
            string data = "";
            var w = new WebClient();
            try
            {
                data = w.DownloadString("https://api.spot-hinta.fi/TodayAndDayForward");
                log("downloadSpotPrices response: " + data);
                saveSetting("spot_price_list", data);
            }
            catch (Exception ex)
            {
                log("downloadSpotPrices error " + ex.ToString());
            }
            finally { }
            lastSpotPriceDownload = DateTime.Now;
            return data;
        }

        private void buttonElectricityPrices_Click(object sender, EventArgs e)
        {
            var prices = downloadSpotPrices();
            parseSpotPrices(prices);
        }

        private void buttonSpotPriceControl_Click(object sender, EventArgs e)
        {
            var form3 = new FormSpotPrices();
            form3.Show();
        }

        private void comboBoxIpAddressType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (readSetting("ip_address_mode") != comboBoxIpAddressMode.SelectedItem.ToString())
            {
                saveSetting("ip_address_mode", comboBoxIpAddressMode.SelectedItem.ToString());
            }
        }

        private void textBoxEmail_Leave_1(object sender, EventArgs e)
        {
            saveEmail(textBoxEmail.Text);
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
        }

        public async Task StartMqttClient()
        {

            //var mqttFactory = new MqttFactory(new ConsoleLogger());
            var mqttFactory = new MqttFactory();

            var broker = readSetting("mqtt_broker");
            if (string.IsNullOrWhiteSpace(broker) == true)
            {
                MessageBox.Show("missing configuration: mqtt server address");
                return;
            }
            int? broker_port = null;
            var broker_address = broker;
            if (broker.Contains(":"))
            {
                var splitted = broker.Split(':');
                broker_address = splitted[0];
                if (int.TryParse(splitted[1], out int port_number) == true)
                {
                    broker_port = port_number;
                }
            }
            var mqttClient = mqttFactory.CreateMqttClient();
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(broker_address, broker_port)
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
            foreach (var sensor in sensors)
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

        private async void buttonStartMqttClient_Click(object sender, EventArgs e)
        {
            await StartMqttClient();
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

        private void telldus(bool turnOn, string deviceId)
        {
            log("telldus turnOn: " + turnOn.ToString() + ". id:" + deviceId);
            try
            {
                var p = new System.Diagnostics.Process();
                string action = "Off";
                if (turnOn == true)
                {
                    action = "On";
                }
                p.StartInfo.Arguments = "id=" + deviceId + " " + action;
                p.StartInfo.FileName = "telldus.exe";
                p.StartInfo.WorkingDirectory = Application.StartupPath;
                p.Start();
            }
            catch (Exception ex)
            {
                log("telldus " + ex.ToString(), true);
            }
            finally { }
        }

        private async void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            timer2.Interval = 30000;


            await getDeviceList();

            var devicecontrol = new List<string>();

            //temperature logging
            if (temperature_log_seconds != 0 && lastTemperatureLogTime < DateTime.Now.AddSeconds(temperature_log_seconds * -1))
            {
                var selected_temp_sensors_list = readSetting("selected_temp_sensors").Split('§');

                string date = dateString();

                var sb = new StringBuilder();

                foreach (var sensor in selected_temp_sensors_list)
                {
                    if (string.IsNullOrWhiteSpace(sensor) == true)
                    {
                        continue;
                    }
                    var dev = (from c1 in deviceList where c1.id == sensor select c1).FirstOrDefault();
                    if (dev != null && dev.temperature.HasValue)
                    {
                        var last_seen = Convert.ToDouble(dev.lastSeen);
                        last_seen = last_seen / 1000;
                        var last_seen_date_utc = UnixTimeStampToDateTime(last_seen);
                        var last_seen_date_local = last_seen_date_utc.ToLocalTime();
                        var last_seen_datetime_string = dateString(last_seen_date_utc);
                        sb.AppendLine(date + "\t" + dev.id + "\t" + last_seen_datetime_string + "\t" + dev.temperature.ToString() + "\t" + dev.name);
                    }
                }

                System.IO.File.AppendAllText(temperatureLogFile, sb.ToString());

                log("Writing temperature log." + sb.ToString());
                lastTemperatureLogTime = DateTime.Now;
            }




            //spot prices

            var spot_price_controlled_devices = readSetting("spot_price_controlled_devices");

            var spot_device_list = spot_price_controlled_devices.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var row in spot_device_list)
            {
                if (string.IsNullOrWhiteSpace(row) == true)
                {
                    continue;
                }

                var data = row.Split(new string[] { dataSeparator }, StringSplitOptions.None);
                var device_id = data[0];

                var cheapesthours = 0;
                double maxprice = 0;
                string timer_spot_price_seconds_string = "60";
                try
                {
                    timer_spot_price_seconds_string = data[3];
                }
                catch { }
                finally { }

                int.TryParse(timer_spot_price_seconds_string, out int timer_spot_price_seconds);




                lastSpotPriceExecute1.TryGetValue(device_id, out DateTime lastSpotPriceExecuteTime1);






                string comboBoxDeviceAction = "";

                try
                {
                    comboBoxDeviceAction = data[4];
                }
                catch { }
                finally { }


                string deviceactionDevice = "";

                try
                {
                    deviceactionDevice = data[5];
                }
                catch { }
                finally { }

                string deviceactionState = "";

                try
                {
                    deviceactionState = data[6];
                }
                catch { }
                finally { }

                if (string.IsNullOrWhiteSpace(comboBoxDeviceAction) == false
                    &&
                    string.IsNullOrWhiteSpace(deviceactionDevice) == false
                    &&
                    string.IsNullOrWhiteSpace(deviceactionState) == false
                    )
                {
                    var device2 = getDevice(deviceactionDevice);
                    if (device2 != null)
                    {
                        if (deviceactionState == "ON")
                        {
                            if (device2.isOn == true)
                            {
                                if (comboBoxDeviceAction == "ON")
                                {
                                    devicecontrol.Add(device_id);
                                    if (lastSpotPriceExecuteTime1 > DateTime.Now.AddSeconds(timer_spot_price_seconds * -1))
                                    {
                                        continue;
                                    }

                                    lastSpotPriceExecute1.Remove(device_id);
                                    lastSpotPriceExecute1.Add(device_id, DateTime.Now);

                                    await deviceControl(device_id, true);
                                    continue;
                                }
                                if (comboBoxDeviceAction == "OFF")
                                {
                                    devicecontrol.Add(device_id);
                                    if (lastSpotPriceExecuteTime1 > DateTime.Now.AddSeconds(timer_spot_price_seconds * -1))
                                    {
                                        continue;
                                    }

                                    lastSpotPriceExecute1.Remove(device_id);
                                    lastSpotPriceExecute1.Add(device_id, DateTime.Now);

                                    await deviceControl(device_id, false);
                                    continue;
                                }

                            }
                        }

                        if (deviceactionState == "OFF")
                        {
                            if (device2.isOn == false)
                            {
                                if (comboBoxDeviceAction == "ON")
                                {
                                    devicecontrol.Add(device_id);
                                    if (lastSpotPriceExecuteTime1 > DateTime.Now.AddSeconds(timer_spot_price_seconds * -1))
                                    {
                                        continue;
                                    }

                                    lastSpotPriceExecute1.Remove(device_id);
                                    lastSpotPriceExecute1.Add(device_id, DateTime.Now);
                                    await deviceControl(device_id, true);
                                    continue;
                                }
                                if (comboBoxDeviceAction == "OFF")
                                {
                                    devicecontrol.Add(device_id);
                                    if (lastSpotPriceExecuteTime1 > DateTime.Now.AddSeconds(timer_spot_price_seconds * -1))
                                    {
                                        continue;
                                    }

                                    lastSpotPriceExecute1.Remove(device_id);
                                    lastSpotPriceExecute1.Add(device_id, DateTime.Now);
                                    await deviceControl(device_id, false);
                                    continue;
                                }
                            }
                        }
                    }
                }

                string comboBoxDeviceAction2 = "";

                try
                {
                    comboBoxDeviceAction2 = data[7];
                }
                catch { }
                finally { }


                string deviceactionDevice2 = "";

                try
                {
                    deviceactionDevice2 = data[8];
                }
                catch { }
                finally { }

                string deviceactionState2 = "";

                try
                {
                    deviceactionState2 = data[9];
                }
                catch { }
                finally { }



                string telldusDeviceId = "";

                try
                {
                    telldusDeviceId = data[10];
                }
                catch { }
                finally { }



                if (string.IsNullOrWhiteSpace(telldusDeviceId) == false)
                {

                    var device2 = getDevice(device_id);
                    if (device2 != null)
                    {
                        bool executeTelldus = true;
                        if (deviceStatusMemory.TryGetValue(device_id, out bool oldStatus))
                        {
                            if (oldStatus == device2.isOn)
                            {
                                executeTelldus = false;
                            }
                        }
                        if (executeTelldus == true)
                        {
                            telldus(device2.isOn, telldusDeviceId);
                            deviceStatusMemory.Remove(device_id);
                            deviceStatusMemory.Add(device_id, device2.isOn);
                        }
                    }
                }



                if (string.IsNullOrWhiteSpace(comboBoxDeviceAction2) == false
                    &&
                    string.IsNullOrWhiteSpace(deviceactionDevice2) == false
                    &&
                    string.IsNullOrWhiteSpace(deviceactionState2) == false
                    )
                {
                    var device2 = getDevice(deviceactionDevice2);
                    if (device2 != null)
                    {
                        if (deviceactionState2 == "ON")
                        {
                            if (device2.isOn == true)
                            {
                                if (comboBoxDeviceAction2 == "ON")
                                {
                                    devicecontrol.Add(device_id);
                                    if (lastSpotPriceExecuteTime1 > DateTime.Now.AddSeconds(timer_spot_price_seconds * -1))
                                    {
                                        continue;
                                    }

                                    lastSpotPriceExecute1.Remove(device_id);
                                    lastSpotPriceExecute1.Add(device_id, DateTime.Now);

                                    await deviceControl(device_id, true);
                                    continue;
                                }
                                if (comboBoxDeviceAction2 == "OFF")
                                {
                                    devicecontrol.Add(device_id);
                                    if (lastSpotPriceExecuteTime1 > DateTime.Now.AddSeconds(timer_spot_price_seconds * -1))
                                    {
                                        continue;
                                    }

                                    lastSpotPriceExecute1.Remove(device_id);
                                    lastSpotPriceExecute1.Add(device_id, DateTime.Now);

                                    await deviceControl(device_id, false);
                                    continue;
                                }

                            }
                        }

                        if (deviceactionState2 == "OFF")
                        {
                            if (device2.isOn == false)
                            {
                                if (comboBoxDeviceAction2 == "ON")
                                {
                                    devicecontrol.Add(device_id);
                                    if (lastSpotPriceExecuteTime1 > DateTime.Now.AddSeconds(timer_spot_price_seconds * -1))
                                    {
                                        continue;
                                    }

                                    lastSpotPriceExecute1.Remove(device_id);
                                    lastSpotPriceExecute1.Add(device_id, DateTime.Now);
                                    await deviceControl(device_id, true);
                                    continue;
                                }
                                if (comboBoxDeviceAction2 == "OFF")
                                {
                                    devicecontrol.Add(device_id);
                                    if (lastSpotPriceExecuteTime1 > DateTime.Now.AddSeconds(timer_spot_price_seconds * -1))
                                    {
                                        continue;
                                    }

                                    lastSpotPriceExecute1.Remove(device_id);
                                    lastSpotPriceExecute1.Add(device_id, DateTime.Now);
                                    await deviceControl(device_id, false);
                                    continue;
                                }
                            }
                        }
                    }
                }



                lastSpotPriceExecute.TryGetValue(device_id, out DateTime lastSpotPriceExecuteTime);


                if (lastSpotPriceExecuteTime > DateTime.Now.AddSeconds(timer_spot_price_seconds * -1))
                {
                    continue;
                }





                if (timer_spot_price_seconds == 0)
                {
                    continue;
                }




                if (string.IsNullOrWhiteSpace(spot_price_controlled_devices))
                {
                    continue;
                }


                if (devicecontrol.Contains(device_id))
                {
                    continue;
                }


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

                var dev = (from c1 in deviceList where c1.id == device_id select c1).FirstOrDefault();

                if (dev == null)
                {
                    continue;
                }

                if (dev.type == "POWER_SOCKET")
                {
                    var spotquery = (from c1 in spotPriceList
                                     where
                                     c1.date.Year == DateTime.Now.Year
                                     && c1.date.Month == DateTime.Now.Month
                                     && c1.date.Day == DateTime.Now.Day
                                     && c1.date.Hour == DateTime.Now.Hour
                                     select c1).FirstOrDefault();

                    bool turnOffByCheapestHours = false;
                    bool turnOffByPrice = false;

                    if (spotquery == null)
                    {

                        spotErrorCounter++;
                        var prices = downloadSpotPrices();
                        parseSpotPrices(prices);
                        if (spotErrorCounter > 20)
                        {
                            log("lets control devices without spot prices");
                        }
                        else
                        {
                            continue;
                        }
                    }
                    spotErrorCounter = 0;
                    log("spot PriceWithTax:" + spotquery.PriceWithTax.ToString());
                    log("maxprice:" + maxprice.ToString());
                    log("spot price rank:" + spotquery.rank.ToString());
                    log("cheapset hours:" + cheapesthours.ToString());


                    if (cheapesthours > 0)
                    {
                        if (spotquery.rank > cheapesthours)
                        {
                            turnOffByCheapestHours = true;
                        }
                    }
                    else
                    {
                        turnOffByCheapestHours = true;
                    }

                    if (maxprice > 0)
                    {
                        if (spotquery.PriceWithTax > maxprice)
                        {

                            turnOffByPrice = true;
                        }
                    }
                    else
                    {
                        turnOffByPrice = true;
                    }


                    if (turnOffByCheapestHours && turnOffByPrice)
                    {
                        if (turnOffByCheapestHours)
                        {
                            log("Turn device [" + dev.name + "] off based on spot rank [" + spotquery.rank.ToString() + "]");
                        }

                        if (turnOffByPrice)
                        {
                            log("Turn device [" + dev.name + "] off based on spot price [" + spotquery.PriceWithTax.ToString() + "]");
                        }

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

                lastSpotPriceExecute.Remove(device_id);

                lastSpotPriceExecute.Add(device_id, DateTime.Now);
            }



            //ruuvi
            if (string.IsNullOrWhiteSpace(log_string) == false)
            {
                log(log_string, false);
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
                var ruuvi_datetime = UnixTimeStampToDateTime(unix);
                string timestamp = ruuvi_datetime.Year.ToString() + "-" + ruuvi_datetime.Month.ToString("00") + "-" + ruuvi_datetime.Day.ToString("00") + " " + ruuvi_datetime.Hour.ToString() + ":" + ruuvi_datetime.Minute.ToString("00") + ":" + ruuvi_datetime.Second.ToString("00");
                log(@"Received payload
   timestamp: " + timestamp + @"
   temperature: " + temperature + @"
   sensor id: " + sensor_id, false);

                //  log("Received MQTT message " + sensor_id);


                var sensors_string = readSetting("mqtt_topics");

                var sensors = sensors_string.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var sensor in sensors)
                {
                    string configuration_sensor = sensor;
                    if (sensor.Contains("/") == true)
                    {
                        var dataArray = sensor.Split('/');
                        configuration_sensor = dataArray[1];
                    }
                    configuration_sensor = configuration_sensor.Replace(":", "");
                    configuration_sensor = configuration_sensor.ToUpper();

                    if (configuration_sensor != sensor_id)
                    {
                        continue;
                    }

                    string rules_data = readSetting("ruuvitag_" + sensor + "_RULES");

                    if (string.IsNullOrWhiteSpace(rules_data) == true)
                    {
                        continue;
                    }
                    var rules = rules_data.Split(new string[] { dataSeparator }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var rule in rules)
                    {
                        string settingname = "ruuvitag_" + sensor + "_" + rule + "_";
                        string do_not_touch_seconds_string = readSetting(settingname + "donottouch");
                        if (int.TryParse(do_not_touch_seconds_string, out int do_not_touch_seconds) == false)
                        {
                            log("timer2_Tick ruuvi error: unable to parse setting (" + settingname + "donottouch) value (" + do_not_touch_seconds_string + ") to int");
                            continue;
                        }

                        ruuviSensorLastTouch.TryGetValue(settingname, out DateTime last_touch);


                        if (last_touch.AddSeconds(do_not_touch_seconds) > DateTime.Now)
                        {
                            //                            log("timer2_Tick ruuvi skip rule because last touch was " + dateString(last_touch));
                            continue;
                        }

                        var value1 = readSetting(settingname + "value1");


                        var temperature_setting = readSetting(settingname + "temperature");
                        var temperature_number = Convert.ToDouble(temperature_setting);

                        var selectedDevice = readSetting(settingname + "device");
                        var deviceAction = readSetting(settingname + "deviceaction");

                        if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                        {
                            if (string.IsNullOrWhiteSpace(readSetting(settingname + "MO")))
                            {
                                continue;
                            }
                        }
                        if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
                        {
                            if (string.IsNullOrWhiteSpace(readSetting(settingname + "TU")))
                            {
                                continue;
                            }
                        }
                        if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
                        {
                            if (string.IsNullOrWhiteSpace(readSetting(settingname + "WE")))
                            {
                                continue;
                            }
                        }
                        if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
                        {
                            if (string.IsNullOrWhiteSpace(readSetting(settingname + "TH")))
                            {
                                continue;
                            }
                        }
                        if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                        {
                            if (string.IsNullOrWhiteSpace(readSetting(settingname + "FR")))
                            {
                                continue;
                            }
                        }
                        if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                        {
                            if (string.IsNullOrWhiteSpace(readSetting(settingname + "SA")))
                            {
                                continue;
                            }
                        }
                        if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                        {
                            if (string.IsNullOrWhiteSpace(readSetting(settingname + "SU")))
                            {
                                continue;
                            }
                        }

                        var timestart = readSetting(settingname + "timestart");



                        var datasplit_start = timestart.Split(':');


                        DateTime d1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                         Convert.ToInt32(datasplit_start[0]),
                        Convert.ToInt32(datasplit_start[1]),
                        0);


                        if (DateTime.Now < d1)
                        {
                            continue;
                        }

                        var timeend = Form1.readSetting(settingname + "timeend");
                        var datasplit_end = timeend.Split(':');

                        var end_hour = Convert.ToInt32(datasplit_end[0]);
                        DateTime d2 = DateTime.MaxValue;
                        if (end_hour < 24)
                        {
                            d2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                              end_hour,
                             Convert.ToInt32(datasplit_end[1]),
                             0);
                        }

                        if (DateTime.Now > d2)
                        {
                            continue;
                        }

                        if (value1 == "<")
                        {
                            if (temperature_number > temperature)
                            {
                                if (deviceAction == "ON")
                                {
                                    if (devicecontrol.Contains(selectedDevice))
                                    {
                                        continue;
                                    }
                                    log("timer2_Tick ruuvi execute rule [" + rule + "] device (" + selectedDevice + ") on. temp:" + temperature.ToString());
                                    await deviceON(selectedDevice);
                                }
                                if (deviceAction == "OFF")
                                {
                                    if (devicecontrol.Contains(selectedDevice))
                                    {
                                        continue;
                                    }
                                    log("timer2_Tick ruuvi execute rule [" + rule + "] device (" + selectedDevice + ") off. temp:" + temperature.ToString());
                                    await deviceOFF(selectedDevice);
                                }
                            }
                        }
                        if (value1 == ">")
                        {
                            if (temperature_number < temperature)
                            {
                                if (deviceAction == "ON")
                                {
                                    if (devicecontrol.Contains(selectedDevice))
                                    {
                                        continue;
                                    }
                                    log("timer2_Tick ruuvi execute rule [" + rule + "] device (" + selectedDevice + ") on. temp:" + temperature.ToString());
                                    await deviceON(selectedDevice);
                                }
                                if (deviceAction == "OFF")
                                {
                                    if (devicecontrol.Contains(selectedDevice))
                                    {
                                        continue;
                                    }
                                    log("timer2_Tick ruuvi execute rule [" + rule + "] device (" + selectedDevice + ") off. temp:" + temperature.ToString());
                                    await deviceOFF(selectedDevice);
                                }
                            }
                        }

                        ruuviSensorLastTouch.Remove(settingname);

                        ruuviSensorLastTouch.Add(settingname, DateTime.Now);
                    }
                }
            }//end of while (mqtt_payload.Count > 0)

            //end of timers

            timer2.Enabled = true;
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            //form:816,500
            //textbox:331,364

            textBoxLog.Width = 331 + (this.Width - 816);
            textBoxLog.Height = 364 + (this.Height - 500);

        }

        private Device getDevice(string id)
        {
            return (from c1 in deviceList where c1.id == id select c1).FirstOrDefault();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            settingsDataSaved = DateTime.MinValue;
            logLastWriteTime = DateTime.MinValue;
            saveSetting(null, null);
            log("shut down");
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
        public bool isOn { get; set; }
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
