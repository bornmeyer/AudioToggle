using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToggle.Models
{
    public class DefaultAudioSwitch : IAudioSwitch
    {
        private readonly IDisplayManager _displayManager = null;
        private readonly IAudioDeviceManager _audioDeviceManager = null;
        private readonly IConfigurationReader _configurationReader = null;
        private readonly IConfigurationWriter _configurationWriter = null;
        private readonly Configuration _configuration = null;
        private DisplayMode _lastDisplayNode = null;

        // Constructors

        public DefaultAudioSwitch(IDisplayManager displayManager,
            IAudioDeviceManager audioDeviceManager,
            IConfigurationReader configurationReader,
            IConfigurationWriter configurationWriter)
        {
            _displayManager = displayManager;
            _audioDeviceManager = audioDeviceManager;
            _configurationReader = configurationReader;
            _configurationWriter = configurationWriter;
            _configuration = CreateDefaultConfigurationIfNecessary(displayManager);
            _lastDisplayNode = _displayManager.GetCurrentDisplayMode();
            _displayManager.DisplayModeChanged += DisplayModeChanged;
            _displayManager.StartPolling(1000);
        }

        // Methods

        private void DisplayModeChanged(DisplayMode obj)
        {

            if (_lastDisplayNode.PresentationDisplayMode != obj.PresentationDisplayMode)
            {
                _displayManager.StopPolling();
                var configNode = _configuration.Nodes.FirstOrDefault(x => x.PresentationDisplayMode == obj.PresentationDisplayMode);
                _audioDeviceManager.SwitchToDevice(configNode?.AudioDeviceId);
                _lastDisplayNode = obj;
                _displayManager.StartPolling(500);
            }

        }

        private Configuration CreateDefaultConfigurationIfNecessary(IDisplayManager displayManager)
        {
            var configuration = _configurationReader.Read();

            if (configuration == null || configuration.Nodes == null)
            {
                configuration.Nodes.Clear();
                var allDisplayModes = displayManager.GetAllDisplayModes();
                var defaultAudioDevice = _audioDeviceManager.GetAllDevices().FirstOrDefault(x => x.CurrentlyDefault);
                foreach (var current in allDisplayModes)
                {
                    var newNode = new ConfigurationNode(defaultAudioDevice, current);
                    configuration.Nodes.Add(newNode);
                }

                _configurationWriter.Write(configuration);
            }
            return configuration;
        }

        public void Switch(DisplayMode displayMode)
        {
            throw new NotImplementedException();
        }

        public void UpdateMapping(DisplayMode displayMode, AudioDevice audioDevice)
        {
            var node = _configuration.Nodes.FirstOrDefault(x => x.PresentationDisplayMode == displayMode.PresentationDisplayMode);
            node.AudioDeviceId = audioDevice.DeviceId;
            _configurationWriter.Write(_configuration);
        }

        public Configuration GetConfigMapping()
        {
            return _configuration;
        }
    }
}
