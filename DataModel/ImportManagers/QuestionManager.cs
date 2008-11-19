using System.Collections.Generic;
using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
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

                    if(c is WebCompiledTest)
                    {
                        StoreCompiledQuestion((c as WebTestControl).Id, pageRef, c.Name,
                                                                 CompiledQuestionManager.Import(questionAnswerNode), GetRank(questionAnswerNode));
                    }
                    else
                    {
                        StoreQuestion((c as WebTestControl).Id, pageRef, c.Name,
                                                         GetAnswer(questionAnswerNode), GetRank(questionAnswerNode));
                    }
                }
        }

        private static void StoreCompiledQuestion(int id, int pageRef, string name, int compiledQuestionRef, int rank)
        {
            var q = ServerModel.DB.Load<TblQuestions>(id);
            q.PageRef = pageRef;
            q.TestName = name;
            q.CompiledQuestionRef = compiledQuestionRef;
            q.Rank = rank;

            ServerModel.DB.Update(q);
        }

        private static void StoreQuestion(int id, int pageRef, string name, string answer, int rank)
        {
            var q = ServerModel.DB.Load<TblQuestions>(id);
            q.PageRef = pageRef;
            q.TestName = name;
            q.CorrectAnswer = answer;
            q.Rank = rank;

            ServerModel.DB.Update(q);
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
    }
}