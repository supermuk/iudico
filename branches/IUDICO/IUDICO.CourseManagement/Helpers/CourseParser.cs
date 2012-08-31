using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CourseManagement.Models.Storage;

namespace IUDICO.CourseManagement.Helpers
{
    public static class CourseParser
    {
        public static IudicoCourseInfo Parse(Course course, ICourseStorage storage)
        {
            int id = course.Id;
            IudicoCourseInfo ci = new IudicoCourseInfo();
            ci.Id = id;

            var nodes = storage.GetNodes(id).ToList();

            for (var i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].IsFolder == false)
                {
                    try
                    {
                        ci.NodesInfo.Add(ParseNode(nodes[i], storage));
                    }
                    catch
                    {
                        
                    }
                }
                else
                {
                    var subNodes = storage.GetNodes(id, nodes[i].Id);
                    nodes.AddRange(subNodes);
                }
            }

            return ci;
        }

        private static IudicoNodeInfo ParseNode(Node node, ICourseStorage storage)
        {
            IudicoNodeInfo ni = new IudicoNodeInfo();
            ni.Id = node.Id;
            ni.CourseId = node.CourseId;

            var path = storage.GetNodePath(node.Id) + ".html";
            TextReader reader = File.OpenText(path);
            XmlDocument xmlDoc = FromHtml(reader);

            // find iudico object nodes <object iudico-question="..." ... >
            foreach (XmlElement el in xmlDoc.GetElementsByTagName("object"))
            {
                bool iudicoQuestion = false;
                IudicoQuestionInfo qi = null;

                // check if it is iudico object
                if (el.Attributes["iudico-question"] != null && el.Attributes["iudico-question"].Value == "true")
                {
                    iudicoQuestion = true;
                    switch (el.Attributes["iudico-type"].Value)
                    {
                        case "iudico-simple":
                            qi = new IudicoSimpleQuestion { Type = IudicoQuestionType.IudicoSimple };
                            break;

                        case "iudico-choice":
                            qi = new IudicoChoiceQuestion { Type = IudicoQuestionType.IudicoChoice };
                            break;

                        case "iudico-compile":
                            qi = new IudicoCompiledTest { Type = IudicoQuestionType.IudicoCompile };
                            break;
                    }
                }

                if (iudicoQuestion == true)
                {
                    // <param name="..." value="..." />
                    foreach (XmlNode xmlNode in el.ChildNodes)
                    {
                        if (xmlNode.Attributes["name"] != null && xmlNode.Attributes["name"].Value == "rank")
                        {
                            double score = 0;
                            double.TryParse(xmlNode.Attributes["value"].Value, out score);
                            qi.MaxScore += score;
                        }
                        if (xmlNode.Attributes["name"] != null && xmlNode.Attributes["name"].Value == "question")
                        {
                            qi.QuestionText = xmlNode.Attributes["value"].Value;
                        }

                        switch (qi.Type)
                        {
                            case IudicoQuestionType.IudicoSimple:
                                if (xmlNode.Attributes["name"] != null && xmlNode.Attributes["name"].Value == "correctAnswer")
                                {
                                    (qi as IudicoSimpleQuestion).CorrectAnswer = xmlNode.Attributes["value"].Value;
                                }
                                break;

                            case IudicoQuestionType.IudicoChoice:
                                if (xmlNode.Attributes["name"] != null && xmlNode.Attributes["name"].Value == "correct")
                                {
                                    (qi as IudicoChoiceQuestion).CorrectChoice = xmlNode.Attributes["value"].Value;
                                }
                                if (xmlNode.Attributes["name"] != null && xmlNode.Attributes["name"].Value.StartsWith("option"))
                                {
                                    (qi as IudicoChoiceQuestion).Options.Add(new Tuple<string, string>(xmlNode.Attributes["name"].Value, xmlNode.Attributes["value"].Value));
                                }
                                break;

                            case IudicoQuestionType.IudicoCompile:
                                if (xmlNode.Attributes["name"] != null && xmlNode.Attributes["name"].Value.StartsWith("testInput"))
                                {
                                    (qi as IudicoCompiledTest).TestInputs.Add(new Tuple<string, string>(xmlNode.Attributes["name"].Value, xmlNode.Attributes["value"].Value));
                                }
                                if (xmlNode.Attributes["name"] != null && xmlNode.Attributes["name"].Value.StartsWith("testOutput"))
                                {
                                    (qi as IudicoCompiledTest).TestOutputs.Add(new Tuple<string, string>(xmlNode.Attributes["name"].Value, xmlNode.Attributes["value"].Value));
                                }
                                break;
                        }
                    }
                    ni.QuestionsInfo.Add(qi);
                }
            }

            return ni;
        }

        private static XmlDocument FromHtml(TextReader reader)
        {
            // setup SGMLReader
            Sgml.SgmlReader sgmlReader = new Sgml.SgmlReader();
            sgmlReader.DocType = "HTML";
            sgmlReader.WhitespaceHandling = WhitespaceHandling.All;
            sgmlReader.CaseFolding = Sgml.CaseFolding.ToLower;
            sgmlReader.InputStream = reader;

            // create document
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.XmlResolver = null;
            doc.Load(sgmlReader);
            return doc;
        }
    }
}