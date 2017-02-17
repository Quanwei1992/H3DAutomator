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

namespace H3DAutomatorGUI
{
    public partial class MainForm : Form
    {

        AutomatorController automator = new AutomatorController();


        public MainForm()
        {
            InitializeComponent();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            status_lable_adb.Text = "正在初始化....";
            automator.DeviceConnected += AutomatorDeviceConnected;
            automator.DeviceDisconnected += AutomatorDeviceDisconnected;
            automator.DeviceChanged += AutomatorDeviceChanged;
            RunAsync(()=> {
                string adbLocation = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "tools/adb.exe");
                bool ret = automator.Init(adbLocation);
                
                RunInMainthread(()=> {
                    if (ret) {
                        status_lable_adb.Text = "初始化成功";
                        var devices = automator.Devices;
                        foreach (var device in devices) {
                            AutomatorDeviceConnected(device);
                        }

                    } else {
                        status_lable_adb.Text = "初始化失败";
                    }
                });
            });
        }



        void AutomatorDeviceConnected(AutomatorDevice device)
        {
            RunInMainthread(()=> {

                if (listView_devices.FindItemWithText(device.ADBDevice.SerialNumber) != null) {
                    return;
                }

                ListViewItem item = new ListViewItem(device.ADBDevice.SerialNumber);
                item.SubItems.Add(device.ADBDevice.State.ToString());
                listView_devices.Items.Add(item);
            });

            
        }

        void AutomatorDeviceDisconnected(AutomatorDevice device)
        {
            RunInMainthread(()=> {
                var item = listView_devices.FindItemWithText(device.ADBDevice.SerialNumber);
                if (item != null) {
                    listView_devices.Items.Remove(item);
                }
            });

        }

        void AutomatorDeviceChanged(AutomatorDevice device)
        {

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

        }

        private void button_install_Click(object sender, EventArgs e)
        {
            string apkPath = "d:/347259.apk";
            var info = APKInfo.ParseAPK(apkPath);
        }



    }
}
