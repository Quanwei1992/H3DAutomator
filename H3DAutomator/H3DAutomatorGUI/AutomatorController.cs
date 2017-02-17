using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Managed.Adb;
using System.IO;
public class AutomatorController
{
    private AndroidDebugBridge mBridge;
    private Dictionary<string, AutomatorDevice> mDeviceCache = new Dictionary<string, AutomatorDevice>();

    public Action<AutomatorDevice> DeviceDisconnected;
    public Action<AutomatorDevice> DeviceChanged;
    public Action<AutomatorDevice> DeviceConnected;

    public AutomatorDevice[] Devices
    {
        get {
            return mDeviceCache.Values.ToArray();
        }
    }

    public bool Init(string adbLocation)
    {
        mBridge = AndroidDebugBridge.CreateBridge(adbLocation, true);
        if (mBridge.Start()) {
            RefreshDevices();
            mBridge.DeviceConnected += MBridge_DeviceConnected;
            mBridge.DeviceChanged += MBridge_DeviceChanged;
            mBridge.DeviceDisconnected += MBridge_DeviceDisconnected;
            return true;
        }
        return false;
    }

    private void MBridge_DeviceDisconnected(object sender, DeviceEventArgs e)
    {
        if (mDeviceCache.ContainsKey(e.Device.SerialNumber)){
            var device = mDeviceCache[e.Device.SerialNumber];
            mDeviceCache.Remove(e.Device.SerialNumber);
            DeviceDisconnected?.Invoke(device);
        }
    }

    private void MBridge_DeviceChanged(object sender, DeviceEventArgs e)
    {
        if (mDeviceCache.ContainsKey(e.Device.SerialNumber)) {
            var device = mDeviceCache[e.Device.SerialNumber];
            device.ADBDevice = e.Device;
            DeviceChanged?.Invoke(device);
        }
    }

    private void MBridge_DeviceConnected(object sender, DeviceEventArgs e)
    {
        if (!mDeviceCache.ContainsKey(e.Device.SerialNumber)) {
            AutomatorDevice device = new AutomatorDevice();
            device.ADBDevice = e.Device;
            mDeviceCache[e.Device.SerialNumber] = device;
            DeviceConnected?.Invoke(device);
        }
        
    }

    public void RefreshDevices()
    {
        var adbDevices = mBridge.Devices;
        for (int i = 0; i < adbDevices.Count; i++) {
            if (mDeviceCache.ContainsKey(adbDevices[i].SerialNumber)) {
                mDeviceCache[adbDevices[i].SerialNumber].ADBDevice = adbDevices[i];
            } else {
                var device = new AutomatorDevice();
                device.ADBDevice = adbDevices[i];
                mDeviceCache[device.ADBDevice.SerialNumber] = device;
            }
        }
    }


    public void InstallPackge(Device[] devices,string packgeLocation)
    {
        foreach (var device in devices)
        {

        }
    }


    


}