using System;
using System.Collections.Generic;
using System.Web.Security;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.WebTest
{
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
                var ua = new TblUserAnswers();
                ua.QuestionRef = t.Id;
                ua.Date = DateTime.Now;
                ua.UserRef = ((CustomUser) Membership.GetUser()).ID;
                ua.UserAnswer = t.UserAnswer;
                ua.IsCompiledAnswer = t is CompiledTest;
                ServerModel.DB.Insert(ua);
                
                if(ua.IsCompiledAnswer)
                    (new CompilationManager()).Compile(ua);
            }
        }
    }
}