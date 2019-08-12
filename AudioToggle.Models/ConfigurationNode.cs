using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public class ConfigurationNode
    {
        // Properties

        public String AudioDeviceId
        {
            get;
            set;
        }

        public PresentationDisplayMode PresentationDisplayMode
        {
            get;
            set;
        }

        // Constructors

        public ConfigurationNode(AudioDevice audioDevice, DisplayMode displayMode)
        {
            AudioDeviceId = audioDevice.DeviceId;
            PresentationDisplayMode = displayMode.PresentationDisplayMode;
        }

        public ConfigurationNode()
        {

        }
    }
}
