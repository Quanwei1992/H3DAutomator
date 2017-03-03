using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Automator
{
    public class WeTestTask : DeviceTask
    {
        private string mPythonPath;
        private string mWetestRootPath;
        private string mPackgeName;
        private int mEnginePort;
        private int mUIPort;
        private string mQQname;
        private string mQQPwd;
        private string mWechatAccount;
        private string mWechatPwd;
        private string mOtherName;
        private string mOtherPwd;


        static int gEnginePort = 50031;
        static int gUIProt = 19008;

        /// <summary>
        /// new WeTestTask
        /// </summary>
        /// <param name="pythonPath">python所在位置</param>
        /// <param name="wetestRootPath">wetest根目录</param>
        /// <param name="packgeName">游戏包名</param>
        /// <param name="qqname">qq账号，每部手机应该都不一样</param>
        /// <param name="qqpwd">qq密码</param>
        /// <param name="wechataccount">微信账号</param>
        /// <param name="wechatpwd">微信密码</param>
        /// <param name="othername">其他任何账号</param>
        /// <param name="otherpwd">其他任何账号的密码</param>
        public WeTestTask(string pythonPath,string wetestRootPath,string packgeName,
            string qqname = null,string qqpwd = null, string wechataccount = null, string wechatpwd = null, string othername = null, string otherpwd = null)
        {
            mPythonPath = pythonPath;
            mWetestRootPath = wetestRootPath;
            mPackgeName = packgeName;
            mQQname = qqname;
            mQQPwd = qqpwd;
            mWechatAccount = wechataccount;
            mWechatPwd = wechatpwd;
            mOtherName = othername;
            mOtherPwd = otherpwd;
        }


        public override TaskResult Run(Managed.Adb.Device adbDevice)
        {
            TaskResult result = new TaskResult();

            mEnginePort = gEnginePort++;
            mUIPort = gUIProt++;

            string arg = string.Format("main.py --packge={0} --engineport={1} --uiport={2} --serial={3}",
                mPackgeName,mEnginePort,mUIPort,adbDevice.SerialNumber);
            
            var psi = new ProcessStartInfo();
            psi.FileName = mPythonPath;
            psi.WorkingDirectory = mWetestRootPath;
            psi.Arguments = arg;
            psi.UseShellExecute = false;
          
            var p = Process.Start(psi);
            p.WaitForExit();
            return result;
        }
    }
}
