using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public interface IConfigurationWriter
    {
        void Write(Mapping configuration);
    }
}
