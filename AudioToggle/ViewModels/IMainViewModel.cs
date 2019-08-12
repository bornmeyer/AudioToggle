using AudioToggle.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace AudioToggle.ViewModels
{
    public interface IMainViewModel
    {
        Boolean IsStateDirty
        {
            get;
        }

        DisplayMode CurrentDisplayMode
        {
            get;
        }

        ObservableCollection<SelectorPanelViewModel> SelectorPanelViewModels
        {
            get;
        }

        ICommand UndoCommand
        {
            get;
        }

        ICommand ApplyChangesCommand
        {
            get;
        }
    }
}