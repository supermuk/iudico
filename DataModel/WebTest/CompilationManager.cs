using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;
using TestingSystem;
using TestingSystem.Compile;
using CompilationTester = TestingSystem.Tester;

namespace IUDICO.DataModel.WebTest
{
    class CompilationManager
    {
        private readonly IList<TblCompiledQuestionsData> _compiledQuestionData;

        private IList<TblCompiledAnswers> _compiledAnswers; 

        private readonly Program _program;

        private readonly TblUserAnswers _userAnswer;


        public static CompilationManager GetNewManager(TblUserAnswers userAnswer)
        {
            return new CompilationManager(userAnswer);
        }


        public void StartCompilation()
        {
            SetCompilationStatusesToEnqueued();

            AddCompilationToTaskQueue();
        }

        public void ReCompile()
        {
            DeletePreviousCompiledAnswers(_userAnswer);
            StartCompilation();
        }


        private void AddCompilationToTaskQueue()
        {
            ThreadPool.QueueUserWorkItem(delegate { Compile(); });
        }

        private CompilationManager(TblUserAnswers userAnswer)
        {
            _userAnswer = userAnswer;

            var question = ServerModel.DB.Load<TblQuestions>((int)userAnswer.QuestionRef);

            var compiledQuestion = ServerModel.DB.Load<TblCompiledQuestions>((int)question.CompiledQuestionRef);

            _compiledQuestionData = ServerModel.DB.Load<TblCompiledQuestionsData>(ServerModel.DB.LookupIds<TblCompiledQuestionsData>(compiledQuestion, null));

            _program = SettingFactory.CreateProgram(_userAnswer, compiledQuestion);
        }

        private void SetCompilationStatusesToEnqueued()
        {
            _compiledAnswers = new List<TblCompiledAnswers>();

            foreach (var c in _compiledQuestionData)
            {
                var ca = StorePreCompiledAnswer(_userAnswer, c);
                _compiledAnswers.Add(ca);
            }
        }

        private void Compile()
        {
            var tester = SettingFactory.CreateTester();

            foreach (var c in _compiledAnswers)
            {
                var testCase = GetTestCaseData(c);

                _program.InputTest = testCase.Input;
                _program.OutputTest = testCase.Output.Replace("\r", string.Empty);

                var testCaseResult = tester.TestProgram(_program);

                StoreUpdatedCompiledAnswer(testCaseResult, c);
            }

        }


        private static void DeletePreviousCompiledAnswers(TblUserAnswers ua)
        {
            var compiledAnswersIds = ServerModel.DB.LookupIds<TblCompiledAnswers>(ua, null);
            ServerModel.DB.Delete<TblCompiledAnswers>(compiledAnswersIds);
        }

        private static TblCompiledQuestionsData GetTestCaseData(TblCompiledAnswers c)
        {
            return ServerModel.DB.Load<TblCompiledQuestionsData>(c.CompiledQuestionsDataRef);
        }

        private static void StoreUpdatedCompiledAnswer(Result res, TblCompiledAnswers compiledAnswer)
        {
            compiledAnswer.MemoryUsed = res.MemoryUsed;
            compiledAnswer.TimeUsed = res.TimeUsed;
            compiledAnswer.StatusRef = (int) res.ProgramStatus;
            compiledAnswer.Output = res.Output;
            
            ServerModel.DB.Update(compiledAnswer);
        }

        private static TblCompiledAnswers StorePreCompiledAnswer(TblUserAnswers ans, TblCompiledQuestionsData data)
        {
            var compiledAnswer = new TblCompiledAnswers
            {
                MemoryUsed = 0,
                TimeUsed = 0,
                StatusRef = (int)Status.Enqueued,
                Output = string.Empty,
                UserAnswerRef = ans.ID,
                CompiledQuestionsDataRef = data.ID
            };

            ServerModel.DB.Insert(compiledAnswer);

            return compiledAnswer;
        }
    }

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

        public static CompilationTester CreateTester()
        {
            return new CompilationTester(GetSettings());
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