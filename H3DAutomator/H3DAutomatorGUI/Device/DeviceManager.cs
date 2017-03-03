using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Managed.Adb;
using System.IO;
public class DeviceManager
{
    private AndroidDebugBridge mADB;
    private Dictionary<string,Automator.Device> mDeviceCache = new Dictionary<string, Automator.Device>();

    public Action<Automator.Device> DeviceDisconnected;
    public Action<Automator.Device> DeviceConnected;

    private static DeviceManager mInstance;
    private DeviceManager() { }

    public static DeviceManager Instance {
        get {
            if (mInstance == null) {
                mInstance = new DeviceManager();
            }
            return mInstance;
        }
    }
    


    public Automator.Device[] Devices
    {
        get {
            return mDeviceCache.Values.ToArray();
        }
    }

    public bool Init(string adbLocation)
    {
        mADB = AndroidDebugBridge.CreateBridge(adbLocation, true);
        if (mADB.Start()) {
            RefreshDevices();
            mADB.DeviceConnected += MBridge_DeviceConnected;
            mADB.DeviceDisconnected += MBridge_DeviceDisconnected;
            return true;
        }
        return false;
    }

    public void Stop()
    {
        mADB.Stop();
    }


    public void RefreshDevices()
    {
        var adbDevices = mADB.Devices;
        for (int i = 0; i < adbDevices.Count; i++) {
            if (mDeviceCache.ContainsKey(adbDevices[i].SerialNumber)) {
                mDeviceCache[adbDevices[i].SerialNumber].ADBDevice = adbDevices[i];
            } else {
                mDeviceCache[adbDevices[i].SerialNumber] = new Automator.Device(adbDevices[i]);
            }
        }
        
    }


    #region DeviceEvents
    private void MBridge_DeviceDisconnected(object sender, DeviceEventArgs e)
    {
        if (mDeviceCache.ContainsKey(e.Device.SerialNumber)) {
            var device = mDeviceCache[e.Device.SerialNumber];
            mDeviceCache.Remove(e.Device.SerialNumber);
            DeviceDisconnected?.Invoke(device);
        }
    }



    private void MBridge_DeviceConnected(object sender, DeviceEventArgs e)
    {
        if (!mDeviceCache.ContainsKey(e.Device.SerialNumber)) {
            mDeviceCache[e.Device.SerialNumber] = new Automator.Device(e.Device as Managed.Adb.Device);
            DeviceConnected?.Invoke(mDeviceCache[e.Device.SerialNumber]);
        }

    }
    #endregion


}