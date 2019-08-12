using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public class Configuration
    {
        // Fields

        public List<ConfigurationNode> Nodes
        {
            get;
            set;
        }

        public ConfigurationNode this[PresentationDisplayMode presentationDisplayMode]
        {
            get
            {
                return Nodes.FirstOrDefault(x => x.PresentationDisplayMode == presentationDisplayMode);
            }
        }

        // Constructors

        public Configuration()
        {
            Nodes = new List<ConfigurationNode>();
        }
    }
}
