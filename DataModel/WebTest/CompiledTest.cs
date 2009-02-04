namespace IUDICO.DataModel.WebTest
{
    public class CompiledTest : Test
    {
        public CompiledTest(string userAnswer, int testId)
        {
            UserAnswer = userAnswer;
            Id = testId;
        }
    }
}