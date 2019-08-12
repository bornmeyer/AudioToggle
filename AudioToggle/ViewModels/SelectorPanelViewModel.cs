using AudioToggle.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.ViewModels
{
    public class SelectorPanelViewModel : ViewModelBase
    {
        // Events

        public event Action IsDirtyChanged;

        // Fields

        private readonly DisplayMode _displayMode = null;
        private readonly ObservableCollection<AudioDevice> _audioDevices = null;
        private AudioDevice _selectedAudioDevice = null;
        private Boolean _isDirty = false;
        private Boolean _canBeDirty = false;
        private AudioDevice _startingValue = null;


        // Properties

        public Boolean IsDirty
        {
            get
            {
                return _isDirty;
            }
            set
            {
                _isDirty = value && _canBeDirty;
                OnPropertyChanged();
            }
        }

        public DisplayMode DisplayMode
        {
            get
            {
                return _displayMode;
            }
        }

        public ObservableCollection<AudioDevice> AudioDevices
        {
            get
            {
                return _audioDevices;
            }
        }

        public AudioDevice SelectedAudioDevice
        {
            get
            {
                return _selectedAudioDevice;
            }
            set
            {

                _selectedAudioDevice = value;
                OnPropertyChanged();
                IsDirty = value != _startingValue;
                OnIsDirtyChanged();
                _startingValue = value;
            }
        }


        // Constructors

        public SelectorPanelViewModel(DisplayMode displayMode, IEnumerable<AudioDevice> audioDevices, AudioDevice selectedAudioDevice = null)
        {
            _canBeDirty = false;
            _displayMode = displayMode;
            _audioDevices = new ObservableCollection<AudioDevice>(audioDevices);
            SelectedAudioDevice = selectedAudioDevice == null ? audioDevices.First() : audioDevices.First(x => x.DeviceId == selectedAudioDevice.DeviceId);
            _startingValue = SelectedAudioDevice;
            IsDirty = false;
            _canBeDirty = true;
        }

        // Methods

        private void OnIsDirtyChanged()
        {
            IsDirtyChanged?.Invoke();
        }

        public void UndoChange()
        {
            SelectedAudioDevice = _startingValue;
        }
    }
}
