using AudioToggle.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AudioToggle.Models
{
    public class DefaultDisplayManager : IDisplayManager
    {
        // Events

        public event Action<DisplayMode> DisplayModeChanged;

        // Fields

        private Timer _pollTimer = null;

        // Constructors

        public DefaultDisplayManager()
        {            
            _pollTimer = new Timer();
        }

        // Methods

        public void StartPolling(Int32 pollingIntervalInMs = 50)
        {
            _pollTimer.Elapsed += (sender, e) => Poll();
            _pollTimer.Interval = pollingIntervalInMs;            
            _pollTimer.Start();
        }

        private void Poll()
        {
            OnDisplayModeChanged(ReadPresentationDisplayMode());
        }
        
        private PresentationDisplayMode ReadPresentationDisplayMode()
        {
            
            Int32 numPathArrayElements;
            Int32 numModeInfoArrayElements;

            // query active paths from the current computer.
            if (DisplayWmiApiWrapper.GetDisplayConfigBufferSizes(DisplayWmiApiWrapper.QueryDisplayFlags.OnlyActivePaths, out numPathArrayElements,
                                                           out numModeInfoArrayElements) == 0)
            {
                var pathInfoArray = new DisplayWmiApiWrapper.DisplayConfigPathInfo[numPathArrayElements];
                var modeInfoArray = new DisplayWmiApiWrapper.DisplayConfigModeInfo[numModeInfoArrayElements];
                DisplayWmiApiWrapper.DisplayConfigTopologyId currentTopologyId = DisplayWmiApiWrapper.DisplayConfigTopologyId.Zero;

                var status = DisplayWmiApiWrapper.QueryDisplayConfig(DisplayWmiApiWrapper.QueryDisplayFlags.DatabaseCurrent,
                                        ref numPathArrayElements, pathInfoArray, ref numModeInfoArrayElements,
                                        modeInfoArray, out currentTopologyId);
                return ((PresentationDisplayMode)currentTopologyId);

            }
            return PresentationDisplayMode.Zero;
        }

        public void StopPolling()
        {
            _pollTimer.Stop();
        }

        private void OnDisplayModeChanged(PresentationDisplayMode displayMode)
        {
            DisplayModeChanged?.Invoke(new DisplayMode(displayMode));
        }

        public IEnumerable<DisplayMode> GetAllDisplayModes()
        {
            return Enum.GetValues(typeof(PresentationDisplayMode))
                .Cast<PresentationDisplayMode>()
                .Where(x => x != PresentationDisplayMode.ForceUint32 && x != PresentationDisplayMode.Zero)
                .Select(x => new DisplayMode(x));
        }

        public DisplayMode GetCurrentDisplayMode()
        {
            var current = ReadPresentationDisplayMode();
            return new DisplayMode(current);
        }
    }
}
