using System;
using UnityEngine;

namespace JosueCore.DebuggerSystem
{
    public class Debugger
    {
        public bool Enabled = true;

        private string debuggerName;
        private string debuggerManagerName;

        public Debugger(string debuggerName, string debuggerManagerName)
        {
            this.debuggerName = debuggerName;
            this.debuggerManagerName = debuggerManagerName;
        }

        private void InternalLog(string message, Action<string> logAction)
        {
            if (!Enabled)
            {
                return;
            }

            logAction.Invoke($"{debuggerManagerName}.{debuggerName}: {message}");
        }

        public void Log(string message)
        {
            InternalLog(message, Debug.Log);
        }

        public void LogWarning(string message)
        {
            InternalLog(message, Debug.LogWarning);
        }

        public void LogError(string message)
        {
            InternalLog(message, Debug.LogError);
        }
    }
}