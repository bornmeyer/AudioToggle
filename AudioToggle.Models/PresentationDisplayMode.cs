using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public enum PresentationDisplayMode : uint
    {
        Zero = 0x0,

        Internal = 0x00000001,
        Clone = 0x00000002,
        Extend = 0x00000004,
        External = 0x00000008,
        ForceUint32 = 0xFFFFFFFF
    }
}
