using System;
using System.Runtime.InteropServices;

namespace Win32
{
    public static class Win32Native
    {
        [DllImport("User32.dll", EntryPoint = "PeekMessage")]
        public static extern bool PeekMessage(
            out NativeMessage lpMsg,
            IntPtr hWnd,
            uint wMsgFilterMin,
            uint wMsgFilterMax,
            uint wRemoveMsg
        );

        [DllImport("User32.dll", EntryPoint = "GetMessage")]
        public static extern int GetMessage(
            out NativeMessage lpMsg,
            IntPtr hWnd,
            int wMsgFilterMin,
            int wMsgFilterMax
        );

        [DllImport("User32.dll", EntryPoint = "TranslateMessage")]
        public static extern int TranslateMessage(ref NativeMessage lpMsg);

        [DllImport("User32.dll", EntryPoint = "DispatchMessage")]
        public static extern int DispatchMessage(ref NativeMessage lpMsg);
    }
}
