using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public interface IPulseGiver<TPulseInterval>
    {
        event Action Pulse;
        void Start(TPulseInterval pulseInterval);
        void Stop();
    }
}
