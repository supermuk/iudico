using System;
using System.Diagnostics;
using System.Threading;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Common
{
    /// <summary>
    /// Class to log data
    /// </summary>
    public static class Logger
    {
        static Logger()
        {
            Debug.IndentLevel = 0;
            __Indent = INDENT_VALUE;
        }

        private class LoggerScope : IDisposable
        {
            public readonly string Name;

            public LoggerScope([NotNull]string name)
            {
                Name = name;
                Enter();
            }

            public void Enter()
            {
                WriteLine("--> " + Name);
                Indent();
            }

            public void Leave()
            {
                UnIndent();
                WriteLine("<-- " + Name);
            }

            public void Dispose()
            {
                Leave();
            }
        }

        public static void WriteLine([NotNull] string line)
        {
            Debug.WriteLine(string.Format("{0}  {1:3}", DateTime.Now.ToLongTimeString(), Thread.CurrentThread.ManagedThreadId) + new string(' ', __Indent) + line);
        }

        [StringFormatMethod("lineFmt")]
        public static void WriteLine([NotNull] string lineFmt, [NotNull] params object[] args)
        {
            WriteLine(string.Format(lineFmt, args));
        }

        public static void Indent()
        {
            __Indent += INDENT_VALUE;
        }

        public static void UnIndent()
        {
            __Indent -= INDENT_VALUE;
        }

        public static IDisposable Scope([NotNull] string name)
        {
            return new LoggerScope(name);
        }

        private const int INDENT_VALUE = 2;

        // TODO: Correct this code for multy-threading runnings
        private static int __Indent;
    }
}
