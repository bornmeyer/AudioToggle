using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public class DisplayMode
    {
        // Properties

        public PresentationDisplayMode PresentationDisplayMode
        {
            get;
            private set;
        }

        public String DisplayModeName
        {
            get;
            private set;
        }

        // Constructors

        public DisplayMode(PresentationDisplayMode presentationDisplayMode)
        {
            PresentationDisplayMode = presentationDisplayMode;
            DisplayModeName = presentationDisplayMode.ToString();
        }

        // Methods

        public override bool Equals(object obj)
        {
            Boolean result = false;
            if (obj is DisplayMode other)
            {
                result = other.PresentationDisplayMode == PresentationDisplayMode;
            }
            return result;
        }

        public override string ToString()
        {
            return $"#{DisplayModeName}(#{PresentationDisplayMode})";
        }
    }
}
