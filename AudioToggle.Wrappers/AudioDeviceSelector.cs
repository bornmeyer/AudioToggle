using System.Runtime.InteropServices;
using AudioToggle.Interop;
using AudioToggle.Interop.Client;
using AudioToggle.Interop.Com.Threading;
using AudioToggle.Interop.Com.User;
using AudioToggle.Interop.Enum;

namespace AudioToggle
{
    public class AudioDeviceSelector
    {

        // Fields

        private static AudioDeviceSelector _instance;
        private readonly PolicyClient _policyClient = new PolicyClient();
        private readonly EnumeratorClient _enumerator = new EnumeratorClient();

        private ExtendedPolicyClient _extendedPolicyClient;

        // Properties

        private ExtendedPolicyClient ExtendPolicyClient
        {
            get
            {
                if (_extendedPolicyClient != null)
                {
                    return _extendedPolicyClient;
                }

                return _extendedPolicyClient = ComThread.Invoke(() => new ExtendedPolicyClient());
            }
        }

        private AudioDeviceSelector()
        {
        }

        public static AudioDeviceSelector Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                return _instance = ComThread.Invoke(() => new AudioDeviceSelector());
            }
        }

   
        public void SwitchTo(string deviceId, ERole role)
        {
            if (role != ERole.ERole_enum_count)
            {
                ComThread.Invoke((() => _policyClient.SetDefaultEndpoint(deviceId, role)));

                return;
            }

            SwitchTo(deviceId, ERole.eConsole);
            SwitchTo(deviceId, ERole.eMultimedia);
            SwitchTo(deviceId, ERole.eCommunications);
        }
       

        public void SwitchProcessTo(string deviceId, ERole role, EDataFlow flow, uint processId)
        {

            var roles = new ERole[]
            {
                ERole.eConsole,
                ERole.eCommunications,
                ERole.eMultimedia
            };

            if (role != ERole.ERole_enum_count)
            {
                roles = new ERole[]
                {
                    role
                };
            }


            ComThread.Invoke((() => ExtendPolicyClient.SetDefaultEndPoint(deviceId, flow, roles, processId)));
        }
       

        public void SwitchProcessTo(string deviceId, ERole role, EDataFlow flow)
        {
            var processId = ComThread.Invoke(() => User32.ForegroundProcessId);
            SwitchProcessTo(deviceId, role, flow, processId);
        }

       
        public bool IsDefault(string deviceId, EDataFlow flow, ERole role)
        {
            return ComThread.Invoke(() => _enumerator.IsDefault(deviceId, flow, role));
        }

       
        public string GetUsedDevice(EDataFlow flow, ERole role, uint processId)
        {
            return ComThread.Invoke(() => ExtendPolicyClient.GetDefaultEndPoint(flow, role, processId));
        }

      
        public void ResetProcessDeviceConfiguration()
        {
            ComThread.Invoke(() =>  ExtendPolicyClient.ResetAllSetEndpoint());
        }
    }
}