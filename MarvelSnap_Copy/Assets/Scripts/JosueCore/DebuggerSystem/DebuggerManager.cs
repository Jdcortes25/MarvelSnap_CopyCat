using System.Collections.Generic;
using UnityEngine;

namespace JosueCore.DebuggerSystem
{
    public class DebuggerManager : MonoBehaviour
    {
        [SerializeField] private bool debuggerEnabled;
        [SerializeField] private string managerName;
        [SerializeField, SerializableInterface(typeof(IDebuggable))] private List<MonoBehaviour> debuggableClasses = new();

        private void Awake()
        {
            InitializeDebuggers();
        }

        private void InitializeDebuggers()
        {
            foreach(IDebuggable debuggableClass in debuggableClasses)
            {
                Debugger debugger = new Debugger(debuggableClass.GetDebuggerName(), managerName);
                debugger.Enabled = debuggerEnabled;
                debuggableClass.SetDebugger(debugger);
            }
        }
    }
}