using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.ImportManagers;

namespace IUDICO.DataModel.WebControl
{
    public class WebPage
    {
        private readonly Dictionary<string, int> answersIndexes = new Dictionary<string, int>();
        private readonly List<WebControl> controls = new List<WebControl>();
        private byte[] byteRepresentation;

        public WebPage(string pathToPage)
        {
            var doc = new XmlDocument();
            doc.LoadXml(File.ReadAllText(pathToPage, StudentHelper.GetEncoding()));
            if (doc.DocumentElement != null)
            {
                SetAnswerIndexes(doc.DocumentElement);
                Parse(doc.DocumentElement);
            }
        }

        public byte[] ByteRepresentation
        {
            get { return byteRepresentation; }
        }

        public List<WebControl> Controls
        {
            get { return controls; }
        }

        private static WebControl GetControlForParse(XmlNode node)
        {
            switch (node.Name)
            {
                case "input":
                    switch (node.Attributes["type"].Value)
                    {
                        case "text":
                            return new WebTextBox();

                        case "button":
                            return new WebButton();
                    }
                    break;

                case "select":
                    return new WebComboBox();

                case "textarea":
                    XmlAttribute id = node.Attributes["id"];
                    if (id == null || id.Value != "traceLog")
                    {
                        return new WebCompiledTest();
                    }
                    break;

                case "span":
                    XmlAttribute attribute = node.Attributes["name"];
                    return attribute != null && attribute.Value == "code"
                               ? (WebControl) new WebHighlightedCode()
                               : new WebLabel();

                case "div":
                    XmlAttribute at = node.Attributes["name"];
                    if (at != null)
                    {
                        string name = at.Value;
                        if (name == "snippet")
                        {
                            return new WebCodeSnippet();
                        }
                        if (name.StartsWith("gen:"))
                        {
                            return new WebSimpleQuestion();
                        }
                    }
                    break;
            }

            return null;
        }

        private void Parse(XmlNode node)
        {
            WebControl c = GetControlForParse(node);
            if (c != null)
            {
                c.Parse(node);
                if (c is WebTestControl)
                {
                    (c as WebTestControl).AnswerIndex = answersIndexes[c.Name];
                }

                Controls.Add(c);
            }
            else if (node.HasChildNodes)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    Parse(childNode);
                }
            }
        }

        public void TransformToAspx(string pageName, int pageRef, XmlNode answerNode, string pathToTempCourseFolder)
        {
            var sw = new StringWriter(); 

            ConstructPageCode(new HtmlTextWriter(sw), pageName, pageRef);

            sw.Close();

            byteRepresentation = StudentHelper.GetEncoding().GetBytes(sw.GetStringBuilder().ToString());

            QuestionManager.Import(pageRef, answerNode, controls, pathToTempCourseFolder);
        }

        private void ConstructPageCode(HtmlTextWriter w, string pageName,int pageId)
        {
            AddScriptHeader(w);
            WriteHtml(w, pageName, pageId);
        }

        private void WriteHtml(HtmlTextWriter w, string pageName, int pageId)
        {
            w.RenderBeginTag(HtmlTextWriterTag.Html);

            DisableTextSelection(w);

            WriteHead(w, pageName);

            WriteBody(w, pageId);

            w.RenderEndTag();
        }

        private void WriteBody(HtmlTextWriter w, int pageId)
        {
            w.AddAttribute("oncontextmenu", "return false"); //Disable context menu on page
            w.RenderBeginTag(HtmlTextWriterTag.Body);
            WriteForm(w, pageId);
            w.RenderEndTag();
        }

        private void WriteForm(HtmlTextWriter w, int pageId)
        {
            w.AddAttribute("runat", "server");
            w.AddAttribute("OnLoad", "OnFormLoad");
            w.RenderBeginTag(HtmlTextWriterTag.Form);

            StoreControls(w);

            WriteScript(w, pageId);

            w.RenderEndTag();
        }

        private void StoreControls(HtmlTextWriter w)
        {
            foreach (WebControl c in Controls)
                c.Store(w);
        }

        private void WriteScript(HtmlTextWriter w, int pageId)
        {
            w.AddAttribute("runat", "server");
            w.RenderBeginTag(HtmlTextWriterTag.Script);
            w.Write(CreateCodeFile(pageId));
            w.RenderEndTag();
        }

        private static void WriteHead(HtmlTextWriter w, string pageName)
        {
            w.RenderBeginTag(HtmlTextWriterTag.Head);
            WriteTitle(w, pageName);
            w.RenderEndTag();
        }

        private static void WriteTitle(HtmlTextWriter w, string pageName)
        {
            w.RenderBeginTag(HtmlTextWriterTag.Title);

            w.WriteEncodedText(pageName);

            w.RenderEndTag();
        }

        private void SetAnswerIndexes(XmlNode node)
        {
            if (node.Name == "script")
            {
                if (!string.IsNullOrEmpty(node.InnerText))
                {
                    ExtractAnswersIndexes(node.InnerText);
                }
            }
            else
            {
                foreach (XmlNode n in node.ChildNodes)
                    SetAnswerIndexes(n);
            }
        }

        private void ExtractAnswersIndexes(string innerText)
        {
            var rx = new Regex(@"'\w+'");

            MatchCollection matches = rx.Matches(innerText);

            for (int i = 0; i < matches.Count; i++)
            {
                try
                {
                    answersIndexes.Add(matches[i].Value.Trim(Convert.ToChar("'")), (i));
                }
                catch (Exception)
                {
                    //TODO: write proper regex for extract empty text from textbox control !!!
                }
            }
        }

        private static void AddScriptHeader(TextWriter w)
        {
            if (w == null) throw new ArgumentNullException("w");
            w.Write("<%@ Page Language=\"C#\" ValidateRequest=\"false\"%>");
        }

        private string CreateCodeFile(int pageId)
        {
            var s = new StringBuilder();
            AddAnswerFiller(s, pageId);
            CreateOnFormLoadEvent(s);
            CreateOnClickEvent(s);
            
            return s.ToString();
        }

        private static void DisableTextSelection(HtmlTextWriter w)
        {
            w.RenderBeginTag(HtmlTextWriterTag.Script);
            w.WriteLine("document.onselectstart=new Function('return false');");
            w.WriteLine("document.onmousedown=function(){return false;};");
            w.WriteLine("document.onclick=function(){return true;};");
            w.RenderEndTag();
        }

        private void CreateOnClickEvent(StringBuilder s)
        {
            s.AppendLine("void onClick(object sender, EventArgs e)");
            s.AppendLine("{");
                s.AppendLine("IUDICO.DataModel.WebTest.Tester tester = new IUDICO.DataModel.WebTest.Tester();");
                foreach (WebControl t in Controls)
                {
                    if (t is WebTestControl)
                    {
                        s.AppendFormat("tester.AddTest(new {0});", (t as WebTestControl).CreateCodeForTest());
                        s.AppendLine();
                    }

                }
                s.AppendLine();
                s.AppendLine("tester.Submit();");
                s.AppendLine("tester.NextTestPage(Response, Request);");
            s.AppendLine("}");
        }

        private void CreateOnFormLoadEvent(StringBuilder s)
        {
            s.AppendLine("protected void OnFormLoad(object sender, EventArgs e)");
            s.AppendLine("{");
                    foreach (var t in Controls)
                    {
                        if (t is WebButton)
                        {
                            s.AppendFormat("{0}.Enabled = IUDICO.DataModel.WebControl.TestPageHelper.IsSubmitEnabled(Request);", t.Name);
                            s.AppendLine();
                        }
                    }

                    s.AppendLine("if (IUDICO.DataModel.WebControl.TestPageHelper.IsWriteAnswers(Request))");
                    s.AppendLine("{");
                        s.AppendLine("WriteAnswers();");
                    s.AppendLine("}");
            s.AppendLine("}");
        }

        private void AddAnswerFiller(StringBuilder s, int pageId)
        {
            s.AppendLine("void WriteAnswers()");
            s.AppendLine("{");

            const string answerFillerVaribleName = "answerFiller";

            s.AppendFormat("IUDICO.DataModel.WebControl.AnswerFiller {0} = new IUDICO.DataModel.WebControl.AnswerFiller({1}, Request);", answerFillerVaribleName, pageId);
            s.AppendLine();
            foreach (var control in controls)
            {
                if(control is WebTestControl)
                    s.AppendLine((control as WebTestControl).CreateAnswerFillerCode(answerFillerVaribleName));
            }
            s.AppendLine("}");
        }
    }

    public class TestPageHelper
    {
        public static bool IsWriteAnswers(HttpRequest request)
        {
            return !(request["answers"]).ToLower().Equals("false");
        }

        public static bool IsSubmitEnabled(HttpRequest request)
        {
            return !(request["submit"]).ToLower().Equals("false");
        }
    }

}