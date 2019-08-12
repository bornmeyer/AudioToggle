using System;
using System.Runtime.InteropServices;

namespace AudioToggle.Interop.Com.User
{
    public static class User32
    {
        private static class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowThreadProcessId([In] IntPtr hWnd, [Out] out uint ProcessId);

            [DllImport("user32.dll")]
            public static extern IntPtr GetForegroundWindow();
        }

        public static uint ForegroundProcessId
        {
            get
            {
                uint processId;
                var activeWindowHandle = NativeMethods.GetForegroundWindow();
                NativeMethods.GetWindowThreadProcessId(activeWindowHandle, out processId);
                return processId;
            }
        }
    }
}