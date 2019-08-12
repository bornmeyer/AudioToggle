using System.Collections;
using System.Collections.Generic;

namespace AudioToggle.Models
{
    public interface IAudioDeviceManager
    {
        IEnumerable<AudioDevice> GetAllDevices();

        void SwitchToDevice(AudioDevice audioDevice);

        void SwitchToDevice(string deviceId);
    }
}