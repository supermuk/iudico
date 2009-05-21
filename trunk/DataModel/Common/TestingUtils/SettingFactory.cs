using System;
using System.Collections.Generic;
using System.IO;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.DB;
using TestingSystem;
using TestingSystem.Compile;

namespace IUDICO.DataModel.Common.TestingUtils
{
    static class SettingFactory
    {
        private static Settings GetSettings()
        {
            var settings = new Settings
                               {
                                   Compilers = new List<Compiler>(),
                                   TestingDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()),
                               };

            settings.Compilers.Add(Compiler.VC8Compiler);
            settings.Compilers.Add(Compiler.VC6Compiler);
            settings.Compilers.Add(Compiler.Delphi7Compiler);
            settings.Compilers.Add(Compiler.Java6Compiler);
            settings.Compilers.Add(Compiler.DotNet2Compiler);
            settings.Compilers.Add(Compiler.DotNet3Compiler);

            return settings;
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
                           Language = LanguageHelper.FxLanguagesToLanguage(cq.LanguageRef)
                       };
        }
    }
}