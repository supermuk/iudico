using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Xml;
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
            doc.LoadXml(File.ReadAllText(pathToPage, Encoding.GetEncoding(1251)));
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
            IUDICO.DataModel.WebControl.WebControl c = GetControlForParse(node);
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

            ConstructPageCode(new HtmlTextWriter(sw), pageName);

            sw.Close();

            byteRepresentation = Encoding.GetEncoding(1251).GetBytes(sw.GetStringBuilder().ToString());

            QuestionManager.Import(pageRef, answerNode, controls, pathToTempCourseFolder);
        }

        private void ConstructPageCode(HtmlTextWriter w, string pageName)
        {
            AddScriptHeader(w);
            WriteHtml(w, pageName);
        }

        private void WriteHtml(HtmlTextWriter w, string pageName)
        {
            w.RenderBeginTag(HtmlTextWriterTag.Html);
            
            WriteHead(w, pageName);

            WriteBody(w);

            w.RenderEndTag();
        }

        private void WriteBody(HtmlTextWriter w)
        {
            w.RenderBeginTag(HtmlTextWriterTag.Body);
            WriteForm(w);
            w.RenderEndTag();
        }

        private void WriteForm(HtmlTextWriter w)
        {
            w.AddAttribute("runat", "server");
            w.RenderBeginTag(HtmlTextWriterTag.Form);

            StoreControls(w);

            WriteScript(w);

            w.RenderEndTag();
        }

        private void StoreControls(HtmlTextWriter w)
        {
            foreach (IUDICO.DataModel.WebControl.WebControl c in Controls)
                c.Store(w);
        }

        private void WriteScript(HtmlTextWriter w)
        {
            w.AddAttribute("runat", "server");
            w.RenderBeginTag(HtmlTextWriterTag.Script);
            w.WriteEncodedText(CreateCodeFile());
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

        private string CreateCodeFile()
        {
            var s = new StringBuilder();

            s.AppendLine("void onClick(object sender, EventArgs e){" + "\n");
            s.AppendFormat("IUDICO.DataModel.WebTest.Tester tester = new IUDICO.DataModel.WebTest.Tester();" + "\n");
            foreach (IUDICO.DataModel.WebControl.WebControl t in Controls)
            {
                if (t is WebTestControl)
                    s.AppendFormat("tester.AddTest(new {0});" + "\n",
                                   (t as WebTestControl).CreateCodeForTest());
            }
            s.AppendLine("tester.Submit();" + "\n");
            s.AppendLine("}");

            return s.ToString();
        }
    }
}