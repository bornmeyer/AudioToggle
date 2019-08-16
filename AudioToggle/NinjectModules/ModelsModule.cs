using AudioToggle.Models;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.NinjectModules
{
    public class ModelsModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IDisplayManager>().To<DefaultDisplayManager>().InSingletonScope();
            Kernel.Bind<IAudioDeviceManager>().To<DefaultAudioDeviceManager>().InSingletonScope();
            Kernel.Bind<IAudioSwitch>().To<DefaultAudioSwitch>().InSingletonScope();
            Kernel.Bind<IConfigurationWriter>().To<DefaultConfigurationWriter>();
            Kernel.Bind<IConfigurationReader>().To<DefaultConfigurationReader>();
            Kernel.Bind<IConfigFilePathLocator>().To<DefaultConfigFilePathLocator>();
            Kernel.Bind<IPulseGiver<Int32>>().To<TimerPulseGiver>();
            Kernel.Bind<IPresentationDisplayModeReader>().To<PresentationDisplayModeReader>();
        }
    }
}
