using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Managed.Adb;
using Managed.Adb.Exceptions;
    public class DeviceLog
    {
        public string s;
        public DateTime date;
    }

    public class AutomatorDevice
    {
        public IDevice ADBDevice { get; set; }
        public readonly List<DeviceLog> Logs = new List<DeviceLog>();
        public Action<DeviceLog> RecvDeviceLog;
       
        public void WriteLog(string str)
        {
            var log = new DeviceLog() {
                s = str,
                date = DateTime.Now
            };

            Logs.Add(log);
            RecvDeviceLog?.Invoke(log);
        }

        public void InstallPackge(string apkLocation, bool forceInstall)
        {

            WriteLog("sync packge to device '" + apkLocation+"'");
            string remoteFilePath = ADBDevice.SyncPackageToDevice(apkLocation);
            WriteLog("install packge " + remoteFilePath);
            InstallReceiver receiver = new InstallReceiver();
            String cmd = String.Format("pm install {1}{0}", remoteFilePath, forceInstall ? "-r " : String.Empty);
            ADBDevice.ExecuteShellCommand(cmd, receiver);
            if (!String.IsNullOrEmpty(receiver.ErrorMessage)) {
                WriteLog("install packge failed " + remoteFilePath+",ErrorMessage:" + receiver.ErrorMessage);
                throw new PackageInstallationException(receiver.ErrorMessage);
            } else {
                WriteLog("install packge successed " + remoteFilePath);
            }
        }




    }
