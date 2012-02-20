using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CompileSystem.Compiling.Compile;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IUDICO.UnitTests.CompileService
{
    class CompileServiceHelper
    {
        public static bool ValidatePath(string path)
        {
            if (path != "")
            {
                bool isExist = File.Exists(path);
                if (isExist)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public static void ValidateCorrectCompilationResult(CompileResult result)
        {
            Assert.AreEqual(result.Compiled,true);
        }

        public static void ValidateIncorrectCompilationResult(CompileResult result)
        {
            Assert.AreEqual(result.Compiled, false);
            Assert.AreNotEqual(result.StandartOutput, "");
        }
    }
}
