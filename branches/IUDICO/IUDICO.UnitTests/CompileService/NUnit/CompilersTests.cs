using System;
using System.IO;

using CompileSystem.Classes.Compiling;
using NUnit.Framework;

using Assert = NUnit.Framework.Assert;

namespace IUDICO.UnitTests.CompileService.NUnit
{
  using CompileSystem;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using CompileSystem.Classes.Compiling;

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
            var newCompiler = new Compiler { Name = "CPP" };
            this.compilers.AddCompiler(newCompiler);

            var result = this.compilers.Contains("CPP");
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

            var newCompiler = new Compiler { Name = "CPP" };
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
            var newCompiler = new Compiler { Name = "CPP" };
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

            compilers = new Compilers("Compilers");
            compilers.Load();
            Assert.AreEqual(compilers.Count, 4);
        }

        [Test]
        public void ParseTest()
        {
            var correctXmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Compilers\CPP8\CPP8.xml");

            // TODO: check it
            var compilersArr = new Compilers("Compilers");
            var privateObject = new PrivateObject(compilersArr, new PrivateType(typeof(Compilers)));

            Assert.AreNotEqual(null, privateObject.Invoke("Parse", correctXmlFilePath));
        }

        [Test]
        public void CompilersParseBadXmlTest()
        {
            var incorrectXmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Compilers\CSharp\CSharp.xml");
            try
            {
              var tempCompilers = new Compilers("Compilers");
              var privateObject = new PrivateObject(tempCompilers, new PrivateType(typeof(Compilers)));

              privateObject.Invoke("Parse", incorrectXmlFilePath);
            }
            catch (Exception)
            {
              Assert.AreEqual(true, true);
            }
        }
        /// <summary>
        /// author - Alexander Tymchenko - 
        /// </summary>
        [Test]
        public void AddDuplicateCompilerTest()
        {
          //creating Compilers from compilers folder
          var firstCompilers = new Compilers("Compilers");
          //initialization
          firstCompilers.Load();
          //trying to add compiler of CPP8 which actually is in Compilers right now !
          firstCompilers.AddCompiler(firstCompilers.GetCompiler("CPP8"));
          //compilers must be unique. same compiler can not be added, thats why test fails yet.
          Assert.AreEqual(firstCompilers.Count,4);
        }
      /// <summary>
      /// author - Alexander Tymchenko
      /// </summary>
        [Test]
        [ExpectedException(typeof(Exception))]
        public void AddInvalidCompilerTest()
        {
            //create compilers with bad path
            var compilers = new Compilers("NoFolderCompilers");
            //try to initialize them
            compilers.Load();
           //there is no Assert, because in this test we are expecting an exception.
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
            var compilersArr = new Compilers(null);
        }
    }
}