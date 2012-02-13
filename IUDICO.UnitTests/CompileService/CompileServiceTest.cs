using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace IUDICO.UnitTests.CompileService
{
    /// <summary>
    ///This is a test class for CompileServiceTest and is intended
    ///to contain all CompileServiceTest Unit Tests
    ///</summary>
    [TestClass]
    public class CompileServiceTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }
        //----------------------------------------------
        static private readonly CompileSystem.CompileService _compileService = new CompileSystem.CompileService();
        //----------------------------------------------
        private const string CorrectCSSourceCode = "using System;namespace MyProg{internal class Program{private static void Main(string[] args){string ar = Console.ReadLine();string result;string first, second;first = ar.Split(' ')[0];second = ar.Split(' ')[1];result = first.Insert(1,second);Console.Write(result);}}}";
        private const string CorrectCPPsourceCode = "#include<iostream>\n#include<string>\nusing namespace std;\nvoid main(){\nstring a,b;\ncin>>a>>b;\ncout<<a<<b;}";
        private const string CorrectJavaSourceCode = "package com.baik;\n import java.io.BufferedReader;\nimport java.io.IOException;\nimport java.io.InputStreamReader;\npublic class Main \n{\npublic static void main(String[] args) throws IOException \n{\nStringBuilder builder = new StringBuilder();\nInputStreamReader input = new InputStreamReader(System.in);\nBufferedReader reader = new BufferedReader(input);\nString inputLine = reader.readLine();\nString first = inputLine.substring(0,1);\n String second = inputLine.substring(2,3);\n String result = first.concat(second);\n System.out.println(result);\n}\n}";
        private const string CorrectDelphiSourceCode = "program Helloworld; \n{$APPTYPE CONSOLE}\n begin\n writeln('Hello, world!');\nend.";

        //+using
        private const string IncorrectCSSourceCode = "System;namespace MyProg{internal class Program{private static void Main(string[] args){string ar = Console.ReadLine();string result;string first, second;first = ar.Split(' ')[0];second = ar.Split(' ')[1];result = first.Insert(1,second);Console.Write(result);}}}";
        //+using
        private const string IncorrectCPPsourceCode = "include<iostream>\n#include<string>\nusing namespace std;\nvoid main(){\nstring a,b;\ncin>>a>>b;\ncout<<a<<b;}";
        //+package
        private const string IncorrectJavaSourceCode = "com.baik;\n import java.io.BufferedReader;\nimport java.io.IOException;\nimport java.io.InputStreamReader;\npublic class Main \n{\npublic static void main(String[] args) throws IOException \n{\nStringBuilder builder = new StringBuilder();\nInputStreamReader input = new InputStreamReader(System.in);\nBufferedReader reader = new BufferedReader(input);\nString inputLine = reader.readLine();\nString first = inputLine.substring(0,1);\n String second = inputLine.substring(2,3);\n String result = first.concat(second);\n System.out.println(result);\n}\n}";
        //+program
        private const string IncorrectDelphiSourceCode = "Helloworld; \n{$APPTYPE CONSOLE}\n begin\n writeln('Hello, world!');\nend.";
        //----------------------------------------------
        private const string CSLanguageType = "CS";
        private const string CPPlanguageType = "CPP";
        private const string JavaLanguageType = "Java";
        private const string DelphiLanguageType = "Delphi";
        //----------------------------------------------
        private readonly string[] _emptyInput = new string[0];
        private readonly string[] _emptyOutput = new string[0];
        //----------------------------------------------
        private const int Timelimit = 100000;
        private const int Memorylimit = 1000;
        //----------------------------------------------
        private const string AcceptedTestResult = "Accepted";
        private const string CompilationErrorResult = "CompilationError";
        private const string WrongAnswerNullResult = "WrongAnswer Test: 0";
        private const string WrongAnswerOneResult = "WrongAnswer Test: 1";

        #region Source tests

        #region CS tests

        /// <summary>
        ///(CS) A test for CS Compile with correct code
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectSourceCSCompileTest()
        {
            string actualResult = _compileService.Compile(CorrectCSSourceCode, CSLanguageType, _emptyInput, _emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///(CS) A test for CS Compile with incorrect code
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectSourceCSCompileTest()
        {
            string actualResult = _compileService.Compile(IncorrectCSSourceCode, CSLanguageType, _emptyInput, _emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(CompilationErrorResult, actualResult);
        }

        #endregion

        #region CPP tests

        /// <summary>
        ///(CPP) A test for C++ Compile with correct code
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectSourceCPLusPlusCompileTest()
        {
            string actualResult = _compileService.Compile(CorrectCPPsourceCode, CPPlanguageType, _emptyInput, _emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///(CPP) A test for C++ Compile with incorrect code
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectSourceCPLusPlusCompileTest()
        {
            string actualResult = _compileService.Compile(IncorrectCPPsourceCode, CPPlanguageType, _emptyInput, _emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(CompilationErrorResult, actualResult);
        }

        #endregion

        #region Java tests

        /// <summary>
        ///(Java) A test for Java Compile with correct code
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectSourceJavaCompileTest()
        {
            string actualResult = _compileService.Compile(CorrectJavaSourceCode, JavaLanguageType, _emptyInput, _emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///(Java) A test for Java Compile with incorrect code
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectSourceJavaCompileTest()
        {
            string actualResult = _compileService.Compile(IncorrectJavaSourceCode, JavaLanguageType, _emptyInput, _emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(CompilationErrorResult, actualResult);
        }

        #endregion

        #region Delphi tests

        /// <summary>
        ///(Delphi) A test for Delphi Compile with correct code
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectSourceDelphiCompileTest()
        {
            string actualResult = _compileService.Compile(CorrectDelphiSourceCode, DelphiLanguageType, _emptyInput, _emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///(Delphi) A test for Delphi Compile with incorrect code
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectSourceDelphiCompileTest()
        {
            string actualResult = _compileService.Compile(IncorrectDelphiSourceCode, DelphiLanguageType, _emptyInput, _emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(CompilationErrorResult, actualResult);
        }

        #endregion

        #endregion

        #region Language tests

        /// <summary>
        ///A test with correct language attribute
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectLanguageCompileTest()
        {
            string actualResult = _compileService.Compile(CorrectCSSourceCode, CSLanguageType, _emptyInput, _emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///A test with incorrect language(CS = real; CPP = attribute 1 , Java = attribute 2) attribute
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectLanguageCompileTest()
        {
            //CS and CPP
            string actualResult = _compileService.Compile(CorrectCSSourceCode, CPPlanguageType, _emptyInput, _emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(CompilationErrorResult, actualResult);

            //CS and Java
            actualResult = _compileService.Compile(CorrectCSSourceCode, JavaLanguageType, _emptyInput, _emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(CompilationErrorResult, actualResult);
        }

        /// <summary>
        ///A test with correct source code and null/unknown/known(lower-case) language attribute
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectLanguageCompileTest2()
        {
            string languageType = "";

            string actualResult = _compileService.Compile(CorrectCSSourceCode, languageType, _emptyInput, _emptyOutput, Timelimit, Memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            languageType = "CSharp";
            actualResult = _compileService.Compile(CorrectCSSourceCode, languageType, _emptyInput, _emptyOutput, Timelimit, Memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            languageType = "cs";
            actualResult = _compileService.Compile(CorrectCSSourceCode, languageType, _emptyInput, _emptyOutput, Timelimit, Memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);
        }

        #endregion

        #region Timelimit tests

        #region CS tests

        /// <summary>
        ///(CS) A test with correct timelimit value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectCSTimelimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            const int timelimit = 100000;
            string actualResult = _compileService.Compile(CorrectCSSourceCode, CSLanguageType, input, output, timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///(CS) A test with 0/less than 0/less than it needs timelimit value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectCSTimelimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            int incorrectTimelimit = 0;
            string actualResult = _compileService.Compile(CorrectCSSourceCode, CSLanguageType, input, output, incorrectTimelimit, Memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            incorrectTimelimit = -5;
            actualResult = _compileService.Compile(CorrectCSSourceCode, CSLanguageType, input, output, incorrectTimelimit, Memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            incorrectTimelimit = 1;
            actualResult = _compileService.Compile(CorrectCSSourceCode, CSLanguageType, input, output, incorrectTimelimit, Memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);
        }

        #endregion

        #region Java

        /// <summary>
        ///(Java) A test with correct timelimit value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectJavaTimelimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };
            const int timelimit = 100000;
            string actualResult = _compileService.Compile(CorrectJavaSourceCode, JavaLanguageType, input, output, timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///(Java) A test with 0/less than 0/less than it needs timelimit value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectJavaTimelimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            int incorrectTimelimit = 0;
            string actualResult = _compileService.Compile(CorrectJavaSourceCode, JavaLanguageType, input, output, incorrectTimelimit, Memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            incorrectTimelimit = -5;
            actualResult = _compileService.Compile(CorrectJavaSourceCode, JavaLanguageType, input, output, incorrectTimelimit, Memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            incorrectTimelimit = 1;
            actualResult = _compileService.Compile(CorrectJavaSourceCode, JavaLanguageType, input, output, incorrectTimelimit, Memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);
        }

        #endregion

        #region CPP

        /// <summary>
        ///(CPP) A test with correct timelimit value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectCPPtimelimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };
            const int timelimit = 100000;
            string actualResult = _compileService.Compile(CorrectCPPsourceCode, CPPlanguageType, input, output, timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///(CPP) A test with 0/less than 0/less than it needs timelimit value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectCPPtimelimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            int incorrectTimelimit = 0;
            string actualResult = _compileService.Compile(CorrectCPPsourceCode, CPPlanguageType, input, output, incorrectTimelimit, Memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            incorrectTimelimit = -5;
            actualResult = _compileService.Compile(CorrectCPPsourceCode, CPPlanguageType, input, output, incorrectTimelimit, Memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            incorrectTimelimit = 1;
            actualResult = _compileService.Compile(CorrectCPPsourceCode, CPPlanguageType, input, output, incorrectTimelimit, Memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);
        }

        #endregion

        #region Delphi

        /// <summary>
        ///(Delphi) A test with correct timelimit value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectDeplhiTimelimitCompileTest()
        {
            string[] input = { "" };
            string[] output = { "Hello, world!" };
            const int timelimit = 100000;
            string actualResult = _compileService.Compile(CorrectDelphiSourceCode, DelphiLanguageType, input, output, timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///(Delphi) A test with 0/less than 0/less than it needs timelimit value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectDelphiTimelimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            int incorrectTimelimit = 0;
            string actualResult = _compileService.Compile(CorrectDelphiSourceCode, DelphiLanguageType, input, output, incorrectTimelimit, Memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            incorrectTimelimit = -5;
            actualResult = _compileService.Compile(CorrectDelphiSourceCode, DelphiLanguageType, input, output, incorrectTimelimit, Memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            incorrectTimelimit = 1;
            actualResult = _compileService.Compile(CorrectDelphiSourceCode, DelphiLanguageType, input, output, incorrectTimelimit, Memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);
        }

        #endregion

        #endregion

        #region Memorylimit tests

        #region CS tests

        /// <summary>
        ///(CS) A test with correct memorylimit value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectCSMemorylimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };
            const int memorylimit = 1000;
            string actualResult = _compileService.Compile(CorrectCSSourceCode, CSLanguageType, input, output, Timelimit, memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///(CS) A test with memorylimit less that needs/0/-1 value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectCSMemorylimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            int memorylimit = 1;
            string actualResult = _compileService.Compile(CorrectCSSourceCode, CSLanguageType, input, output, Timelimit, memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            memorylimit = 0;
            actualResult = _compileService.Compile(CorrectCSSourceCode, CSLanguageType, input, output, Timelimit, memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            memorylimit = -1;
            actualResult = _compileService.Compile(CorrectCSSourceCode, CSLanguageType, input, output, Timelimit, memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);
        }

        #endregion

        #region CPP tests

        /// <summary>
        ///(CPP) A test with correct memorylimit value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectCPPmemorylimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };
            const int memorylimit = 1000;
            string actualResult = _compileService.Compile(CorrectCPPsourceCode, CPPlanguageType, input, output, Timelimit, memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///(CPP) A test with memorylimit less that needs/0/-1 value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectCPPmemorylimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            int memorylimit = 1;
            string actualResult = _compileService.Compile(CorrectCPPsourceCode, CPPlanguageType, input, output, Timelimit, memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            memorylimit = 0;
            actualResult = _compileService.Compile(CorrectCPPsourceCode, CPPlanguageType, input, output, Timelimit, memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            memorylimit = -1;
            actualResult = _compileService.Compile(CorrectCPPsourceCode, CPPlanguageType, input, output, Timelimit, memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);
        }

        #endregion

        #region Java tests

        /// <summary>
        ///(Java) A test with correct memorylimit value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectJavaMemorylimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };
            const int memorylimit = 1000;
            string actualResult = _compileService.Compile(CorrectJavaSourceCode, JavaLanguageType, input, output, Timelimit, memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///(Java) A test with memorylimit less that needs/0/-1 value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectJavaMemorylimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            int memorylimit = 1;
            string actualResult = _compileService.Compile(CorrectJavaSourceCode, JavaLanguageType, input, output, Timelimit, memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            memorylimit = 0;
            actualResult = _compileService.Compile(CorrectJavaSourceCode, JavaLanguageType, input, output, Timelimit, memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            memorylimit = -1;
            actualResult = _compileService.Compile(CorrectJavaSourceCode, JavaLanguageType, input, output, Timelimit, memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);
        }

        #endregion

        #region Delphi tests

        /// <summary>
        ///(Delphi) A test with correct memorylimit value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectDelphiMemorylimitCompileTest()
        {
            string[] input = { "" };
            string[] output = { "Hello, world!" };
            const int memorylimit = 1000;
            string actualResult = _compileService.Compile(CorrectDelphiSourceCode, DelphiLanguageType, input, output, Timelimit, memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///(Delphi) A test with memorylimit less that needs/0/-1 value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectDelphiMemorylimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            int memorylimit = 1;
            string actualResult = _compileService.Compile(CorrectDelphiSourceCode, DelphiLanguageType, input, output, Timelimit, memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            memorylimit = 0;
            actualResult = _compileService.Compile(CorrectJavaSourceCode, JavaLanguageType, input, output, Timelimit, memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);

            memorylimit = -1;
            actualResult = _compileService.Compile(CorrectJavaSourceCode, JavaLanguageType, input, output, Timelimit, memorylimit);
            Assert.AreNotEqual(AcceptedTestResult, actualResult);
        }

        #endregion

        #endregion

        #region InputOutputValue tests

        #region CS tests

        /// <summary>
        ///(CS) A test with correct input/output value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectCSInputOutputValueCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };
            string actualResult = _compileService.Compile(CorrectCSSourceCode, CSLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///(CS) A test with incorrect input/output value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectCSInputOutputValueCompileTest()
        {
            //first parameter fail
            string[] input = { "2 5", "7 5" };
            string[] output = { "35", "75" };
            string actualResult = _compileService.Compile(CorrectCSSourceCode, CSLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(WrongAnswerNullResult, actualResult);

            //secont parameter fail
            string[] newOutput = { "25", "95" };
            actualResult = _compileService.Compile(CorrectCSSourceCode, CSLanguageType, input, newOutput, Timelimit, Memorylimit);
            Assert.AreEqual(WrongAnswerOneResult, actualResult);
        }

        #endregion

        #region CPP tests

        /// <summary>
        ///(CPP) A test with correct input/output value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectCPPinputOutputValueCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };
            string actualResult = _compileService.Compile(CorrectCPPsourceCode, CPPlanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///(CPP) A test with incorrect input/output value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectCPPinputOutputValueCompileTest()
        {
            //first parameter fail
            string[] input = { "2 5", "7 5" };
            string[] output = { "35", "75" };
            string actualResult = _compileService.Compile(CorrectCPPsourceCode, CPPlanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(WrongAnswerNullResult, actualResult);

            //secont parameter fail
            string[] newOutput = { "25", "95" };
            actualResult = _compileService.Compile(CorrectCPPsourceCode, CPPlanguageType, input, newOutput, Timelimit, Memorylimit);
            Assert.AreEqual(WrongAnswerOneResult, actualResult);
        }

        #endregion

        #region Java tests

        /// <summary>
        ///(Java) A test with correct input/output value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectJavaInputOutputValueCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };
            string actualResult = _compileService.Compile(CorrectJavaSourceCode, JavaLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///(Java) A test with incorrect input/output value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectJavaInputOutputValueCompileTest()
        {
            //first parameter fail
            string[] input = { "2 5", "7 5" };
            string[] output = { "35", "75" };
            string actualResult = _compileService.Compile(CorrectJavaSourceCode, JavaLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(WrongAnswerNullResult, actualResult);

            //secont parameter fail
            string[] newOutput = { "25", "95" };
            actualResult = _compileService.Compile(CorrectJavaSourceCode, JavaLanguageType, input, newOutput, Timelimit, Memorylimit);
            Assert.AreEqual(WrongAnswerOneResult, actualResult);
        }

        #endregion

        #region Delphi tests

        //TODO: right code
        /// <summary>
        ///(Delphi) A test with correct input/output value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectDelphiInputOutputValueCompileTest()
        {
            string[] input = { "" };
            string[] output = { "Hello, world!" };
            string actualResult = _compileService.Compile(CorrectDelphiSourceCode, DelphiLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        ///(Delphi) A test with incorrect input/output value
        ///</summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectDelphiInputOutputValueCompileTest()
        {
            string[] input = { "" };
            string[] output = { "Hello" };
            string actualResult = _compileService.Compile(CorrectDelphiSourceCode, DelphiLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(WrongAnswerNullResult, actualResult);
        }

        #endregion

        #endregion
    }
}
