using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Serialization;

namespace TestingSystem
{
    class ExeWrapper
    {
        /// <summary>
        /// Represents the parameter of SetErrorMode function. The system does not display the critical-error-handler
        /// message box. 
        /// </summary>
        private const uint SEM_FAILCRITICALERRORS = 0x0001;

        /// <summary>
        /// Represents the parameter of SetErrorMode function. The system does not display the 
        /// general-protection-fault message box.
        /// </summary>
        private const uint SEM_NOGPFAULTERRORBOX = 0x0002;

        /// <summary>
        /// Represents the parameter of SetErrorMode function. The system does not display a message box when it fails
        /// to find a file. 
        /// </summary>
        private const uint SEM_NOOPENFILEERRORBOX = 0x8000;

        /// <summary>
        /// Represents the parameter of SetErrorMode function. The system automatically fixes memory alignment faults
        /// and makes them invisible to the application.  
        /// </summary>
        private const uint SEM_NOALIGNMENTFAULTEXCEPT = 0x0004;

        /// <summary>
        /// Controls whether the system will handle the specified types of serious errors or whether the process will
        /// handle them.
        /// </summary>
        /// 
        /// <param name="uMode">
        /// The process error mode. 
        /// </param>
        /// 
        /// <returns>
        /// The return value is the previous state of the error-mode bit flags.
        /// </returns>
        [DllImport("kernel32.dll")]
        public static extern uint SetErrorMode(uint uMode);

        /// <summary>
        /// The path for wrapping exe for .NET applications.
        /// </summary>
        /// 
        /// <remarks>
        /// .NET wrapping application was designed because of not working SetErrorMode function with .NET programs.
        /// </remarks>
        private static string NETWrapperPath;
        /// <summary>
        /// Runs provided .exe file and returns the resulting statistic( time usage, memory usage, output). 
        /// </summary>
        static void Main()
        {
            NETWrapperPath = AppDomain.CurrentDomain.BaseDirectory + @"ExeWrapperDotNET.exe";
            //Set error mode, for hiding error message boxes.
            SetErrorMode(0x0001 | 0x0002 | 0x0004 | 0x8000);
            Result result = new Result();
            result.ProgramStatus = Status.Running;

            //Read input data
            string exePath = Console.ReadLine();
            string serializedProgram = Console.In.ReadToEnd();

            //deserialize problem
            XmlSerializer deserializer = new XmlSerializer(typeof(Program));
            MemoryStream inputStream = new MemoryStream();

            byte[] buffer = new byte[serializedProgram.Length];
            for (int i = 0; i < serializedProgram.Length; i++)
            {
                buffer[i] = (byte)serializedProgram[i];
            }

            inputStream.Write(buffer, 0, buffer.Length);
            inputStream.Position = 0;
            Program program = deserializer.Deserialize(inputStream) as Program;

            //create new process
            Process exeProcess = new Process();

            exeProcess.StartInfo.RedirectStandardError = true;
            exeProcess.StartInfo.RedirectStandardInput = true;
            exeProcess.StartInfo.RedirectStandardOutput = true;

            exeProcess.StartInfo.CreateNoWindow = true;
            exeProcess.StartInfo.UseShellExecute = false;

            //check if provided exe. is .NET based
            exeProcess.StartInfo.FileName = NETWrapperPath;
            exeProcess.StartInfo.Arguments = "\"" + exePath + "\"";
            try
            {
                //if true we run .NET wrapper
                Assembly.LoadFile(Path.GetFullPath(exePath));
                if (!File.Exists(exePath))
                {
                    throw new FileNotFoundException("Can't find file", exePath);
                }
            }
            catch
            {
                //if not we simply run it.
                exeProcess.StartInfo.Arguments = "";
                exeProcess.StartInfo.FileName = exePath;
            }

            //start process
            MemoryCounter memoryCounter = new MemoryCounter(exeProcess, 20);
            exeProcess.Start();
            Thread.Sleep(100);
            //write input data
            exeProcess.StandardInput.Write(program.InputTest);
            exeProcess.StandardInput.Close();

            exeProcess.WaitForExit(program.TimeLimit);
            if (!exeProcess.HasExited)
            {
                exeProcess.Kill();
                result.ProgramStatus = Status.TimeLimit;
            }
            memoryCounter.Stop();

            //get program statistic
            result.Error = exeProcess.StandardError.ReadToEnd();
            result.Output = exeProcess.StandardOutput.ReadToEnd();
            result.TimeUsed = (int)exeProcess.TotalProcessorTime.TotalMilliseconds;
            result.MemoryUsed = (int)memoryCounter.Memory / 1024;

            //some programs output 0 char that is not acceptible, we replace it with 13(return)  char
            for (int i = 0; i < 32; i++)
            {
                if (i != 9 && i != 10 && i != 13)
                {
                    result.Output = result.Output.Replace((char)i, (char)13);
                }
            }

            //serialize and deserialize output
            //this is done becase string is changed during serialization
            //as program.OutputTest was serilized/deserialized
            //we need make same chnges with result.Output
            XmlSerializer stringSerializer = new XmlSerializer(typeof(string));
            MemoryStream stringStream = new MemoryStream();
            stringSerializer.Serialize(stringStream, result.Output);
            stringStream.Position = 0;
            result.Output = stringSerializer.Deserialize(stringStream) as string;
            result.Output = result.Output.Trim();

            //set program status
            if (result.ProgramStatus != Status.TimeLimit)
            {
                if (exeProcess.ExitCode != 0)
                {
                    result.ProgramStatus = Status.Crashed;
                }
                else
                {
                    if (result.MemoryUsed > program.MemoryLimit)
                    {
                        result.ProgramStatus = Status.MemoryLimit;
                    }
                    else
                    {
                        if (result.Output == program.OutputTest)
                        {
                            result.ProgramStatus = Status.Accepted;
                        }
                        else
                        {
                            result.ProgramStatus = Status.WrongAnswer;
                        }
                    }

                }
            }
            //serialize the result of progarm execution
            XmlSerializer serializer = new XmlSerializer(typeof(Result));
            MemoryStream outputStream = new MemoryStream();
            serializer.Serialize(outputStream, result);

            //write the result to the stream
            Stream standardOutput = Console.OpenStandardOutput();
            outputStream.WriteTo(standardOutput);
            standardOutput.Close();
        }
    }
}
