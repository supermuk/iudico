using System;
using System.IO;

namespace CompileSystem.Classes
{
    public static class Helper
    {
        public static string CreateFileForCompilation(string source, string extension)
        {
            // validate input parameters
            if (string.IsNullOrEmpty(extension))
                throw new Exception("Extension is not valid");

            // set default values 
            // TODO: maybe we need to move it somewhere
            const string ProgramName = "Program";
            string testingDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            // write source into a file
            string programDirectory = Path.Combine(testingDirectory, ProgramName);
            string sourceFilePath = Path.Combine(programDirectory, ProgramName + "." + extension);

            Directory.CreateDirectory(programDirectory);

            // TODO: maybe take another program name?

            var writer = new StreamWriter(sourceFilePath);
            writer.Write(source);
            writer.Close();

            return sourceFilePath;
        }
    }
}