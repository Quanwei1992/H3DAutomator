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
            Queue<DeviceTask> queue = new Queue<DeviceTask>();

            string apkPath = "d:/wetest.apk";
            //queue.Enqueue(new InstallTask(apkPath));
            //APKInfo info = APKInfo.ParseAPK(apkPath);
            //queue.Enqueue(new LaunchAppTask(info.PackgeName, info.LauncherActivity));
            queue.Enqueue(new WeTestTask("E:/Python27/python.exe",
                "E:/GitRepos/H3DAutomator/H3DAutomator/wetest","com.tencent.wetest.demo",
                50032,19009));
            var devices = DeviceManager.Instance.Devices;
            foreach (var device in devices) {
                device.TaskMgr.RunTasks(queue.ToArray());
            }
        }
    }
}
