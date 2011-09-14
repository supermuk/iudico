using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.WebControl;

namespace IUDICO.DataModel.ImportManagers
{
    public class QuestionManager
    {
        public static void Import(int pageRef, XmlNode answerNode, List<WebControlBase> tests, string pathToTempCourseFolder)
        {
            foreach (WebControlBase c in tests)
            {
                if (c is WebTestControlBase)
                    StoreTest(c, answerNode, pageRef);

                if (c is WebCodeSnippet)
                    FilesManager.StoreAllPageFiles(pageRef,
                                                   Path.Combine(pathToTempCourseFolder, c.Name) +
                                                   FileExtentions.WordHtmlFolder);
            }
        }

        private static void StoreTest(WebControlBase c, XmlNode answerNode, int pageRef)
        {
            XmlNode questionAnswerNode = GetTestAnswerNode(answerNode, ((WebTestControlBase) c).AnswerIndex);

            if(c is WebCompiledTest)
            {
                StoreCompiledTestControl(c, pageRef, questionAnswerNode);
            }
            else
            {
                StoreControl(c, pageRef, questionAnswerNode);
            }
        }

        private static void StoreControl(WebControlBase c, int pageRef, XmlNode questionAnswerNode)
        {
            StoreQuestion(((WebTestControlBase) c).Id, pageRef, c.Name,
                          GetAnswer(questionAnswerNode), GetRank(questionAnswerNode));
        }

        private static void StoreCompiledTestControl(WebControlBase c, int pageRef, XmlNode questionAnswerNode)
        {
            StoreCompiledQuestion(((WebTestControlBase) c).Id, pageRef, c.Name,
                                  CompiledQuestionManager.Import(questionAnswerNode), GetRank(questionAnswerNode));
        }

        private static void StoreCompiledQuestion(int id, int pageRef, string name, int compiledQuestionRef, int rank)
        {
            var q = ServerModel.DB.Load<TblQuestions>(id);
            q.PageRef = pageRef;
            q.TestName = name;
            q.CompiledQuestionRef = compiledQuestionRef;
            q.Rank = rank;
            q.IsCompiled = true;

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
                    return Int32.Parse(n.InnerText);
            
            return 0;
        }

        private static string GetAnswer(XmlNode node)
        {
            return XmlUtility.GetAnswer(node);
        }
    }
}