using CompileSystem;
using CompileSystem.Compiling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace IUDICO.UnitTests
{


    /// <summary>
    ///This is a test class for CompileServiceTest and is intended
    ///to contain all CompileServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CompileServiceTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for CS Compile with correct code
        ///</summary>
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectCSCompileTest()
        {
            CompileService compileService = new CompileService();

            string source = "using System;namespace MyProg{internal class Program{private static void Main(string[] args){string ar = Console.ReadLine();string result;string first, second;first = ar.Split(' ')[0];second = ar.Split(' ')[1];result = first.Insert(1,second);Console.Write(result);}}}";
            string language = "CS";
            string[] input = { "2 5" };//{"a", "b", "c", "d", "e"};
            string[] output = { "25" };//{"abcde"};
            int timelimit = 100000;
            int memorylimit = 1000;
            string expectedResult = "Accepted";
            string actualResult = compileService.Compile(source, language, input, output, timelimit, memorylimit);
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        ///A test for CS Compile with incorrect code
        ///</summary>
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectCSCompileTest()
        {
            CompileService compileService = new CompileService();

            string source = "System;namespace MyProg{internal class Program{private static void Main(string[] args){string ar = Console.ReadLine();string result;string first, second;first = ar.Split(' ')[0];second = ar.Split(' ')[1];result = first.Insert(1,second);Console.Write(result);}}}";
            string language = "CS";
            string[] input = { "2 5" };
            string[] output = { "25" };
            int timelimit = 100000;
            int memorylimit = 1000;
            string expectedResult = "Accepted";
            string actualResult = compileService.Compile(source, language, input, output, timelimit, memorylimit);
            Assert.AreNotEqual(expectedResult, actualResult);
        }

        /// <summary>
        ///A test for Java Compile with correct code
        ///</summary>
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectJavaCompileTest()
        {
            CompileService compileService = new CompileService();

            string source = "package com.baik;\n import java.io.BufferedReader;\nimport java.io.IOException;\nimport java.io.InputStreamReader;\npublic class Main \n{\npublic static void main(String[] args) throws IOException \n{\nStringBuilder builder = new StringBuilder();\nInputStreamReader input = new InputStreamReader(System.in);\nBufferedReader reader = new BufferedReader(input);\nString inputLine = reader.readLine();\nString first = inputLine.substring(0,1);\n String second = inputLine.substring(2,3);\n String result = first.concat(second);\n System.out.println(result);\n}\n}";
            string language = "Java";
            string[] input = { "2 5" };
            string[] output = { "25" };
            int timelimit = 100000;
            int memorylimit = 1000;
            string expectedResult = "Accepted";
            string actualResult = compileService.Compile(source, language, input, output, timelimit, memorylimit);
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        ///A test for C++ Compile with correct code
        ///</summary>
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectCPLusPlusCompileTest()
        {
            CompileService compileService = new CompileService();

            string source = "#include<iostream>\n#include<string>\nusing namespace std;\nvoid main(){\nstring a,b;\ncin>>a>>b;\ncout<<a<<b;}";
            string language = "CPP";
            string[] input = { "2 5" };
            string[] output = { "25" };
            int timelimit = 100000;
            int memorylimit = 1000;
            string expectedResult = "Accepted";
            string actualResult = compileService.Compile(source, language, input, output, timelimit, memorylimit);
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        ///A test for C++ Compile with incorrect code
        ///</summary>
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectCPLusPlusCompileTest()
        {
            CompileService compileService = new CompileService();

            string source = "include<iostream>\n#include<string>\nusing namespace std;\nvoid main(){\nstring a,b;\ncin>>a>>b;\ncout<<a<<b;}";
            string language = "CPP";
            string[] input = { "2 5" };
            string[] output = { "25" };
            int timelimit = 100000;
            int memorylimit = 1000;
            string expectedResult = "Accepted";
            string actualResult = compileService.Compile(source, language, input, output, timelimit, memorylimit);
            Assert.AreNotEqual(expectedResult, actualResult);
        }

        /// <summary>
        ///A test for Delphi Compile with correct code
        ///</summary>
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectDelphiCompileTest()
        {
            CompileService compileService = new CompileService();
            string source = "program Helloworld; \n{$APPTYPE CONSOLE}\n begin\n writeln('Hello, world!');\nend.";
            string language = "Delphi";
            string[] input = { "" };
            string[] output = { "Hello, world!" };
            int timelimit = 100000;
            int memorylimit = 1000;
            string expectedResult = "Accepted";
            string actualResult = compileService.Compile(source, language, input, output, timelimit, memorylimit);
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        ///A test for Delphi Compile with incorrect code
        ///</summary>
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectDelphiCompileTest()
        {
            CompileService compileService = new CompileService();
            string source = "Helloworld; \n{$APPTYPE CONSOLE}\n begin\n writeln('Hello, world!');\nend.";
            string language = "Delphi";
            string[] input = { "" };
            string[] output = { "Hello, world!" };
            int timelimit = 100000;
            int memorylimit = 1000;
            string expectedResult = "Accepted";
            string actualResult = compileService.Compile(source, language, input, output, timelimit, memorylimit);
            Assert.AreNotEqual(expectedResult, actualResult);
        }
    }
}
