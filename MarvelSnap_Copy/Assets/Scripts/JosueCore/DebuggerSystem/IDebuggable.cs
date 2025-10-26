namespace JosueCore.DebuggerSystem
{
    public interface IDebuggable
    {
        public void SetDebugger(Debugger debugger);

        public string GetDebuggerName();
    }
}