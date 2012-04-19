using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;

namespace CompileSystem.Classes.Compiling
{
    public class Compiler
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Extension { get; set; }
        public string Arguments { get; set; }
        public string CompiledExtension { get; set; }
        public bool IsNeedShortPath { get; set; }

        public const string CompilerDirectory = "$CompilerDirectory$";
        public const string SourceFilePath = "$SourceFilePath$";

        public Compiler()
        {
            Name = "";
            Location = "";
            Extension = "";
            Arguments = "";
            CompiledExtension = "";
            IsNeedShortPath = false;
        }

        public bool Compile(string sourceFilePath, out string standardOutput, out string standardError)
        {
            if (!File.Exists(sourceFilePath))
                throw new FileNotFoundException("Can't find such file", sourceFilePath);

            sourceFilePath = Path.GetFullPath(sourceFilePath);

            try
            {
                using (var process = new Process())
                {
                    //standard path
                    var shortLocation = Path.GetDirectoryName(Location);
                    var shortSourceFilePath = sourceFilePath;

                    if (IsNeedShortPath)
                    {
                        shortLocation = ToShortPathName(Path.GetDirectoryName(Location));
                        shortSourceFilePath = ToShortPathName(sourceFilePath);
                    }

                    //this.Arguments = "-U\"$CompilerDirectory$\" $SourceFilePath$";
                    //set compilation arguments
                    process.StartInfo.FileName = Location;
                    process.StartInfo.Arguments =
                        Arguments.Replace(CompilerDirectory, shortLocation).Replace(SourceFilePath, shortSourceFilePath);
                    process.StartInfo.WorkingDirectory = Path.GetDirectoryName(sourceFilePath);

                    //set up process info nad start
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();

                    //get output and error info
                    standardOutput = process.StandardOutput.ReadToEnd();
                    standardError = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    var compiled = File.Exists(Path.ChangeExtension(sourceFilePath, CompiledExtension));

                    return compiled;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error occurred during compilation", exception);
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern uint GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)] string lpszLongPath,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpszShortPath,
            uint cchBuffer);

        private static string ToShortPathName(string longName)
        {
            var shortNameBuffer = new StringBuilder(256);
            var bufferSize = (uint)shortNameBuffer.Capacity;
            var result = GetShortPathName(longName, shortNameBuffer, bufferSize);

            return shortNameBuffer.ToString();
        }
    }
}