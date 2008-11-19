using System;
using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.ImportManagers
{
    class CompiledQuestionManager
    {
        public static int Import(XmlNode node)
        {
            int id = Store(GetLanguage(node), XmlUtility.getTimeLimit(node), XmlUtility.getMemoryLimit(node), XmlUtility.getOutputLimit(node));
            SetInputOutput(node, id);
            
            return id;
        }

        private static int Store(Language language, int timeLimit, int memoryLimit, int outputLimit)
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
                if (XmlUtility.isTestCase(n))
                {
                    StoreData(compiledQuestionRef, n.ChildNodes[1].InnerText, n.ChildNodes[0].InnerText);
                }
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

        private static Language LanguageIndex(string lang)
        {
            foreach (Language l in Enum.GetValues(typeof(Language)))
            {
                if (l.ToString().Equals(lang))
                    return l;
            }

            return 0;
        }

        private static Language GetLanguage(XmlNode node)
        {
            foreach (XmlNode n in node.ChildNodes)
                if (XmlUtility.isLanguage(n))
                    return LanguageIndex(n.InnerText);
            return 0;
        }
    }
}