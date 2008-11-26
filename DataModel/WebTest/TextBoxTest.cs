namespace IUDICO.DataModel.WebTest
{
    /// <summary>
    /// Summary description for IUDICO.DataModel.WebTest.TextBoxTest
    /// </summary>
    public class TextBoxTest : Test
    {
        public TextBoxTest(string userAnswer, int testId)
        {
            UserAnswer = userAnswer;
            Id = testId;
        }
    }
}