using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Xml;
using IUDICO.DataModel.WebControl;

namespace CourseImport.WebControl
{
    public class WebPage
    {
        private readonly Dictionary<string, int> answersIndexes = new Dictionary<string, int>();
        private readonly List<IUDICO.DataModel.WebControl.WebControl> controls = new List<IUDICO.DataModel.WebControl.WebControl>();

        public WebPage(string pathToPage)
        {
            var doc = new XmlDocument();
            doc.Load(pathToPage);
            if (doc.DocumentElement != null)
            {
                SetAnswerIndexes(doc.DocumentElement);
                Parse(doc.DocumentElement);
            }
        }

        private static IUDICO.DataModel.WebControl.WebControl GetControlForParse(XmlNode node)
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
                               ? (IUDICO.DataModel.WebControl.WebControl) new WebHighlightedCode()
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

                controls.Add(c);
            }
            else if (node.HasChildNodes)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    Parse(childNode);
                }
            }
        }

        public List<IUDICO.DataModel.WebControl.WebControl> SaveAsAsp(string path)
        {
            var sw = new StreamWriter(path, false, Encoding.Default);
            var w = new HtmlTextWriter(sw);
            string pageName = Path.GetFileNameWithoutExtension(path);

            AddScriptHeader(w);



            w.RenderBeginTag(HtmlTextWriterTag.Html);
            w.RenderBeginTag(HtmlTextWriterTag.Head);
            w.RenderBeginTag(HtmlTextWriterTag.Title);

            w.WriteEncodedText(pageName);

            w.RenderEndTag();
            w.RenderEndTag();

            w.RenderBeginTag(HtmlTextWriterTag.Body);
            w.AddAttribute("runat", "server");
            w.RenderBeginTag(HtmlTextWriterTag.Form);

            foreach (IUDICO.DataModel.WebControl.WebControl c in controls)
                c.Store(w);

            w.AddAttribute("runat", "server");
            w.RenderBeginTag(HtmlTextWriterTag.Script);
            w.WriteEncodedText(CreateCodeFile());
            w.RenderEndTag();

            w.RenderEndTag();
            w.RenderEndTag();

            w.Flush();
            sw.Close();

            return controls;
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
                w.Write("<%@ Page Language=\"C#\"%>");
        }

        private string CreateCodeFile()
        {
            var s = new StringBuilder();

            s.AppendLine("void onClick(object sender, EventArgs e){" + "\n");
            s.AppendFormat("Tester tester = new Tester();" + "\n");
            foreach (IUDICO.DataModel.WebControl.WebControl t in controls)
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