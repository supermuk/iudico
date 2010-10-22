using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using TestingSystem.Compile;
using TestingSystem;

using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace WebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class CompileService : System.Web.Services.WebService
    {
        /// <summary>
        /// Creates and compile program
        /// </summary>
        /// <param name="source">User source</param>
        /// <param name="language">"CPP" - C++, "CS" - C#, "Delphi" - Delphi, "Java" - Java</param>
        /// <param name="input">Serialized array of input data </param>
        /// <param name="output">Serialized array of output data</param>
        /// <param name="timeLimit">Time limit for program</param>
        /// <param name="memoryLimit">Memory limit for program</param>
        /// <returns>"Accepted" if program status Accepted, ProgramStatus + index of failed test in other cases</returns>
        [WebMethod]
        public string Compile(string source, string language, string[] input, string[] output, int timelimit, int memorylimit)
        {

            
            for (int i = 0; i < input.Length; i++)
            {
                Program p = new Program();
                p.Source = source;
                p.MemoryLimit = memorylimit;
                p.TimeLimit = timelimit;
                p.InputTest = input[i];
                p.OutputTest = output[i];
               
                switch (language)
                {
                    case "CPP": p.Language = Language.VC8; break;
                    case "CS": p.Language = Language.CSharp3; break;
                    case "Delphi": p.Language = Language.Delphi7; break;
                    case "Java": p.Language = Language.Java6; break;
                }

                var settings = new Settings();
                settings.Compilers = new List<Compiler>();
                settings.TestingDirectory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString());
                settings.Compilers.Add(Compiler.VC8Compiler);
                settings.Compilers.Add(Compiler.DotNet3Compiler);
                settings.Compilers.Add(Compiler.Delphi7Compiler);
                settings.Compilers.Add(Compiler.Java6Compiler);

                CompilationTester tester = new CompilationTester(settings);

                Result result = tester.TestProgram(p);
                
                if (result.ProgramStatus != Status.Accepted)
                {
                    return System.Enum.GetName(typeof(Status), result.ProgramStatus) + " Test: " + i.ToString();
                }
            }

            return "Accepted";
        }
        /// <summary>
        /// Just to test connection
        /// </summary>
        /// <returns>string "Hello World!"</returns>
        [WebMethod()]
        public string Hello()
        {
            return "HelloWorld!";
        }
    }
}
