using System.Xml;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.ImportManagers
{
    class CompiledQuestionManager
    {
        public static int Import(XmlNode node)
        {
            int id = Store(GetLanguage(node), XmlUtility.GetTimeLimit(node), XmlUtility.GetMemoryLimit(node), XmlUtility.GetOutputLimit(node));
            SetInputOutput(node, id);
            
            return id;
        }

        private static int Store(FxLanguages language, int timeLimit, int memoryLimit, int outputLimit)
        {
            var cq = new TblCompiledQuestions
                         {
                             LanguageRef = language.ID,
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

        private static FxLanguages LanguageName(string lang)
        {
            switch(lang)
            {
                case ("CS"):
                    return FxLanguages.DotNet3;
                case ("Cpp"):
                    return FxLanguages.Vs8CPlusPlus;
                case ("Delphi"):
                    return FxLanguages.Delphi7;
                case ("Java"):
                    return FxLanguages.Java6;
                default:
                    return FxLanguages.DotNet3;
            }
        }

        private static FxLanguages GetLanguage(XmlNode node)
        {
            foreach (XmlNode n in node.ChildNodes)
                if (XmlUtility.IsLanguage(n))
                    return LanguageName(n.InnerText);
            return FxLanguages.DotNet3;
        }
    }
}