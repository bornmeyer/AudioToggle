using NAudio.CoreAudioApi;
using AudioToggle;
using AudioToggle.Interop.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public class DefaultAudioDeviceManager : IAudioDeviceManager
    {
        public IEnumerable<AudioDevice> GetAllDevices()
        {
            var enumerator = new MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).Select(x => (x.ID, x.FriendlyName, x.DataFlow));
            var defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            foreach (var current in devices)
            {
                yield return new AudioDevice(current.ID, current.FriendlyName, current.DataFlow, current.ID == defaultDevice.ID);
            }
        }

        public void SwitchToDevice(AudioDevice audioDevice)
        {
            SwitchToDevice(audioDevice.DeviceId);
            //AudioDeviceSelector.Instance.ResetProcessDeviceConfiguration();
            //AudioSwitcher.Instance.ResetProcessDeviceConfiguration();
            //AudioSwitcher.Instance.SwitchProcessTo(device.ID, AudioToggle.Interop.Enum.ERole.eConsole, (EDataFlow)device.DataFlow, 6172);
            //AudioDeviceSelector.Instance.SwitchProcessTo(audioDevice.DeviceId, AudioToggle.Interop.Enum.ERole.ERole_enum_count, (EDataFlow)audioDevice.DataFlow, 11136);
            //AudioSwitcher.Instance.SwitchProcessTo(deviceId, AudioToggle.Interop.Enum.ERole.ERole_enum_count, AudioToggle.Interop.Enum.EDataFlow.eRender);

        }

        public void SwitchToDevice(string deviceId)
        {
            AudioDeviceSelector.Instance.SwitchTo(deviceId, ERole.ERole_enum_count);
        }
    }
}
