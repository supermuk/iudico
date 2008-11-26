namespace IUDICO.DataModel.WebTest
{
    /// <summary>
    /// Summary description for IUDICO.DataModel.WebTest.SimpleQuestionTest
    /// </summary>
    public class SimpleQuestionTest : Test
    {
        public SimpleQuestionTest(int testId, params bool[] list)
        {
            Id = testId;

            string ans = string.Empty;

            foreach (bool b in list)
                ans += b ? "1" : "0";

            UserAnswer = ans;
        }
    }
}