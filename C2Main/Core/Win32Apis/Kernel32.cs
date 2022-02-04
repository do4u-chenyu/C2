using System;
using System.Runtime.InteropServices;

namespace C2.Controls.OS
{
    public static class Kernel32
    {
        const string DllName = "Kernel32";

        [DllImport(DllName)]
        public static extern Int32 GetLastError();

        [DllImport(DllName)]
        public static extern bool Beep(uint dwFreq, uint dwDuration);

        [DllImport(DllName)]
        public static extern UIntPtr GlobalSize(IntPtr hMem);

        [DllImport(DllName)]
        public static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport(DllName)]
        public static extern bool AttachConsole(uint dwProcessId);

        [DllImport(DllName)]
        public static extern bool FreeConsole();

        public enum CtrlTypes : uint
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        public delegate bool ConsoleCtrlDelegate(CtrlTypes CtrlType);

        [DllImport(DllName)]
        public static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate HandlerRoutine, bool Add);

        [DllImport(DllName)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GenerateConsoleCtrlEvent(CtrlTypes dwCtrlEvent, uint dwProcessGroupId);
    }
}
