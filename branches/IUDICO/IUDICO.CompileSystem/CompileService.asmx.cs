using System;
using System.IO;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.ComponentModel;
using CompileSystem.Classes.Compiling;
using CompileSystem.Classes.Testing;

namespace CompileSystem
{
    /// <summary>
    /// Summary description for Compile
    /// </summary>
    [WebService(Namespace = "http://tests-ua.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class CompileService : WebService
    {
        /// <summary>
        /// Creates and compiles program
        /// </summary>
        /// <param name="source">User source code</param>
        /// <param name="language">"CPP" - C++, "CS" - C#, "Delphi" - Delphi, "Java" - Java</param>
        /// <param name="input">Serialized array of input data </param>
        /// <param name="output">Serialized array of output data</param>
        /// <param name="timelimit">Time limit for program</param>
        /// <param name="memorylimit">Memory limit for program</param>
        /// <returns>"Accepted" if program status Accepted, ProgramStatus + index of failed test in other cases</returns>
        [WebMethod]
        public string Compile(string source, string language, string[] input, string[] output, int timelimit, int memorylimit)
        {
            //remove after testing
            const string compilerDirectory = "Compilers";
            var compilers = new Compilers(compilerDirectory);
            compilers.Load();
            //----

            if (string.IsNullOrEmpty(language))
                throw new Exception("Bad language name");

            source = HttpUtility.UrlDecode(source);
            Compiler currentCompiler = compilers.GetCompiler(language);

            if (currentCompiler == null)
                throw new Exception("Can't find compiler with such name");

            string compileFilePath = Classes.Helper.CreateFileForCompilation(source, currentCompiler.Extension);

            var compileTask = new CompileTask(currentCompiler, compileFilePath);

            if (!compileTask.Execute())
                return "CompilationError";

            var executeFilePath = Path.ChangeExtension(compileFilePath, currentCompiler.CompiledExtension);

            for (int i = 0; i < input.Length; i++)
            {
                var currentStatus = Tester.Test(executeFilePath, input[i], output[i], timelimit, memorylimit);

                if (currentStatus.TestResult != "Accepted")
                {
                    currentStatus.TestResult = currentStatus.TestResult + " Test: " + i;
                    return currentStatus.TestResult;
                }
            }

            return "Accepted";
        }
    }
}