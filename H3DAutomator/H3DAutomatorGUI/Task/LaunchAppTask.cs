using Managed.Adb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automator
{
    public class LaunchAppTask : DeviceTask
    {
        private string mPackgeName;
        private string mLaunchActivity;

        public LaunchAppTask(string packgeName,string launchActivity)
        {
            mPackgeName = packgeName;
            mLaunchActivity = launchActivity;
        }

        public override TaskResult Run(Managed.Adb.Device adbDevice)
        {
            TaskResult result = new TaskResult();
            String cmd = String.Format("am start {0}/{1}",mPackgeName,mLaunchActivity);
            var receiver = new LaunchAppReceiver();
            adbDevice.ExecuteShellCommand(cmd, receiver);
            //if (!String.IsNullOrEmpty(receiver.ErrorMessage)) {
            //    result.ok = false;
            //    result.Msg = receiver.ErrorMessage;
            //}
            return result;
        }

        public override string ToString()
        {
            return string.Format("LaunchApk {0}/{1}",mPackgeName,mLaunchActivity);
        }
    }
}
