using AudioToggle.NinjectModules;
using AudioToggle.ViewModels;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AudioToggle
{
    public class ViewModelLocator
    {
        // Fields

        private DependencyObject _dummy = null;
        private IKernel _kernel = null;

        public IMainViewModel MainViewModel
        {
            get
            {
                return _kernel.Get<IMainViewModel>();
            }
        }

        // Constructors

        public ViewModelLocator()
        {
            _dummy = new DependencyObject();
            _kernel = new StandardKernel(new ViewModelModule(), new ModelsModule());
        }

        // Methods       
        
        private bool IsInDesignMode()
        {
            return DesignerProperties.GetIsInDesignMode(_dummy);
        }
    }
}
