using System;
using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Dao;
using IUDICO.DataModel.Dao.Entity;

namespace IUDICO.DataModel.ImportManagers
{
    class CompiledQuestionManager
    {
        public static int Import(XmlNode node)
        {
            var cqe = new CompiledQuestionEntity(GetLanguage(node), XmlUtility.getTimeLimit(node), XmlUtility.getMemoryLimit(node), XmlUtility.getOutputLimit(node));
            DaoFactory.CompiledQuestionDao.Insert(cqe);
            SetInputOutput(node, cqe.Id);
            
            return cqe.Id;
        }

        private static void SetInputOutput(XmlNode node, int id)
        {
            foreach (XmlNode n in node.ChildNodes)
                if (XmlUtility.isTestCase(n))
                {
                    var cqde = new CompiledQuestionDataEntity(id, n.ChildNodes[1].InnerText, n.ChildNodes[0].InnerText);
                    DaoFactory.CompiledQuestionDataDao.Insert(cqde);
                }
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