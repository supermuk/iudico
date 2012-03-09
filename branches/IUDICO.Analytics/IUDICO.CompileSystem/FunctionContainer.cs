using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CompileSystem.Compiling;
using CompileSystem.Compiling.Compile;
using CompileSystem.Compiling.Run;

namespace CompileSystem
{
    public static class FunctionContainer
    {
        public static Settings CreateDefaultSetting()
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
            return settings;
        }

        public static Program CreateProgram(string source, int memoryLimit, int timeLimit)
        {
            var program = new Program {Source = source, MemoryLimit = memoryLimit, TimeLimit = timeLimit};
            return program;
        }

        public static CompilationTester CreateCompilationTester(Settings settings)
        {
            var tester = new CompilationTester(settings);
            return tester;
        }

        public static string AssignLanguageForProgram(string language, ref Program program)
        {
            switch (language)
            {
                case "CPP":
                    program.Language = Language.VC8;
                    break;
                case "CS":
                    program.Language = Language.CSharp3;
                    break;
                case "Delphi":
                    program.Language = Language.Delphi7;
                    break;
                case "Java":
                    program.Language = Language.Java6;
                    break;
                default:
                    return "Unsupported language";
            }

            return String.Empty;
        }
    }
}