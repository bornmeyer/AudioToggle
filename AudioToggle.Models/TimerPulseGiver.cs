using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AudioToggle.Models
{
    public class TimerPulseGiver : IPulseGiver<Int32>
    {
        // Events

        public event Action Pulse;

        // Fields

        private readonly Timer _timer = null;

        // Constructors

        public TimerPulseGiver()
        {
            _timer = new Timer();
            _timer.Elapsed += (sender, e) => OnPulse();
        }

        // Methods

        public void Start(Int32 pulseInterval)
        {
            _timer.Interval = pulseInterval;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void OnPulse()
        {
            Pulse?.Invoke();
        }
    }
}
