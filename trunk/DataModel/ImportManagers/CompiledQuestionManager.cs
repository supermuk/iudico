using System;
using System.Xml;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.ImportManagers
{
    [DBEnum("fxLanguages")]
    public enum FX_LANGUAGE
    {
        Axapta = 1,
        Cpp = 2,
        Delphi = 3,
        HTML = 4,
        Java = 5,
        JavaScript = 6,
        Perl = 7,
        PHP = 8,
        Python = 9,
        RIB = 10,
        RSL = 11,
        Ruby = 12,
        Smalltalk = 13,
        SQL = 14,
        VBScript = 15,
        CS = 16
    }

    class CompiledQuestionManager
    {
        public static int Import(XmlNode node)
        {
            int id = Store(GetLanguage(node), XmlUtility.GetTimeLimit(node), XmlUtility.GetMemoryLimit(node), XmlUtility.GetOutputLimit(node));
            SetInputOutput(node, id);
            
            return id;
        }

        private static int Store(FX_LANGUAGE language, int timeLimit, int memoryLimit, int outputLimit)
        {
            var cq = new TblCompiledQuestions
                         {
                             LanguageRef = ((int) language),
                             MemoryLimit = memoryLimit,
                             TimeLimit = timeLimit,
                             OutputLimit = outputLimit
                         };

            ServerModel.DB.Insert(cq);

            return cq.ID;

        }

        private static void SetInputOutput(XmlNode node, int compiledQuestionRef)
        {
            foreach (XmlNode n in node.ChildNodes)
                if (XmlUtility.IsTestCase(n))
                    if(n.HasChildNodes)
                        StoreData(compiledQuestionRef, n.LastChild.InnerText, n.FirstChild.InnerText);
        }

        private static void StoreData(int compiledQuestionRef, string input, string ouput)
        {
            var cqd = new TblCompiledQuestionsData
            {
                CompiledQuestionRef = compiledQuestionRef,
                Input = input,
                Output = ouput
            };

            ServerModel.DB.Insert(cqd);
        }

        private static FX_LANGUAGE LanguageIndex(string lang)
        {
            foreach (FX_LANGUAGE l in Enum.GetValues(typeof(FX_LANGUAGE)))
            {
                if (l.ToString().ToLower().Equals(lang.ToLower()))
                    return l;
            }

            return 0;
        }

        private static FX_LANGUAGE GetLanguage(XmlNode node)
        {
            foreach (XmlNode n in node.ChildNodes)
                if (XmlUtility.IsLanguage(n))
                    return LanguageIndex(n.InnerText);
            return 0;
        }
    }
}