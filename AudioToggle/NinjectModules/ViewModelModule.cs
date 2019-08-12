using AudioToggle.ViewModels;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.NinjectModules
{
    public class ViewModelModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IMainViewModel>().To<MainViewModel>().InSingletonScope();
        }
    }
}
