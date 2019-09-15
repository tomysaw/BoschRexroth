using System;

namespace BoschRexroth.Root
{
    public class Logger
    {
        private Action<string> _logAction;
        public Logger(Action<string> logAction)
        {
            _logAction = logAction;
        }

        public void Log(string s, params object[] args)
        {
            _logAction(string.Format(s, args));
        }
    }
}
