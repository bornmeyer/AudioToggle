using System;

namespace AudioToggle.Interop.Com.Threading
{
    public sealed class InvalidThreadException : Exception
    {

        public InvalidThreadException()
        {

        }

        public InvalidThreadException(string message)
            : base(message)
        {
        }
    }
}