using System;
using System.Web;
using System.Web.UI;
using System.Xml;
using LEX.CONTROLS;

namespace IUDICO.DataModel.WebControl
{
    public enum LANGUAGE
    {
        Axapta = 1,
        Cpp = 2,
        Delphi = 4,
        HTML = 8,
        Java = 16,
        JavaScript = 32,
        Perl = 64,
        PHP = 128,
        Python = 256,
        RIB = 512,
        RSL = 1024,
        Ruby = 2048,
        Smalltalk = 4096,
        SQL = 8192,
        VBScript = 16384
    }

    internal class WebHighlightedCode : IUDICO.DataModel.WebControl.WebControl
    {
        private LANGUAGE language;
        private string text;

        public override void Parse([NotNull] XmlNode node)
        {
            base.Parse(node);
            node = node.SelectSingleNode("pre");
            text = node.InnerText;
            node = node.SelectSingleNode("code");
            language = (LANGUAGE) Enum.Parse(typeof (LANGUAGE), node.Attributes["class"].Value, true);
        }

        public override void Store(HtmlTextWriter w)
        {
            base.Store(w);
            string ls = language.ToString().ToLower();
            w.AddAttribute(HtmlTextWriterAttribute.Name, "code");
            w.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "auto");
            w.RenderBeginTag(HtmlTextWriterTag.Span);
            w.AddAttribute(HtmlTextWriterAttribute.Class, ls);
            w.WriteFullBeginTag(string.Concat("pre><code class=\"", ls, "\""));
            w.Write(HttpUtility.HtmlEncode(text));
            w.WriteFullBeginTag("/code></pre");
            w.RenderEndTag();
        }
    }
}