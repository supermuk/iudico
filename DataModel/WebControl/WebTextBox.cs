﻿using System.Web.UI;
using System.Xml;
using CourseImport.Common;
using LEX.CONTROLS;

namespace IUDICO.DataModel.WebControl
{
    internal class WebTextBox : WebTestControl
    {
        private string text;

        public override void Parse([NotNull] XmlNode node)
        {
            base.Parse(node);
            XmlAttribute at = node.Attributes["value"];
            text = HtmlUtility.QuotesDecode(at != null ? at.Value : node.InnerText);
        }

        public override void Store(HtmlTextWriter w)
        {
            base.Store(w);
            if (!string.IsNullOrEmpty(text))
            {
                w.AddAttribute(HtmlTextWriterAttribute.Value, HtmlUtility.QuotesEncode(text));
            }

            w.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            w.AddAttribute("runat", "server");
            w.RenderBeginTag("asp:TextBox");
            w.RenderEndTag();
        }

        public override string CreateCodeForTest()
        {
            return string.Format("TextBoxTest({0}.Text, {1})", Name, Id);
        }
    }
}