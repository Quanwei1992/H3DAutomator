using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Managed.Adb;
using Managed.Adb.Exceptions;
using System.IO;
using System.Net;

namespace Automator
{
    public class InstallTask : DeviceTask
    {
        private string mPackgeLocation;
        public InstallTask(string packgeLocation)
        {
            mPackgeLocation = packgeLocation;
        }

        public override TaskResult Run(Managed.Adb.Device adbDevice)
        {
            TaskResult result = new TaskResult();

            //un install
            var apkInfo = APKInfo.ParseAPK(mPackgeLocation);
            try {
                if (adbDevice.PackageManager.Exists(apkInfo.PackgeName)) {
                    adbDevice.UninstallPackage(apkInfo.PackgeName);
                }
                
            } catch {

            }

            LogWrapper.LogInfo("CanuSU:" + adbDevice.SerialNumber + ":" + adbDevice.CanSU());
            string remoteFilePath = adbDevice.SyncPackageToDevice(mPackgeLocation);
            InstallReceiver receiver = new InstallReceiver();
            String cmd = String.Format("pm install {1}{0}", remoteFilePath, true ? "-r " : String.Empty);
            if (adbDevice.CanSU()) {
                adbDevice.ExecuteRootShellCommand(cmd, receiver);
            } else {
                adbDevice.ExecuteShellCommand(cmd,receiver);
            }

            if (!String.IsNullOrEmpty(receiver.ErrorMessage)) {
                result.ok = false;
                result.Msg = receiver.ErrorMessage;
            }
            return result;        
        }

        public override string ToString()
        {
            return string.Format("InstallApk {0}", mPackgeLocation);
        }
    }

   
}
