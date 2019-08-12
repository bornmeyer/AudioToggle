using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public interface IAudioSwitch
    {
        void Switch(DisplayMode displayMode);

        void UpdateMapping(DisplayMode displayMode, AudioDevice audioDevice);

        Configuration GetConfigMapping();
    }
}
