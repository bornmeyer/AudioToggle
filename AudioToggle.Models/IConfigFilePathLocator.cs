using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public interface IConfigFilePathLocator
    {
        FileInfo Locate();
    }
}
