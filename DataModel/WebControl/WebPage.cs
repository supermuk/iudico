﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Xml;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.ImportManagers;

namespace IUDICO.DataModel.WebControl
{
    public class WebPage
    {
        private readonly Dictionary<string, int> _answersIndexes = new Dictionary<string, int>();
        private readonly List<WebControl> _controls = new List<WebControl>();
        private byte[] _byteRepresentation;

        public WebPage(string pathToPage)
        {
            var doc = new XmlDocument();
            doc.LoadXml(File.ReadAllText(pathToPage, StudentEncoding.GetEncoding()));
            if (doc.DocumentElement != null)
            {
                SetAnswerIndexes(doc.DocumentElement);
                Parse(doc.DocumentElement);
            }
        }

        public byte[] ByteRepresentation
        {
            get { return _byteRepresentation; }
        }

        public List<WebControl> Controls
        {
            get { return _controls; }
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
                    (c as WebTestControl).AnswerIndex = _answersIndexes[c.Name];
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

            _byteRepresentation = StudentEncoding.GetEncoding().GetBytes(sw.GetStringBuilder().ToString());

            QuestionManager.Import(pageRef, answerNode, _controls, pathToTempCourseFolder);
        }

        private void ConstructPageCode(HtmlTextWriter w, string pageName)
        {
            AddScriptHeader(w);
            WriteHtml(w, pageName);
        }

        private void WriteHtml(HtmlTextWriter w, string pageName)
        {
            w.RenderBeginTag(HtmlTextWriterTag.Html);

            DisableTextSelection(w);

            WriteHead(w, pageName);

            WriteBody(w);

            w.RenderEndTag();
        }

        private void WriteBody(HtmlTextWriter w)
        {
            w.AddAttribute("oncontextmenu", "return false"); //Disable context menu on page
            w.RenderBeginTag(HtmlTextWriterTag.Body);
            WriteForm(w);
            w.RenderEndTag();
        }

        private void WriteForm(HtmlTextWriter w)
        {
            w.AddAttribute("runat", "server");
            w.AddAttribute("OnLoad", "OnFormLoad");
            w.RenderBeginTag(HtmlTextWriterTag.Form);

            StoreControls(w);

            WriteScript(w);

            w.RenderEndTag();
        }

        private void StoreControls(HtmlTextWriter w)
        {
            foreach (WebControl c in Controls)
                c.Store(w);
        }

        private void WriteScript(HtmlTextWriter w)
        {
            w.AddAttribute("runat", "server");
            w.RenderBeginTag(HtmlTextWriterTag.Script);
            w.Write(CreateCodeFile());
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
                    _answersIndexes.Add(matches[i].Value.Trim(Convert.ToChar("'")), (i));
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
            CodeForAnswerFiller(s);
            CreateOnFormLoadEvent(s);
            CreateOnClickEvent(s);
            
            return s.ToString();
        }

        private static void DisableTextSelection(HtmlTextWriter w)
        {
            w.RenderBeginTag(HtmlTextWriterTag.Script);
            w.WriteLine("history.forward(0)");
            //w.WriteLine("document.onselectstart=new Function('return false');");
            //w.WriteLine("document.onmousedown=function(){return false;};");
            //w.WriteLine("document.onclick=function(){return true;};");
            w.RenderEndTag();
        }

        private void CreateOnClickEvent(StringBuilder s)
        {
            s.AppendLine("void onClick(object sender, EventArgs e)");
            s.AppendLine("{");
                s.AppendLine("var tester = new IUDICO.DataModel.Common.TestingUtils.Tester();");
                foreach (WebControl t in Controls)
                {
                    if (t is WebTestControl)
                    {
                        s.AppendFormat("tester.AddTest(new {0});", (t as WebTestControl).CreateCodeForTest());
                        s.AppendLine();
                    }

                }
                s.AppendLine();
                s.AppendLine("tester.TryToSubmit(Request);");
                s.AppendLine("tester.NextTestPage(Response, Request);");
            s.AppendLine("}");
        }

        private void CreateOnFormLoadEvent(StringBuilder s)
        {
            s.AppendLine("protected void OnFormLoad(object sender, EventArgs e)");
            s.AppendLine("{");
            
            CodeForUnitTesting(s);
            CodeForFillingAnswers(s);
            CodetForCheckingIsSubmitEnabled(s);

            s.AppendLine("}");
        }

        private void CodeForFillingAnswers(StringBuilder s)
        {
            s.AppendLine("if (IUDICO.DataModel.Common.TestRequestUtils.RequestConditionChecker.DoFillAnswers(Request))");
            s.AppendLine("{");
            s.AppendLine("FillAnswers();");
            CodeForDisableSubmit(s);
            s.AppendLine("return;");
            s.AppendLine("}");
        }

        private void CodeForUnitTesting(StringBuilder s)
        {
            s.AppendLine("if (IUDICO.DataModel.Common.TestRequestUtils.RequestConditionChecker.IsForUnitTesting(Request))");
            s.AppendLine("{");
            CodeForDisableSubmit(s);
            s.AppendLine("return;");
            s.AppendLine("}");
        }

        private void CodetForCheckingIsSubmitEnabled(StringBuilder s)
        {
            foreach (var t in Controls)
            {
                if (t is WebButton)
                {
                    s.AppendFormat("{0}.Enabled = IUDICO.DataModel.Common.TestRequestUtils.RequestConditionChecker.IsSubmitEnabled(Request);", t.Name);
                    s.AppendLine();
                }
            }
        }

        private void CodeForDisableSubmit(StringBuilder s)
        {
            foreach (var t in Controls)
            {
                if (t is WebButton)
                {
                    s.AppendFormat("{0}.Enabled = false;", t.Name);
                    s.AppendLine();
                }
            }
        }

        private void CodeForAnswerFiller(StringBuilder s)
        {
            s.AppendLine("void FillAnswers()");
            s.AppendLine("{");

            const string answerFillerVaribleName = "answerFiller";

            s.AppendFormat("var {0} = new IUDICO.DataModel.Common.TestingUtils.AnswerFiller(Request);", answerFillerVaribleName);
            s.AppendLine();
            foreach (var control in _controls)
            {
                if(control is WebTestControl)
                    s.AppendLine((control as WebTestControl).CreateAnswerFillerCode(answerFillerVaribleName));
            }
            s.AppendLine("}");
        }
    }
}