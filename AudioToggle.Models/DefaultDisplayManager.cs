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

        private readonly IPulseGiver<Int32> _pollPulse = null;
        private readonly IPresentationDisplayModeReader _presentationDisplayModeReader = null;

        // Constructors

        public DefaultDisplayManager(IPulseGiver<Int32> pulseGiver, IPresentationDisplayModeReader presentationDisplayModeReader)
        {
            _pollPulse = pulseGiver;
            _presentationDisplayModeReader = presentationDisplayModeReader;

            _pollPulse.Pulse += Poll;
        }

        // Methods

        public void StartPolling(Int32 pollingIntervalInMs = 50)
        {
            _pollPulse.Start(pollingIntervalInMs);
        }

        private void Poll()
        {
            OnDisplayModeChanged(_presentationDisplayModeReader.ReadPresentationDisplayMode());
        }
        

        public void StopPolling()
        {
            _pollPulse.Stop();
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
                .OrderBy(x => x)
                .Select(x => new DisplayMode(x));
        }

        public DisplayMode GetCurrentDisplayMode()
        {
            var current = _presentationDisplayModeReader.ReadPresentationDisplayMode();
            return new DisplayMode(current);
        }
    }
}
