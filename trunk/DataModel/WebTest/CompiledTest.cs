namespace IUDICO.DataModel.WebTest
{
    class CompiledTest : Test
    {
        public CompiledTest(string userAnswer, int testId)
        {
            UserAnswer = userAnswer;
            Id = testId;
        }
    }
}