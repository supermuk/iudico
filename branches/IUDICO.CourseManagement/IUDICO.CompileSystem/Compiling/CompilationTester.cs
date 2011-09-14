using System.IO;
using System.Linq;
using CompileSystem.Compiling.Compile;
using CompileSystem.Compiling.Run;

namespace CompileSystem.Compiling
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
        /// Path, where compilation/testing takes place
        /// </summary>
        protected string _ProgramDirectory;

        /// <summary>
        /// Path to executable
        /// </summary>
        protected string _ProgramPath;

        /// <summary>
        /// Compile program
        /// </summary>
        /// <param name="source">Source code</param>
        /// <param name="language">Language</param>
        /// <returns></returns>
        public CompileResult Compile(string source, Language language)
        {
            //search for apropriate compiler
            var currentCompiler = Settings.Compilers.FirstOrDefault(compiler => compiler.Name == language);

            //if no compiler found - throw the exception
            if (currentCompiler == null)
            {
                throw new CompileException("No compiler found for specified language");
            }

            //write source into a file
            _ProgramDirectory = Path.Combine(Settings.TestingDirectory, ProgramName);
            _ProgramPath = Path.Combine(_ProgramDirectory, ProgramName + "." + currentCompiler.Extension);

            Directory.CreateDirectory(_ProgramDirectory);

            if (language == Language.Java6)
            {
                const string classStr = "class";
                const string packageStr = "package";

                var packageIndex = source.IndexOf(packageStr);

                if (packageIndex != -1)
                {
                    try
                    {
                       source.Remove(packageIndex,source.IndexOf(";", packageIndex) - packageIndex + 1);
                    }
                    catch
                    {
                    }
                }

                string className;

                try
                {
                    className =
                        source.Substring(source.IndexOf(classStr) + classStr.Length,
                                         source.IndexOf("{", source.IndexOf(classStr)) -
                                         (source.IndexOf(classStr) + classStr.Length)).Trim();
                }
                catch
                {
                    className = ProgramName;
                }

                if (Path.GetInvalidFileNameChars().Any(invalidChar => className.Contains(invalidChar.ToString())))
                {
                    className = ProgramName;
                }

                _ProgramPath = Path.Combine(_ProgramDirectory, className + "." + currentCompiler.Extension);
            }

            var writer = new StreamWriter(_ProgramPath);
            writer.Write(source);
            writer.Close();

            //copmile the source
            return currentCompiler.Compile(_ProgramPath);
        }

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
            var testResult = Runner.Execute(Path.ChangeExtension(_ProgramPath, "exe"), program);

            return testResult;
        }

        public void FinishTesting()
        {
            try
            {
                Directory.Delete(_ProgramDirectory, true);
            }
            catch
            {
            }
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
            
            Settings = settings;
        }

    }
}
