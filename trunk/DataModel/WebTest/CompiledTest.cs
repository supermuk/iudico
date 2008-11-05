namespace IUDICO.DataModel.WebTest
{
    class CompiledTest : Test
    {
        public CompiledTest(string userAnswer, long testId)
        {
            UserAnswer = userAnswer;
            Id = testId;
        }
    }
}