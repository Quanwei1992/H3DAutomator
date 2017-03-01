using Managed.Adb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automator
{
    public class TaskManager
    {
        private Managed.Adb.Device mDevice;

        private DeviceTask[] mDeviceTasks = null;
        private Task mTask = null;
        private int mCurrentTaskIndex = 0;

       
        public Action<DeviceTask> TaskStart;
        public Action<DeviceTask,TaskResult> TaskEnd;
        
        public DeviceTask[] Tasks
        {
            get {
                return mDeviceTasks;
            }
        }
        
        public int RunningTaskIndex
        {
            get {
                return mCurrentTaskIndex;
            }
        }


        public TaskManager(Managed.Adb.Device device) {
            mDevice = device;
        }


        public void RunTasks(DeviceTask[] tasks)
        {
            if (mTask!=null){
                return;
            }
            mDeviceTasks = tasks;
            mTask = new Task(_run, TaskCreationOptions.LongRunning);
            mTask.Start();
        }


        public void StopAll()
        {
            if (mTask != null) {
               
            }
        }

        void _run()
        {
            for (int i = 0; i < mDeviceTasks.Length; i++) {
                mCurrentTaskIndex = i;
                mDeviceTasks[i].ADBDevice = mDevice;
                TaskStart?.Invoke(mDeviceTasks[i]);
                LogWrapper.LogInfoFormat("设备[{0}] 开始任务 {1}/{2} {3}",mDevice.SerialNumber,
                    i,mDeviceTasks.Length,mDeviceTasks[i].ToString());
                var result = mDeviceTasks[i].Run();
                TaskEnd?.Invoke(mDeviceTasks[i],result);
                if (!result.ok) {
                    LogWrapper.LogInfoFormat("设备[{0}] 执行任务失败 {1}/{2} {3} 错误信息:{4}", mDevice.SerialNumber,
                        i, mDeviceTasks.Length, mDeviceTasks[i].ToString(), result.Msg);
                } else {
                    LogWrapper.LogInfoFormat("设备[{0}] 完成任务 {1}/{2} {3}",mDevice.SerialNumber,
                    i,mDeviceTasks.Length,mDeviceTasks[i].ToString());
                }
            }

            mDeviceTasks = null;
            mTask = null;
            mCurrentTaskIndex = 0;

            LogWrapper.LogInfoFormat("设备[{0}] 完成所有任务",mDevice.SerialNumber);
        }

    }
}
