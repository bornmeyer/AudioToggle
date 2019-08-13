using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public class Mapping
    {
        // Fields

        public List<MappingNode> Nodes
        {
            get;
            set;
        }

        public MappingNode this[PresentationDisplayMode presentationDisplayMode]
        {
            get
            {
                return Nodes.FirstOrDefault(x => x.PresentationDisplayMode == presentationDisplayMode);
            }
        }

        // Constructors

        public Mapping()
        {
            Nodes = new List<MappingNode>();
        }
    }
}
