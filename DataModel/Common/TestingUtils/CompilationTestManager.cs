using System.Collections.Generic;
using System.Threading;
using IUDICO.DataModel.DB;
using TestingSystem;

namespace IUDICO.DataModel.Common.TestingUtils
{
    class CompilationTestManager
    {
        private readonly IList<TblCompiledQuestionsData> _compiledQuestionData;

        private IList<TblCompiledAnswers> _compiledAnswers; 

        private readonly Program _program;

        private readonly TblUserAnswers _userAnswer;


        public static CompilationTestManager GetNewManager(TblUserAnswers userAnswer)
        {
            return new CompilationTestManager(userAnswer);
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

        private CompilationTestManager(TblUserAnswers userAnswer)
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

                PrepateTestCaseForTesting(testCase);

                var testCaseResult = tester.TestProgram(_program);

                StoreUpdatedCompiledAnswer(testCaseResult, c);
            }

        }

        private void PrepateTestCaseForTesting(TblCompiledQuestionsData testCase)
        {
            _program.InputTest = testCase.Input;
            _program.OutputTest = testCase.Output.Replace("\r", string.Empty);
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
}