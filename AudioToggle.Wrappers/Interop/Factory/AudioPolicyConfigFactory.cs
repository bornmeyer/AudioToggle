using AudioToggle.Interop.Com.Base;
using AudioToggle.Interop.Interface.Policy.Extended;
using System;

namespace AudioToggle.Interop.Factory
{
    internal sealed class AudioPolicyConfigFactory
    {
        public static IAudioPolicyConfigFactory Create()
        {
            object factory;
            var iid = typeof(IAudioPolicyConfigFactory).GUID;
            ComBase.RoGetActivationFactory("Windows.Media.Internal.AudioPolicyConfig", ref iid, out factory);
            return (IAudioPolicyConfigFactory)factory;
        }
    }
}