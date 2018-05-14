using System;
using System.Runtime.InteropServices;

namespace Noesis
{
    public enum LogLevel
    {
        Trace = 0,
        Debug = 1,
        Info = 2,
        Warning = 3,
        Error = 4
    }

    public delegate void LogCallback(LogLevel level, string channel, string message);

    public class Log
    {
        private static LogCallback _managedCallback;
        public static LogCallback LogCallback
        {
            get { return _managedCallback; }
            set
            {
                _managedCallback = value;
                Noesis_RegisterLogCallback(_managedCallback != null ? _nativeCallback : null);
            }
        }

        #region Internal log helpers
        internal static void Error(string text)
        {
            DoLog((int)LogLevel.Error, "", text);
        }
        #endregion

        #region Native to managed callback
        private delegate void NativeLogCallback(int level, [MarshalAs(UnmanagedType.LPWStr)]string channel, [MarshalAs(UnmanagedType.LPWStr)]string message);
        private static NativeLogCallback _nativeCallback = DoLog;

        [MonoPInvokeCallback(typeof(NativeLogCallback))]
        private static void DoLog(int level, string channel, string message)
        {
            LogCallback callback = LogCallback;
            if (callback != null)
            {
                callback((LogLevel)level, channel, message);
            }
        }

        [DllImport(Library.Name)]
        private static extern void Noesis_RegisterLogCallback(NativeLogCallback callback);
        #endregion
    }
}
