using System.Collections.Generic;
using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Dao;
using IUDICO.DataModel.WebControl;

namespace IUDICO.DataModel.ImportManagers
{
    public class QuestionManager
    {
        public static void Import(int pageRef, XmlNode answerNode, List<WebControl.WebControl> tests)
        {
            foreach (WebControl.WebControl c in tests)
                if (c is WebTestControl)
                {
                    XmlNode questionAnswerNode = GetTestAnswerNode(answerNode, (c as WebTestControl).AnswerIndex);

                    QuestionEntity cae;

                    if(c is WebCompiledTest)
                    {
                        cae = QuestionEntity.newCompiledQuestion((c as WebTestControl).Id, pageRef, c.Name,
                                                                 CompiledQuestionManager.Import(questionAnswerNode), GetRank(questionAnswerNode));
                    }
                    else
                    {
                        cae = QuestionEntity.newQuestion((c as WebTestControl).Id, pageRef, c.Name,
                                                         GetAnswer(questionAnswerNode), GetRank(questionAnswerNode));
                    }

                    Store(cae);
                }
        }

        private static XmlNode GetTestAnswerNode(XmlNode node, int index)
        {
            return node.ChildNodes[index];
        }

        private static int GetRank(XmlNode node)
        {
            foreach (XmlNode n in node.ChildNodes)
                if (n.Name == "rank")
                    return int.Parse(n.InnerText);
            return 0;
        }

        private static string GetAnswer(XmlNode node)
        {
            return XmlUtility.getAnswer(node);
        }

        private static void Store(QuestionEntity cae)
        {
            DaoFactory.CorrectAnswerDao.Insert(cae);
        }

    }
}