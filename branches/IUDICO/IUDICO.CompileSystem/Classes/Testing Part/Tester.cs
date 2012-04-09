using System.Diagnostics;
using System.IO;

namespace CompileSystem.Testing_Part
{
    public static class Tester
    {
        public static Status Test(string executeFilePath, string input, string output, int timelimit, int memorylimit)
        {
            Status status;

            //validate path
            if (!File.Exists(executeFilePath))
                throw new FileNotFoundException("Can't find such file", executeFilePath);

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
                //exeProcess.StartInfo.Arguments = "";
                exeProcess.Start();

                exeProcess.StandardInput.Write(input);
                exeProcess.StandardInput.Close();

                var outputResult = exeProcess.StandardOutput.ReadToEnd().Trim();
                var outputError = exeProcess.StandardError.ReadToEnd();

                if (outputResult == output)
                    status = new Status("Accepted");

                else
                    status = new Status("Crash");
            }

            return status;
        }
    }
}