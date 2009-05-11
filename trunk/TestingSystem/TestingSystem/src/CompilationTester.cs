using System.IO;

namespace TestingSystem
{
    /// <summary>
    /// Main component class. Compiles provided source and runnes *.exe files againts tests.
    /// </summary>
    public class CompilationTester
    {
        /// <summary>
        /// Represents settings of this component.
        /// </summary>
        private Settings settings;

        /// <summary>
        /// Represents the name of source and exe files.
        /// </summary>
        const string programName = "program";

        /// <summary>
        /// Tests provided program.
        /// </summary>
        /// 
        /// <param name="program">
        /// Program to test.
        /// </param>
        /// 
        /// <returns>
        /// Result of program testing.
        /// </returns>
        public Result TestProgram(Program program)
        {
            //search fo apropriate compiler
            Compile.Compiler currentCompiler = null;
            foreach (Compile.Compiler compiler in settings.Compilers)
            {
                if (compiler.Name == program.Language)
                {
                    currentCompiler = compiler;
                    break;
                }
            }
            //if no compiler found - throw the exception
            if (currentCompiler == null)
            {
                throw new Compile.CompileException("No compiler found for specified language");
            }

            //write source into a file
            string programDirectory = Path.Combine(settings.TestingDirectory, programName);
            Directory.CreateDirectory(programDirectory);
            string programPath = Path.Combine(programDirectory, programName + "." + currentCompiler.Extension);
            StreamWriter writer = new StreamWriter(programPath);
            writer.Write(program.Source);
            writer.Close();

            //copmile the source
            Compile.CompileResult compileResult = currentCompiler.Compile(programPath);

            Result testResult = null;
            if (compileResult.Compiled)
            {
                Runner executer = new Runner(settings.UserName, settings.Password);
                testResult = executer.Execute(Path.ChangeExtension(programPath, "exe"), program);
            }
            else
            {
                testResult = new Result();
                testResult.ProgramStatus = Status.CompilationError;
                testResult.Output = compileResult.StandartOutput;
            }
            try
            {
                Directory.Delete(programDirectory, true);
            }
            catch { }
            return testResult;
        }

        /// <summary>
        /// Creates new instance of <see cref="CompilationTester"/> class.
        /// </summary>
        /// 
        /// <param name="settings">
        /// Settings of this component.
        /// </param>
        public CompilationTester(Settings settings)
        {
            ProjectHelper.ValidateNotNull(settings, "settings");
            this.settings = settings;
        }

    }
}
