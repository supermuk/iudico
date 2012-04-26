using System;
using System.IO;

namespace CompileSystem.Classes.Compiling
{
    public class CompileTask
    {
        private string standardOutput;
        private string standardError;
        private string sourceFilePath;
        private Compiler compiler;

        public CompileTask(Compiler compiler, string sourceFilePath)
        {
            if (compiler == null)
                throw new Exception("Compiler can't be null");

            if (!File.Exists(sourceFilePath))
                throw new FileNotFoundException("Can't find such file", sourceFilePath);

            this.sourceFilePath = sourceFilePath;
            this.compiler = compiler;
            this.standardOutput = string.Empty;
            this.standardError = string.Empty;
        }

        public string StandardOutput
        {
            get { return this.standardOutput; }
        }

        public string StandardError
        {
            get { return this.standardError; }
        }

        public bool Execute()
        {
            return this.compiler.Compile(this.sourceFilePath, out this.standardOutput, out this.standardError);
        }
    }
}