using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Managed.Adb;
using System.IO;
using System.Threading;
using Managed.Adb.Exceptions;
using Automator;
using System.Configuration;
namespace H3DAutomatorGUI
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {



            // 读取配置文件
            textBox_python.Text = Config.Read("AppSettings", "Python", "", "./config.ini");
            textBox_wt.Text = Config.Read("AppSettings", "wetest", "", "./config.ini");


            status_lable_adb.Text = "正在初始化....";
            LogWrapper.OnRecvLog += OnRecvLog;
            DeviceManager.Instance.DeviceConnected += DeviceConnected;
            DeviceManager.Instance.DeviceDisconnected += DeviceDisconnected;
            RunAsync(()=> {
                string adbLocation = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "tools/adb.exe");
                bool ret = DeviceManager.Instance.Init(adbLocation);
                
                RunInMainthread(()=> {
                    if (ret) {
                        status_lable_adb.Text = "初始化成功";
                        var devices = DeviceManager.Instance.Devices;
                        foreach (var device in devices) {
                            DeviceConnected(device);
                        }

                    } else {
                        status_lable_adb.Text = "初始化失败";
                    }
                });
            });
        }



        void DeviceConnected(Automator.Device device)
        {
            RunInMainthread(()=> {

                if (listView_devices.FindItemWithText(device.ADBDevice.SerialNumber) != null) {
                    return;
                }

                ListViewItem item = new ListViewItem(device.ADBDevice.SerialNumber);
                item.SubItems.Add(device.ADBDevice.State.ToString());
                item.SubItems.Add(device.ADBDevice.Model.ToString());
                item.SubItems.Add(device.ADBDevice.Product.ToString());
                item.SubItems.Add(device.ADBDevice.DeviceProperty.ToString());
                listView_devices.Items.Add(item);
            });

            
        }

        void DeviceDisconnected(Automator.Device device)
        {
            RunInMainthread(()=> {
                var item = listView_devices.FindItemWithText(device.ADBDevice.SerialNumber);
                if (item != null) {
                    listView_devices.Items.Remove(item);
                }
            });

        }





        void RunAsync(Action action)
        {
            ((Action)(delegate ()
            {
                action?.Invoke();
            })).BeginInvoke(null, null);
        }

        void RunInMainthread(Action action)
        {
            this.BeginInvoke((Action)(delegate ()
            {
                action?.Invoke();
            }));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogWrapper.OnRecvLog -= OnRecvLog;
            System.Environment.Exit(0);

        }
        private void textBox_log_TextChanged(object sender, EventArgs e)
        {

        }

        void OnRecvLog(LogLevel lv, string info)
        {
            string log = string.Format("{0} [{1}]:{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
               lv.ToString(), info);

            RunInMainthread(() => {
                textBox_log.Text += log + "\r\n";
            });
        }

        private void button_install_Click_1(object sender, EventArgs e)
        {
            
          
            button_install.Enabled = false;
            List<DeviceTask> tasks = new List<DeviceTask>();
            string packgeName = "";
            if (radioButton_install.Checked) {
                string apkFilePath = textBox_apk.Text;
                var installTask = new InstallTask(apkFilePath);
                tasks.Add(installTask);
                var info = APKInfo.ParseAPK(apkFilePath);
                packgeName = info.PackgeName;
            } else {
                packgeName = textBox_apk.Text;
            }

            WeTestTask wtTask = new WeTestTask(textBox_python.Text, textBox_wt.Text, packgeName);
            tasks.Add(wtTask);

            var devices = DeviceManager.Instance.Devices;
            int complateCount = 0;
            for (int i = 0; i < devices.Length; i++) {
                var device = devices[i];
                device.TaskMgr.TaskEnd += (task,result) => {
                    if (device.TaskMgr.RunningTaskIndex == device.TaskMgr.Tasks.Length - 1 || !result.ok) {
                        complateCount++;
                        if (complateCount == devices.Length) {
                            LogWrapper.LogInfo("所有任务已完成");
                            RunInMainthread(()=> {
                                button_install.Enabled = true;
                            });
                            
                        }
                    }
                };
                device.TaskMgr.RunTasks(tasks.ToArray());
            }
        }

        private void button_python_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "exe(*.exe)|*.exe";
            ofd.FilterIndex = 0;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK) {
                textBox_python.Text = ofd.FileName;
                Config.Write("AppSettings", "Python", ofd.FileName, "./config.ini");
            }
        }

        private void button_wt_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择WeTest根目录";
            if (dialog.ShowDialog() == DialogResult.OK) {
                string foldPath = dialog.SelectedPath;
                textBox_wt.Text = foldPath;
                Config.Write("AppSettings", "wetest", foldPath, "./config.ini");
            }
        }

        private void button_apk_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "apk(*.apk)|*.apk";
            ofd.FilterIndex = 0;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK) {
                textBox_apk.Text = ofd.FileName;
                if (radioButton_install.Checked) {
                    Config.Write("AppSettings", "apk_path", ofd.FileName, "./config.ini");
                }
            }
        }


        bool firstSelectInstallButton = true;
        

        private void radioButton_install_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_install.Checked) {
                label_title.Text = "APK:";
                textBox_apk.Enabled = false;
                button_apk.Visible = true;
                if (firstSelectInstallButton) {
                    textBox_apk.Text = Config.Read("AppSettings", "apk_path", "", "./config.ini");
                    firstSelectInstallButton = false;
                }
            }
        }
        bool firstSelectOnlyTestButton = true;

        private void radioButton_onlytest_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_onlytest.Checked) {
                label_title.Text = "BundleID";
                textBox_apk.Enabled = true;
                button_apk.Visible = false;
                if (firstSelectOnlyTestButton) {
                    textBox_apk.Text = Config.Read("AppSettings", "apk_bundle_id", "", "./config.ini");
                    firstSelectOnlyTestButton = false;
                }
            }
        }

        private void textBox_apk_TextChanged(object sender, EventArgs e)
        {
            if (radioButton_onlytest.Checked) {
                Config.Write("AppSettings", "apk_bundle_id",textBox_apk.Text, "./config.ini");
            }
        }
    }
}
