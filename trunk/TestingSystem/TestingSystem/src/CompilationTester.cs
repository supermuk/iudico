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
        public readonly Settings Settings;

        /// <summary>
        /// Represents the name of source and exe files.
        /// </summary>
        public const string ProgramName = "program";

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
            //search for apropriate compiler
            Compile.Compiler currentCompiler = null;
            foreach (Compile.Compiler compiler in Settings.Compilers)
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
            string programDirectory = Path.Combine(Settings.TestingDirectory, ProgramName);
            Directory.CreateDirectory(programDirectory);

            string programPath = Path.Combine(programDirectory, ProgramName + "." + currentCompiler.Extension);
            if (program.Language == Language.Java6)
            {
                const string classStr = "class";
                string className;
                try
                {
                    className = program.Source.Substring(
                        program.Source.IndexOf(classStr) + classStr.Length,
                        program.Source.IndexOf("{", program.Source.IndexOf(classStr)) - (program.Source.IndexOf(classStr) + classStr.Length)
                        );
                    className = className.Trim();
                }
                catch
                {
                    className = ProgramName;
                }
                foreach (char invalidChar in Path.GetInvalidFileNameChars())
                {
                    if (className.Contains(invalidChar.ToString()))
                    {
                        className = ProgramName;
                        break;
                    }
                }


                programPath = Path.Combine(programDirectory, className + "." + currentCompiler.Extension);

            }
            StreamWriter writer = new StreamWriter(programPath);
            writer.Write(program.Source);
            writer.Close();

            //copmile the source
            Compile.CompileResult compileResult = currentCompiler.Compile(programPath);

            Result testResult = null;
            if (compileResult.Compiled)
            {
                testResult = Runner.Execute(Path.ChangeExtension(programPath, "exe"), program);
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
            catch
            { }
            return testResult;
        }

        /// <summary>
        /// Creates new instance of <see cref="Tester"/> class.
        /// </summary>
        /// 
        /// <param name="settings">
        /// Settings of this component.
        /// </param>
        public CompilationTester(Settings settings)
        {
            ProjectHelper.ValidateNotNull(settings, "settings");
            this.Settings = settings;
        }

    }
}
