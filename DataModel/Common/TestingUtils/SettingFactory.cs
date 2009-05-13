using System;
using System.Collections.Generic;
using System.IO;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;
using TestingSystem;
using TestingSystem.Compile;

namespace IUDICO.DataModel.Common.TestingUtils
{
    static class SettingFactory
    {
        private const string DotNetPath = @"Compilers\dotNET\csc.exe";
        private const string DelphiPath = @"Compilers\Delphi7\Dcc32.exe";
        private const string Vc6Path = @"Compilers\VC6\CL.EXE";
        private const string Vc8Path = @"Compilers\VC8\CL.EXE";

        private const string DotNetLanguage = "CS dotNET 3.5";
        private const string DelphiLanguage = "Dcc32";
        private const string Vc6Language = "VC6";
        private const string Vc8Language = "VC8";


        private static Settings GetSettings()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            var settings = new Settings
                               {
                                   Compilers = new List<Compiler>(),
                                   TestingDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()),
                                   UserName = "",
                                   Password = ""
                               };

            AddDotNetCompilersToSettings(settings, baseDirectory);
            AddDelphiCompilersToSettings(settings, baseDirectory);
            AddVc6CompilersToSettings(settings, baseDirectory);
            AddVc8CompilersToSettings(settings, baseDirectory);
            return settings;
        }

        private static void AddVc8CompilersToSettings(Settings settings, string baseDirectory)
        {
            settings.Compilers.Add(new Compiler(Vc8Language, Path.Combine(baseDirectory, Vc8Path), "/I\"$CompilerDirectory$\" $SourceFilePath$ /link /LIBPATH:\"$CompilerDirectory$\"", "cpp"));
        }

        private static void AddVc6CompilersToSettings(Settings settings, string baseDirectory)
        {
            settings.Compilers.Add(new Compiler(Vc6Language, Path.Combine(baseDirectory, Vc6Path), "/I\"$CompilerDirectory$\" $SourceFilePath$ /link /LIBPATH:\"$CompilerDirectory$\"", "cpp"));
        }

        private static void AddDelphiCompilersToSettings(Settings settings, string baseDirectory)
        {
            settings.Compilers.Add(new Compiler(DelphiLanguage, Path.Combine(baseDirectory, DelphiPath), "-U\"$CompilerDirectory$\" $SourceFilePath$", "pas"));
        }

        private static void AddDotNetCompilersToSettings(Settings settings, string baseDirectory)
        {
            const string reference = "/reference:";
            var referenceList = new List<string>
                                    { "System.Core.dll", "System.Xml.Linq.dll", 
                                                     "System.WorkflowServices.dll", "System.Net.dll","System.Data.Linq.dll","System.Data.Entity.dll","System.AddIn.dll" };
            string allReferences = "";
            foreach (string systemReference in referenceList)
            {
                allReferences += reference + systemReference + " ";
            }

            settings.Compilers.Add(new Compiler(DotNetLanguage, Path.Combine(baseDirectory, DotNetPath), @"/t:exe " + allReferences + "$SourceFilePath$", "cs"));
        }

        public static CompilationTester CreateTester()
        {
            return new CompilationTester(GetSettings());
        }

        public static Program CreateProgram(TblUserAnswers ua, TblCompiledQuestions cq)
        {
            return new Program
                       {
                           Source = ua.UserAnswer,
                           MemoryLimit = (int)cq.MemoryLimit,
                           TimeLimit = (int)cq.TimeLimit,
                           Language = LanguageName(cq.LanguageRef)
                       };
        }

        private static string LanguageName(int languageRef)
        {
            switch ((FX_LANGUAGE)languageRef)
            {
                case (FX_LANGUAGE.Cpp):
                    {
                        return Vc8Language;
                    }
                case (FX_LANGUAGE.Delphi):
                    {
                        return DelphiLanguage;
                    }
                case (FX_LANGUAGE.CS):
                    {
                        return DotNetLanguage;
                    }
            }
            return string.Empty;
        }
    }
}