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
        private readonly Dictionary<string, int> _answersIndexes = new Dictionary<string, int>();
        private readonly List<WebControlBase> _controls = new List<WebControlBase>();
        private byte[] _binaryRepresentation;

        public WebPage(string pathToPage)
        {
            var doc = new XmlDocument();
            using (var reader = new StreamReader(pathToPage)) // File.ReadAllText is not applicable here, because we should know how to deal with 2 encodings: Unicode and the old one - 1250.
            {
                doc.LoadXml(reader.ReadToEnd());
            }
            if (doc.DocumentElement != null)
            {
                SetAnswerIndexes(doc.DocumentElement);
                Parse(doc.DocumentElement);
            }
        }

        public byte[] BinaryRepresentation
        {
            get { return _binaryRepresentation; }
        }

        public List<WebControlBase> Controls
        {
            get { return _controls; }
        }

        private static WebControlBase GetControlForParse(XmlNode node)
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
                               ? (WebControlBase) new WebHighlightedCode()
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
            WebControlBase c = GetControlForParse(node);
            if (c != null)
            {
                c.Parse(node);
                if (c is WebTestControlBase)
                {
                    (c as WebTestControlBase).AnswerIndex = _answersIndexes[c.Name];
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

        public void TransformToAspControl(int pageRef, XmlNode answerNode, string pathToTempCourseFolder)
        {
            var sw = new StringWriter(); 

            ConstructPageCode(new HtmlTextWriter(sw));

            sw.Close();

            _binaryRepresentation = Encoding.Unicode.GetBytes(sw.GetStringBuilder().ToString());

            QuestionManager.Import(pageRef, answerNode, _controls, pathToTempCourseFolder);
        }

        private void ConstructPageCode(HtmlTextWriter w)
        {
            AddHeader(w);
            w.AddAttribute("runat", "server");
            w.AddStyleAttribute(HtmlTextWriterStyle.Position, "relative");
            w.RenderBeginTag(HtmlTextWriterTag.Div);
                StoreControls(w);
            w.RenderEndTag();
        }


        private void StoreControls(HtmlTextWriter w)
        {
            foreach (WebControlBase c in Controls)
            {
                c.Store(w);
                w.WriteLine();
            }
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
               //TODO: write proper regex for extract empty text from textbox control !!!
                _answersIndexes.Add(matches[i].Value.Trim(Convert.ToChar("'")), (i));
            }
        }

        private static void AddHeader(TextWriter w)
        {
            if (w == null) throw new ArgumentNullException("w");
            w.WriteLine(@"<%@ Control Language=""C#""%>");
            w.WriteLine(@"<%@ Register Src=""~/Controls/TestControls/TextBoxTest.ascx"" TagName=""TextBoxTest"" TagPrefix=""it"" %>");
            w.WriteLine(@"<%@ Register Src=""~/Controls/TestControls/ComboBoxTest.ascx"" TagName=""ComboBoxTest"" TagPrefix=""it"" %>");
            w.WriteLine(@"<%@ Register Src=""~/Controls/TestControls/CompiledTest.ascx"" TagName=""CompiledTest"" TagPrefix=""it"" %>");
            w.WriteLine(@"<%@ Register Src=""~/Controls/TestControls/SimpleQuestionTest.ascx"" TagName=""SimpleQuestionTest"" TagPrefix=""it"" %>");
            w.WriteLine();
        }
    }
}