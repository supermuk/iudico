using System;
using System.Collections.Generic;
using System.IO;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;
using TestingSystem;
using TestingSystem.Compile;
using CompilationTester = TestingSystem.Tester;

namespace IUDICO.DataModel.Common.TestingUtils
{
    static class SettingFactory
    {
        private const string DotNetPath = @"Compilers\dotNET\csc.exe";
        private const string DelphiPath = @"Compilers\Delphi7\Dcc32.exe";
        private const string Vc6Path = @"Compilers\VC6\CL.EXE";

        private const string DotNetLanguage = "CS dotNET 2.0";
        private const string DelphiLanguage = "Dcc32";
        private const string Vc6Language = "VC6";


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

            settings.Compilers.Add(new Compiler(DotNetLanguage, Path.Combine(baseDirectory, DotNetPath), "/t:exe $SourceFilePath$", "cs"));
            settings.Compilers.Add(new Compiler(DelphiLanguage, Path.Combine(baseDirectory, DelphiPath), "-U\"$CompilerDirectory$\" $SourceFilePath$", "pas"));
            settings.Compilers.Add(new Compiler(Vc6Language, Path.Combine(baseDirectory, Vc6Path), "/I\"$CompilerDirectory$\" $SourceFilePath$ /link /LIBPATH:\"$CompilerDirectory$\"", "cpp"));
            
            return settings;
        }

        public static Tester CreateTester()
        {
            return new Tester(GetSettings());
        }

        public static Program CreateProgram(TblUserAnswers ua, TblCompiledQuestions cq)
        {
            return new Program
                       {
                           Source = ua.UserAnswer,
                           MemoryLimit = (int) cq.MemoryLimit,
                           TimeLimit = (int) cq.TimeLimit,
                           Language = LanguageName(cq.LanguageRef)
                       };
        }

        private static string LanguageName(int languageRef)
        {
            switch ((FX_LANGUAGE)languageRef)
            {
                case (FX_LANGUAGE.Cpp):
                    {
                        return Vc6Language;
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