using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompileSystem.Testing_Part
{
    public class Status
    {
        public string TestResult { get; private set; }

        public Status(string testResult)
        {
            TestResult = testResult;
        }
    }
}