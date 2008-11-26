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
                if(t is CompiledTest)
                {
                    

                }
                else
                {
                    ua.UserAnswer = t.UserAnswer;
                    ua.IsCompiledAnswer = false;
                }
                ServerModel.DB.Insert(ua);
            }
        }
    }
}