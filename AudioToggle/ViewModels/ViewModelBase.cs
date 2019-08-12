using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        // Event

        public event PropertyChangedEventHandler PropertyChanged;

        // Properties

        protected void OnPropertyChanged([CallerMemberName]String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
