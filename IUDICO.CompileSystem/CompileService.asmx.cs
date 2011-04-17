using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;
using System.ComponentModel;
using CompileSystem.Compiling;
using CompileSystem.Compiling.Compile;

namespace CompileSystem
{
    /// <summary>
    /// Summary description for Compile
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
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
            var settings = new Settings
            {
                TestingDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()),
                Compilers = new List<Compiler>
                                                       {
                                                           Compiler.VC8Compiler,
                                                           Compiler.DotNet3Compiler,
                                                           Compiler.Delphi7Compiler,
                                                           Compiler.Java6Compiler
                                                       }
            };

            var p = new Program {Source = source, MemoryLimit = memorylimit, TimeLimit = timelimit};
            var tester = new CompilationTester(settings);
            Result testResult;

            switch (language)
            {
                case "CPP": p.Language = Language.VC8; break;
                case "CS": p.Language = Language.CSharp3; break;
                case "Delphi": p.Language = Language.Delphi7; break;
                case "Java": p.Language = Language.Java6; break;
            }

            var compileResult = tester.Compile(p.Source, p.Language);

            if (!compileResult.Compiled)
            {
                testResult = new Result
                {
                    ProgramStatus = Status.CompilationError,
                    Output = compileResult.StandartOutput
                };

                return Enum.GetName(typeof(Status), testResult.ProgramStatus);
            }

            for (var i = 0; i < input.Length; i++)
            {
                p.InputTest = input[i];
                p.OutputTest = output[i];

                var result = tester.TestProgram(p);

                if (result.ProgramStatus != Status.Accepted)
                {
                    return Enum.GetName(typeof(Status), result.ProgramStatus) + " Test: " + i;
                }
            }

            return "Accepted";
        }
    }
}