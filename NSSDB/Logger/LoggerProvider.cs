//https://docs.microsoft.com/en-us/ef/core/miscellaneous/logging

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EFLogging
{
    public class LoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            if (categoryName == typeof(Microsoft.EntityFrameworkCore.Storage.IRelationalCommandBuilderFactory).FullName)
            {
                return new SqlLogger(categoryName);
            }

            return new MyLogger();
        }

        public void Dispose()
        { }

        private class MyLogger : ILogger
        {
            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                #warning don't forget to update this
                //File.AppendAllText(@"C:\temp\log.txt", formatter(state, exception));
                //Console.WriteLine(formatter(state, exception));
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        }

        private class SqlLogger : ILogger
        {
            private string _test;

            public SqlLogger(string test)
            {
                _test = test;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                if (eventId.Id == (int)RelationalEventId.ExecutedCommand)
                {
                    var data = state as IEnumerable<KeyValuePair<string, object>>;
                    if (data != null)
                    {
                        var commandText = data.Single(p => p.Key == "CommandText").Value;
                        Console.WriteLine(commandText);
                    }
                }
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        }
    }
}
