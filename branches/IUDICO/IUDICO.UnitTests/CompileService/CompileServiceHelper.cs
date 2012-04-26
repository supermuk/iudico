using System.IO;

namespace IUDICO.UnitTests.CompileService
{
    internal class CompileServiceHelper
    {
        public static bool ValidatePath(string path)
        {
            if (path != string.Empty)
            {
                var isExist = File.Exists(path);

                return isExist;
            }

            return false;
        }

        #region Language getters

        public static string CPPLanguageName
        {
            get
            {
                return "CPP8";
            }
        }

        public static string CSharpLanguageName
        {
            get
            {
                return "CSharp";
            }
        }

        public static string JavaLanguageName
        {
            get
            {
                return "Java";
            }
        }

        public static string DelphiLanguageName
        {
            get
            {
                return "Delphi";
            }
        }

        #endregion

        #region Parameters getters

        public static string[] EmptyInput
        {
            get
            {
                return new string[0];
            }
        }

        public static string[] EmptyOutput
        {
            get
            {
                return new string[0];
            }
        }

        public static string[] Input
        {
            get
            {
                return new[] { "2 5", "7 5" };
            }
        }

        public static string[] Output
        {
            get
            {
                return new[] { "25", "75" };
            }
        }

        #endregion

        #region TimeLimit/MemoryLimit getters

        public static int TimeLimit
        {
            get
            {
                return 2000;
            }
        }

        public static int MemoryLimit
        {
            get
            {
                return 3000;
            }
        }

        #endregion

        #region Result getters

        public static string AcceptedTestResult
        {
            get
            {
                return "Accepted";
            }
        }

        public static string CompilationErrorResult
        {
            get
            {
                return "CompilationError";
            }
        }

        public static string TimeLimitOneResult
        {
            get
            {
                return "TimeLimit Test: 0";
            }
        }

        public static string MemoryLimitOneResult
        {
            get
            {
                return "MemoryLimit Test: 0";
            }
        }

        public static string WrongAnswerOneResult
        {
            get
            {
                return "WrongAnswer Test: 0";
            }
        }

        public static string WrongAnswerTwoResult
        {
            get
            {
                return "WrongAnswer Test: 1";
            }
        }

        #endregion
    }
}