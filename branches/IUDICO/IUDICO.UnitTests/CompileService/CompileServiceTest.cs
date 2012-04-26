using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace IUDICO.UnitTests.CompileService
{
    using CompileSystem;

    /// <summary>
    /// This is a test class for CompileServiceTest and is intended
    /// to contain all CompileServiceTest Unit Tests
    /// </summary>
    [TestClass]
    public class CompileServiceTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        // ----------------------------------------------
        private static readonly CompileService CompileService = new CompileService();

        // ----------------------------------------------
        private string CorrectCSSourceCode = CompileServiceLanguageSourceCode.CSCorrectSourceCode;

        private string CorrectCPPsourceCode = CompileServiceLanguageSourceCode.CPPCorrectSourceCode;

        private string CorrectJavaSourceCode = CompileServiceLanguageSourceCode.JavaCorrectSourceCode;

        private string CorrectDelphiSourceCode = CompileServiceLanguageSourceCode.DelphiCorrectSourceCode;

        private string IncorrectCSSourceCode = CompileServiceLanguageSourceCode.CSIncorrectSourceCode;

        private string IncorrectCPPsourceCode = CompileServiceLanguageSourceCode.CPPIncorrectSourceCode;

        private string IncorrectJavaSourceCode = CompileServiceLanguageSourceCode.JavaIncorrectSourceCode;

        private string IncorrectDelphiSourceCode = CompileServiceLanguageSourceCode.DelphiIncorrectSourceCode;

        private string TimeLimitCPPSourceCode = CompileServiceLanguageSourceCode.CPPTimelimitCorrectSourceCode;

        private string TimeLimitCSSourceCode = CompileServiceLanguageSourceCode.CSTimelimitCorrectSourceCode;

        private string TimeLimitJavaSourceCode = CompileServiceLanguageSourceCode.JavaTimelimitCorrectSourceCode;

        private string TimeLimitDelphiSourceCode = CompileServiceLanguageSourceCode.DelphiTimelimitCorrectSourceCode;

        private string MemoryLimitCSSourceCode = CompileServiceLanguageSourceCode.CSMemorylimitCorrectSourceCode;

        private string MemoryLimitCPPSourceCode = CompileServiceLanguageSourceCode.CPPMemorylimitCorrectSourceCode;

        private string MemoryLimitJavaSourceCode = CompileServiceLanguageSourceCode.JavaMemorylimitCorrectSourceCode;

        private string MemoryLimitDelphiSourceCode = CompileServiceLanguageSourceCode.DelphiMemorylimitCorrectSourceCode;

        // ----------------------------------------------
        private const string CSLanguageType = "CSharp";

        private const string CPPlanguageType = "CPP8";

        private const string JavaLanguageType = "Java";

        private const string DelphiLanguageType = "Delphi";

        // ----------------------------------------------
        private readonly string[] emptyInput = new string[0];

        private readonly string[] emptyOutput = new string[0];

        // ----------------------------------------------
        private const int Timelimit = 2000;

        private const int Memorylimit = 3000;

        // ----------------------------------------------
        private const string AcceptedTestResult = "Accepted";

        private const string CompilationErrorResult = "CompilationError";

        private const string WrongAnswerNullResult = "WrongAnswer Test: 0";

        private const string WrongAnswerOneResult = "WrongAnswer Test: 1";

        private const string TimeLimitResult = "TimeLimit";

        private const string MemoryLimitResult = "MemoryLimit";

        // Integration tests
        #region Source tests

        #region CS tests

        /// <summary>
        /// (CS) A test for CS Compile with correct code
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectSourceCSCompileTest()
        {
            var actualResult = CompileService.Compile(
                this.CorrectCSSourceCode, CSLanguageType, this.emptyInput, this.emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// (CS) A test for CS Compile with incorrect code
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectSourceCSCompileTest()
        {
            var actualResult = CompileService.Compile(
                this.IncorrectCSSourceCode, CSLanguageType, this.emptyInput, this.emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(CompilationErrorResult, actualResult);
        }

        #endregion

        #region CPP tests

        /// <summary>
        /// (CPP) A test for C++ Compile with correct code
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectSourceCPLusPlusCompileTest()
        {
            var actualResult = CompileService.Compile(
                this.CorrectCPPsourceCode, CPPlanguageType, this.emptyInput, this.emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// (CPP) A test for C++ Compile with incorrect code
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectSourceCPLusPlusCompileTest()
        {
            var actualResult = CompileService.Compile(
                this.IncorrectCPPsourceCode, CPPlanguageType, this.emptyInput, this.emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(CompilationErrorResult, actualResult);
        }

        #endregion

        #region Java tests

        /// <summary>
        /// (Java) A test for Java Compile with correct code
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectSourceJavaCompileTest()
        {
            var actualResult = CompileService.Compile(
                this.CorrectJavaSourceCode, JavaLanguageType, this.emptyInput, this.emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// (Java) A test for Java Compile with incorrect code
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectSourceJavaCompileTest()
        {
            var actualResult = CompileService.Compile(
                this.IncorrectJavaSourceCode, 
                JavaLanguageType, 
                this.emptyInput, 
                this.emptyOutput, 
                Timelimit, 
                Memorylimit);
            Assert.AreEqual(CompilationErrorResult, actualResult);
        }

        #endregion

        #region Delphi tests

        /// <summary>
        /// (Delphi) A test for Delphi Compile with correct code
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectSourceDelphiCompileTest()
        {
            var actualResult = CompileService.Compile(
                this.CorrectDelphiSourceCode, 
                DelphiLanguageType, 
                this.emptyInput, 
                this.emptyOutput, 
                Timelimit, 
                Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// (Delphi) A test for Delphi Compile with incorrect code
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectSourceDelphiCompileTest()
        {
            var actualResult = CompileService.Compile(
                this.IncorrectDelphiSourceCode, 
                DelphiLanguageType, 
                this.emptyInput, 
                this.emptyOutput, 
                Timelimit, 
                Memorylimit);
            Assert.AreEqual(CompilationErrorResult, actualResult);
        }

        #endregion

        #endregion

        #region Language tests

        /// <summary>
        /// A test with correct language attribute
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectLanguageCompileTest()
        {
            var actualResult = CompileService.Compile(
                this.CorrectCPPsourceCode, CPPlanguageType, this.emptyInput, this.emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// A test with incorrect language(CS = real; CPP = attribute 1 , Java = attribute 2) attribute
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectLanguageCompileTest()
        {
            // CS and CPP
            var actualResult = CompileService.Compile(
                this.CorrectCSSourceCode, CPPlanguageType, this.emptyInput, this.emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(CompilationErrorResult, actualResult);

            // CS and Java
            actualResult = CompileService.Compile(
                this.CorrectCSSourceCode, JavaLanguageType, this.emptyInput, this.emptyOutput, Timelimit, Memorylimit);
            Assert.AreEqual(CompilationErrorResult, actualResult);
        }

        /// <summary>
        /// A test with correct source code and null/unknown/known(lower-case) language attribute
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectLanguageCompileTest2()
        {
            string languageType = null;
            string actualResult;

            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectCSSourceCode, languageType, this.emptyInput, this.emptyOutput, Timelimit, Memorylimit);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            languageType = string.Empty;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectCSSourceCode, languageType, this.emptyInput, this.emptyOutput, Timelimit, Memorylimit);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            languageType = "cs";
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectCSSourceCode, languageType, this.emptyInput, this.emptyOutput, Timelimit, Memorylimit);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        #endregion

        #region Timelimit tests

        #region CS

        /// <summary>
        /// (CS) A test with correct timelimit value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectCSTimelimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            var actualResult = CompileService.Compile(
                this.CorrectCSSourceCode, CSLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// (CS) A test with 0/less than 0/less than it needs timelimit value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectCSTimelimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            var incorrectTimelimit = 0;
            string actualResult;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectCSSourceCode, CSLanguageType, input, output, incorrectTimelimit, Memorylimit);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            incorrectTimelimit = -5;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectCSSourceCode, CSLanguageType, input, output, incorrectTimelimit, Memorylimit);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            incorrectTimelimit = 1;
            actualResult = CompileService.Compile(
                this.TimeLimitCSSourceCode, CPPlanguageType, input, output, incorrectTimelimit, Memorylimit);
            Assert.AreEqual(TimeLimitResult, actualResult);
        }

        #endregion

        #region Java

        /// <summary>
        /// (Java) A test with correct timelimit value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectJavaTimelimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            var actualResult = CompileService.Compile(
                this.CorrectJavaSourceCode, JavaLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// (Java) A test with 0/less than 0/less than it needs timelimit value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectJavaTimelimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            var incorrectTimelimit = 0;
            string actualResult;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectJavaSourceCode, JavaLanguageType, input, output, incorrectTimelimit, Memorylimit);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            incorrectTimelimit = -5;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectJavaSourceCode, JavaLanguageType, input, output, incorrectTimelimit, Memorylimit);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            incorrectTimelimit = 1;
            actualResult = CompileService.Compile(
                this.TimeLimitJavaSourceCode, JavaLanguageType, input, output, incorrectTimelimit, Memorylimit);
            Assert.AreEqual(TimeLimitResult, actualResult);
        }

        #endregion

        #region CPP

        /// <summary>
        /// (CPP) A test with correct timelimit value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectCPPtimelimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };
            var actualResult = CompileService.Compile(
                this.CorrectCPPsourceCode, CPPlanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// (CPP) A test with 0/less than 0/less than it needs timelimit value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectCPPtimelimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            var incorrectTimelimit = 0;
            string actualResult;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectCPPsourceCode, CPPlanguageType, input, output, incorrectTimelimit, Memorylimit);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            incorrectTimelimit = -5;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectCPPsourceCode, CPPlanguageType, input, output, incorrectTimelimit, Memorylimit);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            incorrectTimelimit = 1;
            actualResult = CompileService.Compile(
                this.TimeLimitCPPSourceCode, CPPlanguageType, input, output, incorrectTimelimit, Memorylimit);
            Assert.AreEqual("TimeLimit Test: 0", actualResult);
        }

        #endregion

        #region Delphi

        /// <summary>
        /// (Delphi) A test with correct timelimit value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectDeplhiTimelimitCompileTest()
        {
            string[] input = { string.Empty };
            string[] output = { "Hello, world!" };

            var actualResult = CompileService.Compile(
                this.CorrectDelphiSourceCode, DelphiLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// (Delphi) A test with 0/less than 0/less than it needs timelimit value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectDelphiTimelimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            var incorrectTimelimit = 0;
            string actualResult;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectDelphiSourceCode, DelphiLanguageType, input, output, incorrectTimelimit, Memorylimit);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            incorrectTimelimit = -5;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectDelphiSourceCode, DelphiLanguageType, input, output, incorrectTimelimit, Memorylimit);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            incorrectTimelimit = 1;
            actualResult = CompileService.Compile(
                this.TimeLimitDelphiSourceCode, DelphiLanguageType, input, output, incorrectTimelimit, Memorylimit);
            Assert.AreEqual(TimeLimitResult, actualResult);
        }

        #endregion

        #endregion

        #region Memorylimit tests

        #region CS tests

        /// <summary>
        /// (CS) A test with correct memorylimit value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectCSMemorylimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            var actualResult = CompileService.Compile(
                this.CorrectCSSourceCode, CSLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// (CS) A test with memorylimit less that needs/0/-1 value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectCSMemorylimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            var incorrectMemorylimit = 0;
            string actualResult;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectCSSourceCode, CSLanguageType, input, output, Timelimit, incorrectMemorylimit);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            incorrectMemorylimit = -5;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectCSSourceCode, CSLanguageType, input, output, Timelimit, incorrectMemorylimit);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            incorrectMemorylimit = 1;
            actualResult = CompileService.Compile(
                this.MemoryLimitCSSourceCode, CSLanguageType, input, output, Timelimit, incorrectMemorylimit);
            Assert.AreEqual(MemoryLimitResult, actualResult);
        }

        #endregion

        #region CPP tests

        /// <summary>
        /// (CPP) A test with correct memorylimit value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectCPPmemorylimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            var actualResult = CompileService.Compile(
                this.CorrectCPPsourceCode, CPPlanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// (CPP) A test with memorylimit less that needs/0/-1 value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectCPPmemorylimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            var incorrectMemorylimit = 0;
            string actualResult;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectCPPsourceCode, CPPlanguageType, input, output, Timelimit, incorrectMemorylimit);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            incorrectMemorylimit = -5;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectCPPsourceCode, CPPlanguageType, input, output, Timelimit, incorrectMemorylimit);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            incorrectMemorylimit = 1;
            actualResult = CompileService.Compile(
                this.CorrectCPPsourceCode, CPPlanguageType, input, output, Timelimit, incorrectMemorylimit);
            Assert.AreEqual("MemoryLimit Test: 0", actualResult);
        }

        #endregion

        #region Java tests

        /// <summary>
        /// (Java) A test with correct memorylimit value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectJavaMemorylimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            var actualResult = CompileService.Compile(
                this.CorrectJavaSourceCode, JavaLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// (Java) A test with memorylimit less that needs/0/-1 value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectJavaMemorylimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            var incorrectMemorylimit = 0;
            string actualResult;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectJavaSourceCode, JavaLanguageType, input, output, Timelimit, incorrectMemorylimit);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            incorrectMemorylimit = -5;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectJavaSourceCode, JavaLanguageType, input, output, Timelimit, incorrectMemorylimit);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            incorrectMemorylimit = 1;
            actualResult = CompileService.Compile(
                this.MemoryLimitJavaSourceCode, JavaLanguageType, input, output, Timelimit, incorrectMemorylimit);
            Assert.AreEqual(MemoryLimitResult, actualResult);
        }

        #endregion

        #region Delphi tests

        /// <summary>
        /// (Delphi) A test with correct memorylimit value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectDelphiMemorylimitCompileTest()
        {
            string[] input = { string.Empty };
            string[] output = { "Hello, world!" };

            var actualResult = CompileService.Compile(
                this.CorrectDelphiSourceCode, DelphiLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// (Delphi) A test with memorylimit less that needs/0/-1 value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectDelphiMemorylimitCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };

            var incorrectMemorylimit = 0;
            string actualResult;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectDelphiSourceCode, DelphiLanguageType, input, output, Timelimit, incorrectMemorylimit);
                Assert.AreEqual(false, true);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            incorrectMemorylimit = -5;
            try
            {
                actualResult = CompileService.Compile(
                    this.CorrectDelphiSourceCode, DelphiLanguageType, input, output, Timelimit, incorrectMemorylimit);
                Assert.AreEqual(true, false);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }

            incorrectMemorylimit = 1;
            actualResult = CompileService.Compile(
                this.MemoryLimitDelphiSourceCode, DelphiLanguageType, input, output, Timelimit, incorrectMemorylimit);
            Assert.AreEqual(MemoryLimitResult, actualResult);
        }

        #endregion

        #endregion

        #region InputOutputValue tests

        #region CS tests

        /// <summary>
        /// (CS) A test with correct input/output value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectCSInputOutputValueCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };
            var actualResult = CompileService.Compile(
                this.CorrectCSSourceCode, CSLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// (CS) A test with incorrect input/output value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectCSInputOutputValueCompileTest()
        {
            // first parameter fail
            string[] input = { "2 5", "7 5" };
            string[] output = { "35", "75" };
            var actualResult = CompileService.Compile(
                this.CorrectCSSourceCode, CSLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(WrongAnswerNullResult, actualResult);

            // secont parameter fail
            string[] newOutput = { "25", "95" };
            actualResult = CompileService.Compile(
                this.CorrectCSSourceCode, CSLanguageType, input, newOutput, Timelimit, Memorylimit);
            Assert.AreEqual(WrongAnswerOneResult, actualResult);
        }

        #endregion

        #region CPP tests

        /// <summary>
        /// (CPP) A test with correct input/output value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectCPPinputOutputValueCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };
            var actualResult = CompileService.Compile(
                this.CorrectCPPsourceCode, CPPlanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// (CPP) A test with incorrect input/output value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectCPPinputOutputValueCompileTest()
        {
            // first parameter fail
            string[] input = { "2 5", "7 5" };
            string[] output = { "35", "75" };
            var actualResult = CompileService.Compile(
                this.CorrectCPPsourceCode, CPPlanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(WrongAnswerNullResult, actualResult);

            // secont parameter fail
            string[] newOutput = { "25", "95" };
            actualResult = CompileService.Compile(
                this.CorrectCPPsourceCode, CPPlanguageType, input, newOutput, Timelimit, Memorylimit);
            Assert.AreEqual(WrongAnswerOneResult, actualResult);
        }

        #endregion

        #region Java tests

        /// <summary>
        /// (Java) A test with correct input/output value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectJavaInputOutputValueCompileTest()
        {
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };
            var actualResult = CompileService.Compile(
                this.CorrectJavaSourceCode, JavaLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// (Java) A test with incorrect input/output value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectJavaInputOutputValueCompileTest()
        {
            // first parameter fail
            string[] input = { "2 5", "7 5" };
            string[] output = { "35", "75" };
            var actualResult = CompileService.Compile(
                this.CorrectJavaSourceCode, JavaLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(WrongAnswerNullResult, actualResult);

            // secont parameter fail
            string[] newOutput = { "25", "95" };
            actualResult = CompileService.Compile(
                this.CorrectJavaSourceCode, JavaLanguageType, input, newOutput, Timelimit, Memorylimit);
            Assert.AreEqual(WrongAnswerOneResult, actualResult);
        }

        #endregion

        #region Delphi tests

        // TODO: right code
        /// <summary>
        /// (Delphi) A test with correct input/output value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void CorrectDelphiInputOutputValueCompileTest()
        {
            string[] input = { string.Empty };
            string[] output = { "Hello, world!" };
            var actualResult = CompileService.Compile(
                this.CorrectDelphiSourceCode, DelphiLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(AcceptedTestResult, actualResult);
        }

        /// <summary>
        /// (Delphi) A test with incorrect input/output value
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void IncorrectDelphiInputOutputValueCompileTest()
        {
            string[] input = { string.Empty };
            string[] output = { "Hello" };
            var actualResult = CompileService.Compile(
                this.CorrectDelphiSourceCode, DelphiLanguageType, input, output, Timelimit, Memorylimit);
            Assert.AreEqual(WrongAnswerNullResult, actualResult);
        }

        #endregion

        #endregion

        /// <summary>
        /// Load test for CompileService
        /// </summary>
        [TestMethod]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\IUDICO\\IUDICO.CompileSystem", "/")]
        [UrlToTest("http://localhost:1345/")]
        public void LoadCompileTest()
        {
            var testsCount = 100;
            var resultList = new List<string>();
            var startDate = DateTime.Now;
            string[] input = { "2 5", "7 5" };
            string[] output = { "25", "75" };
            for (var i = 0; i < testsCount; i++)
            {
                var actualResult = CompileService.Compile(
                    this.CorrectCPPsourceCode, CPPlanguageType, input, output, Timelimit, Memorylimit);
                resultList.Add(actualResult);
            }

            var endDate = DateTime.Now;
            var loadTime = endDate - startDate;

            for (var i = 0; i < testsCount; i++)
            {
                Assert.AreEqual(resultList[i], AcceptedTestResult);
            }
        }
    }
}