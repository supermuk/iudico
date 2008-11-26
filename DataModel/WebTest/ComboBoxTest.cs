namespace IUDICO.DataModel.WebTest
{
    /// <summary>
    /// Summary description for IUDICO.DataModel.WebTest.ComboBoxTest
    /// </summary>
    public class ComboBoxTest : Test
    {
        public ComboBoxTest(int userAnswer, int testId)
        {
            Id = testId;

            UserAnswer = userAnswer.ToString();
        }
    }
}