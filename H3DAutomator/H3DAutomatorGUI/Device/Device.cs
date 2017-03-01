using Managed.Adb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automator
{
    public class Device
    {
        private Managed.Adb.Device mADBDevice;
        private TaskManager mTaskManager;

        public TaskManager TaskMgr
        {
            get {
                return mTaskManager;
            }
        }

        public Managed.Adb.Device ADBDevice {
            get {
                return mADBDevice;
            }
            set {
                mADBDevice = value;
            }
        }


        public Device(Managed.Adb.Device adbDevice)
        {
            mADBDevice = adbDevice;
            mTaskManager = new TaskManager(adbDevice);
        }

    }
}
