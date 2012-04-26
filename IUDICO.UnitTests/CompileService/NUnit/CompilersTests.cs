using System;
using System.IO;
using CompileSystem.Classes.Compiling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace IUDICO.UnitTests.CompileService.NUnit
{
    [TestFixture]
    public class CompilersTests
    {
        private readonly Compilers compilers = new Compilers("Directory");

        [Test]
        public void CompilerConstructorTest()
        {
            var target = new Compilers("Directory");
            Assert.AreNotEqual(target.GetCompilers(), null);
        }

        [Test]
        public void CompilersClearTest()
        {
            Assert.AreEqual(this.compilers.Count, 0);
            this.compilers.AddCompiler(new Compiler());
            Assert.AreEqual(this.compilers.Count, 1);
            this.compilers.Clear();
            Assert.AreEqual(this.compilers.Count, 0);
        }

        [Test]
        public void CompilersAddCompilerTest()
        {
            Assert.AreEqual(this.compilers.Count, 0);
            this.compilers.AddCompiler(new Compiler());
            Assert.AreEqual(this.compilers.Count, 1);
            this.compilers.Clear();
        }

        [Test]
        public void CompilersContainsTest()
        {
            Assert.AreEqual(this.compilers.Count, 0);
            var newCompiler = new Compiler {Name = "CPP"};
            this.compilers.AddCompiler(newCompiler);

            bool result = this.compilers.Contains("CPP");
            Assert.AreEqual(true, result);

            result = this.compilers.Contains("BadCompilerName");
            Assert.AreEqual(result, false);

            this.compilers.Clear();
        }

        [Test]
        public void CompilersGetCompilerTest()
        {
            Assert.AreEqual(this.compilers.Count, 0);
            var result = this.compilers.GetCompiler("CPP");
            Assert.AreEqual(null, result);

            var newCompiler = new Compiler {Name = "CPP"};
            this.compilers.AddCompiler(newCompiler);

            result = this.compilers.GetCompiler("CPP");
            
            Assert.AreNotEqual(result, null);
            Assert.AreEqual("CPP", result.Name);

            result = this.compilers.GetCompiler("BadName");
            Assert.AreEqual(result, null);

            this.compilers.Clear();
        }

        [Test]
        public void GetCompilersTest()
        {
            var newCompiler = new Compiler {Name = "CPP"};
            var resultList = this.compilers.GetCompilers();

            Assert.AreEqual(resultList.Count, 0);

            this.compilers.AddCompiler(newCompiler);
            resultList = this.compilers.GetCompilers();

            Assert.AreEqual(resultList.Count, 1);

            this.compilers.Clear();
        }

        [Test]
        public void LoadTest()
        {
            var compilers = new Compilers("EmptyCompiler");
            compilers.Load();
            Assert.AreEqual(compilers.Count, 0);
            
            compilers = new Compilers("TestCompilers");
            compilers.Load();
            Assert.AreEqual(compilers.Count, 2);
        }

        [Test]
        public void ParseTest()
        {
            var correctXmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TestCompilers\CPP8.xml");

            //TODO: check it
            var compilers = new Compilers("Compilers");
            var privateObject = new PrivateObject(compilers, new PrivateType(typeof (Compilers)));

            Assert.AreNotEqual(null, privateObject.Invoke("Parse", correctXmlFilePath));
        }

        [Test]
        [global::NUnit.Framework.ExpectedException(typeof(Exception))]
        public void CompilersParseBadXmlTest()
        {
            var incorrectXmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                       @"TestCompilers\CSharp.xml");

            var tempCompilers = new Compilers("Compilers");
            var privateObject = new PrivateObject(tempCompilers, new PrivateType(typeof(Compilers)));

            privateObject.Invoke("Parse", incorrectXmlFilePath);
        }

        [Test]
        [global::NUnit.Framework.ExpectedException(typeof(Exception))]
        public void CompilersLoadTwoXmlTest()
        {
            var compilers = new Compilers("BadCompilers");
            compilers.Load();
        }

        [Test]
        [global::NUnit.Framework.ExpectedException(typeof(Exception))]
        public void CompilersAddNullCompilerTest()
        {
            this.compilers.AddCompiler(null);
        }

        [Test]
        [global::NUnit.Framework.ExpectedException(typeof(Exception))]
        public void CompilersConstructorTest()
        {
            var compilers = new Compilers(null);
        }
    }
}
