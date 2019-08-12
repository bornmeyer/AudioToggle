using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public class AudioDevice
    {
        // Properties

        public String DeviceId
        {
            get;
            private set;
        }

        public String DeviceName
        {
            get;
            private set;
        }

        internal DataFlow DataFlow
        {
            get;
            private set;
        }

        public Boolean CurrentlyDefault
        {
            get;
            private set;
        }

        // Constructors

        public AudioDevice(String deviceId, String deviceName, DataFlow dataFlow, Boolean currentlyDefault)
        {
            DeviceId = deviceId;
            DeviceName = deviceName;
            DataFlow = dataFlow;
            CurrentlyDefault = currentlyDefault;
        }

        // Methods

        public override string ToString()
        {
            return $"#{DeviceId} - #{DeviceName}";
        }
    }
}
