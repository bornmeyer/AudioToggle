using AudioToggle.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AudioToggle.ViewModels
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {

        // Fields

        private IDisplayManager _displayManager = null;
        private DisplayMode _currentDisplayMode = null;
        private readonly IAudioSwitch _audioSwitch = null;
        private ObservableCollection<SelectorPanelViewModel> _selectorPanelViewModels = null;
        private Boolean _isStateDirty = false;


        // Properties      

        public Boolean IsStateDirty
        {
            get
            {
                return _isStateDirty;
            }
            set
            {
                _isStateDirty = value;
                OnPropertyChanged();
            }
        }

        public DisplayMode CurrentDisplayMode
        {
            get
            {
                return _currentDisplayMode;
            }
            set
            {
                _currentDisplayMode = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SelectorPanelViewModel> SelectorPanelViewModels
        {
            get
            {
                return _selectorPanelViewModels;
            }
        }

        public ICommand UndoCommand
        {
            get;
            private set;
        }

        public ICommand ApplyChangesCommand
        {
            get;
            private set;
        }

        // Constructors

        public MainViewModel(IDisplayManager displayManager,
            IAudioDeviceManager audioDeviceManager,
            IAudioSwitch audioSwitch)
        {
            _audioSwitch = audioSwitch;
            _selectorPanelViewModels = new ObservableCollection<SelectorPanelViewModel>();

            _displayManager = displayManager;
            var audioDevices = audioDeviceManager.GetAllDevices();
            UndoCommand = new RelayCommand<Object>(UndoChanges);
            ApplyChangesCommand = new RelayCommand<Object>(ApplyChanges);


            var mapping = _audioSwitch.GetConfigMapping();
            foreach (var current in _displayManager.GetAllDisplayModes())
            {
                var currentAudioDeviceId = mapping[current.PresentationDisplayMode];
                var selectorViewModel = new SelectorPanelViewModel(current, audioDevices.ToArray(), audioDevices.First(x => currentAudioDeviceId.AudioDeviceId == x.DeviceId));
                selectorViewModel.IsDirtyChanged += CheckIfAnySelectorIsDirty;
                _selectorPanelViewModels.Add(selectorViewModel);
            }

        }

        // Methods
      
        private void UndoChanges(Object arg)
        {
            foreach (var current in SelectorPanelViewModels)
            {
                current.UndoChange();
            }
        }

        private void ApplyChanges(Object arg)
        {
            foreach (var current in SelectorPanelViewModels.Where(x => x.IsDirty))
            {
                _audioSwitch.UpdateMapping(current.DisplayMode, current.SelectedAudioDevice);
                current.IsDirty = false;
            }
            CheckIfAnySelectorIsDirty();
        }

        private void CheckIfAnySelectorIsDirty()
        {
            IsStateDirty = _selectorPanelViewModels.Any(x => x.IsDirty);
        }

    }
}
