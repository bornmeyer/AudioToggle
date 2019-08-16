using AudioToggle.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public class PresentationDisplayModeReader : IPresentationDisplayModeReader
    {
        public PresentationDisplayMode ReadPresentationDisplayMode()
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
    }
}
