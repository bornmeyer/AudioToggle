using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public interface IAudioSwitch
    {
        void UpdateMapping(DisplayMode displayMode, AudioDevice audioDevice);

        Mapping GetConfigMapping();
    }
}
