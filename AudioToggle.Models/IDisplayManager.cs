using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public interface IDisplayManager
    {
        event Action<DisplayMode> DisplayModeChanged;

        IEnumerable<DisplayMode> GetAllDisplayModes();

        void StartPolling(Int32 pollingIntervalInMs = 50);
        void StopPolling();

        DisplayMode GetCurrentDisplayMode();
    }
}
