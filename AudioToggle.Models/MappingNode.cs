using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public class MappingNode
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

        public MappingNode(AudioDevice audioDevice, DisplayMode displayMode)
        {
            AudioDeviceId = audioDevice.DeviceId;
            PresentationDisplayMode = displayMode.PresentationDisplayMode;
        }

        public MappingNode()
        {

        }
    }
}
