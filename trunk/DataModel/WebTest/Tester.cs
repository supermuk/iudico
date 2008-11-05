using System.Collections.Generic;

/// <summary>
/// Summary description for PageTest
/// </summary>
public class Tester
{
    private readonly List<Test> tests = new List<Test>();

    public void AddTest(Test newTest)
    {
        tests.Add(newTest);
    }

    public void Submit()
    {
        foreach (Test t in tests)
        {
           // var uae = new UserAnswerEntity();
           // DaoFactory.UserAnswerDao.Insert(uae);
        }
    }
}