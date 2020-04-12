using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Loki2D.Core.Utilities
{
    public static class Debug
    {
        public static EventHandler<LogEvent> Logged; 

        public static void Log(object log)
        {
            var logEvent = new LogEvent() {Log = log};
            Logged.Invoke(null, logEvent);

            Console.WriteLine(log);
        }
    }

    public class LogEvent : EventArgs
    {
        public object Log;
    }
}
