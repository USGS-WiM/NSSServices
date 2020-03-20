using System;
using System.Diagnostics;
using Newtonsoft.Json.Serialization;

namespace NSSServices
{
    internal class memoryTraceWriter : ITraceWriter
    {
        public TraceLevel LevelFilter
        {
            // trace all messages.
            get { return TraceLevel.Verbose; }
        }

        public void Trace(TraceLevel level, string message, Exception ex)
        {
            Console.WriteLine(level.ToString());
            Console.WriteLine(message);
            if (ex != null)
                Console.WriteLine(ex.Message);
            if (ex?.InnerException != null) Console.WriteLine(ex.InnerException.Message);
        }
    }
}
