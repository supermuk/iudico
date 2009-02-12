using System;
using System.Reflection;

namespace TestingSystem
{
    class ExeWrapperDotNET
    {
        static void Main(string[] args)
        {
            try
            {
                //get path to .exe file
                string input = args[0];
                //load it
                Assembly assembly = Assembly.LoadFile(input);
                //get entry point
                MethodInfo entryPoint = assembly.EntryPoint;
                ParameterInfo[] parameters = entryPoint.GetParameters();
                //invoke exe file
                entryPoint.Invoke(null, parameters);

                Environment.ExitCode = 0;
            }
            catch
            {
                Environment.ExitCode = -1;
            }
        }
    }
}
