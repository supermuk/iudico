using System;
using System.Reflection;

namespace Helper.DotNetExeWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var input = args[0];
                var assembly = Assembly.LoadFile(input);
                var entryPoint = assembly.EntryPoint;
                var parametersInfo = entryPoint.GetParameters();
                var parameters = new object[parametersInfo.Length];
                
                for (var i = 0; i < parametersInfo.Length; i++)
                {
                    parameters[i] = parametersInfo[i].ParameterType.IsPrimitive ? Activator.CreateInstance(parametersInfo[i].ParameterType) : null;
                }
                
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
