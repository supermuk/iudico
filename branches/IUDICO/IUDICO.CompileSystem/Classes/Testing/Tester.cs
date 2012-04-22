﻿using System;
using System.Diagnostics;
using System.IO;

namespace CompileSystem.Classes.Testing
{
    public static class Tester
    {
        public static Status Test(string executeFilePath, string input, string output, int timelimit, int memorylimit)
        {
            Status status = null;

            //validate input values
            if (!File.Exists(executeFilePath))
                throw new FileNotFoundException("Can't find such file", executeFilePath);

            if (timelimit <= 0)
                throw new Exception("Timelimit can't be less or equal to 0");

            if (memorylimit <= 0)
                throw new Exception("Memorylimit can't be less or equal to 0");

            //--------------------

            //Set error mode, for hiding error message boxes.
            //SetErrorMode(0x0001 | 0x0002 | 0x0004 | 0x8000);
            //TODO: test if needs

            //create new process
            using (var exeProcess = new Process())
            {
                exeProcess.StartInfo.RedirectStandardError = true;
                exeProcess.StartInfo.RedirectStandardInput = true;
                exeProcess.StartInfo.RedirectStandardOutput = true;
                exeProcess.StartInfo.CreateNoWindow = true;
                exeProcess.StartInfo.UseShellExecute = false;
                exeProcess.StartInfo.FileName = executeFilePath;
                long memoryValue = -1;

                exeProcess.Start();
                int processId = exeProcess.Id;

                exeProcess.StandardInput.Write(input);
                //memory usage
                memoryValue = Process.GetProcessById(processId).PeakWorkingSet64;
                
                exeProcess.StandardInput.Close();
                exeProcess.WaitForExit(timelimit);

                if (!exeProcess.HasExited)
                {
                    exeProcess.Kill();
                    status = new Status("TimeLimit");
                }
                
                var outputResult = exeProcess.StandardOutput.ReadToEnd().Trim();
                var outputError = exeProcess.StandardError.ReadToEnd();
                memoryValue = memoryValue/1024;

                // set result
                if (status == null)
                {
                    if (exeProcess.ExitCode != 0)
                        status = new Status("Crashed");
                    else
                    {
                        if (memoryValue > memorylimit)
                            status = new Status("Memorylimit");

                        else
                        status = outputResult == output ? new Status("Accepted") : new Status("WrongAnswer");
                    }
                }
            }

            return status;
        }
    }
}