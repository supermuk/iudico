using System;

namespace CompileSystem.Classes.Testing
{
    public class Status
    {
        public string TestResult { get; set; }

        public Status(string testResult)
        {
            if(string.IsNullOrEmpty(testResult))
                throw new Exception("Bad input string");

            TestResult = testResult;
        }
    }
}