using System.Diagnostics;
using System.IO;
using System.Security;
using System.Xml.Serialization;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;

namespace TestingSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class Runner
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
        private static string NETWrapperPath = "ExeWrapperDotNET.exe";

        /// <summary>
        /// The path for wrapping exe for java applications.
        /// </summary>
        /// 
        /// <remarks>
        /// java wrapping application was designed because of not working SetErrorMode function with .NET programs.
        /// </remarks>
        private static string JavaWrapperPath = @"..\..\test_files\Compilers\Java6\bin\java.exe";

        /// <summary>
        /// Executes provided exe file and returns the result of program using.
        /// </summary>
        /// 
        /// <param name="exePath">
        /// Path of exe file to run.
        /// </param>
        /// 
        /// <param name="program">
        /// Execunitg constraints.(like memory limit, time limit)
        /// </param>
        /// 
        /// <returns>
        /// Detailed result of program executing.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        /// If any argument is null.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// If provided path is invalid.
        /// </exception>
        /// <exception cref="">
        /// 
        /// </exception>
        public static Result ExecuteWin32(string exePath, Program program, string arguments)
        {
            //validate arguments
            ProjectHelper.ValidateNotNull(program, "program");
            ProjectHelper.ValidateFileExists(exePath, "exePath");

            //Set error mode, for hiding error message boxes.
            SetErrorMode(0x0001 | 0x0002 | 0x0004 | 0x8000);
            Result result = new Result();
            result.ProgramStatus = Status.Running;


            //create new process
            using (Process exeProcess = new Process())
            {
                exeProcess.StartInfo.RedirectStandardError = true;
                exeProcess.StartInfo.RedirectStandardInput = true;
                exeProcess.StartInfo.RedirectStandardOutput = true;
                exeProcess.StartInfo.CreateNoWindow = true;
                exeProcess.StartInfo.UseShellExecute = false;

                exeProcess.StartInfo.FileName = exePath;
                exeProcess.StartInfo.Arguments = arguments;

                //start process
                MemoryCounter memoryCounter = new MemoryCounter(exeProcess, 20);
                exeProcess.Start();
                Thread.Sleep(200);
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
            }
            return result;
        }

        /// <summary>
        /// Executes provided exe file and returns the result of program using.
        /// </summary>
        /// 
        /// <param name="exePath">
        /// Path of exe file to run.
        /// </param>
        /// 
        /// <param name="program">
        /// Execunitg constraints.(like memory limit, time limit)
        /// </param>
        /// 
        /// <returns>
        /// Detailed result of program executing.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">
        /// If any argument is null.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// If provided path is invalid.
        /// </exception>
        /// <exception cref="">
        /// 
        /// </exception>
        public static Result Execute(string exePath, Program program)
        {
            if (program.Language == Language.DotNet2 || program.Language == Language.DotNet3)
            {
                return ExecuteWin32(NETWrapperPath, program, exePath);
            }
            if (program.Language == Language.Java6)
            {
                return ExecuteWin32(JavaWrapperPath, program,
                    "-cp " + Path.GetDirectoryName(exePath) + " " + Path.GetFileNameWithoutExtension(exePath));
            }

            return ExecuteWin32(exePath, program, "");

        }
    }


}
