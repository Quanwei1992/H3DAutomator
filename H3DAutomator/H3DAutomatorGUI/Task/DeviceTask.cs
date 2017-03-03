using Automator;
using Managed.Adb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automator
{

    public class TaskResult
    {
        public bool ok = true;
        public string Msg = string.Empty;
    }

    public abstract class DeviceTask
    {
        public abstract TaskResult Run(Managed.Adb.Device adbDevice);
    }

}

