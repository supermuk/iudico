/// <summary>
/// Summary description for SimpleQuestionTest
/// </summary>
public class SimpleQuestionTest : Test
{
    public SimpleQuestionTest(long testId, params bool[] list)
    {
        Id = testId;

        string ans = string.Empty;

        foreach (bool b in list)
            ans += b ? "1" : "0";

        UserAnswer = ans;
    }
}