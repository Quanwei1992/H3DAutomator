using Managed.Adb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automator
{
    public class LaunchAppReceiver : MultiLineReceiver
    {
        protected override void ProcessNewLines(string[] lines)
        {
            int a = 10;
            a++;

        }
    }
}
