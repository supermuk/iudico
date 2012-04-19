using System;
using System.IO;

namespace CompileSystem.Classes.Compiling
{
    public class CompileTask
    {
        private string _standardOutput;
        private string _standardError;
        private string _sourceFilePath;
        private Compiler _compiler;

        public CompileTask(Compiler compiler, string sourceFilePath)
        {
            if (compiler == null)
                throw new Exception("Compiler can't be null");

            if (!File.Exists(sourceFilePath))
                throw new FileNotFoundException("Can't find such file", sourceFilePath);

            _sourceFilePath = sourceFilePath;
            _compiler = compiler;
            _standardOutput = "";
            _standardError = "";
        }

        public string StandardOutput
        {
            get { return _standardOutput; }
        }

        public string StandardError
        {
            get { return _standardError; }
        }

        public bool Execute()
        {
            return _compiler.Compile(_sourceFilePath, out _standardOutput, out _standardError);
        }
    }
}