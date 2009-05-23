using System;
using System.Web;
using System.Web.UI;
using System.Xml;
using LEX.CONTROLS;

namespace IUDICO.DataModel.WebControl
{
    public enum Language
    {
        Axapta = 1,
        Cpp = 2,
        Delphi = 4,
        Html = 8,
        Java = 16,
        JavaScript = 32,
        Perl = 64,
        Php = 128,
        Python = 256,
        Rib = 512,
        Rsl = 1024,
        Ruby = 2048,
        Smalltalk = 4096,
        Sql = 8192,
        VbScript = 16384
    }

    internal class WebHighlightedCode : WebControl
    {
        private Language _language;
        private string _text;

        public override void Parse([NotNull] XmlNode node)
        {
            base.Parse(node);
            node = node.SelectSingleNode("pre");
            _text = node.InnerText;
            node = node.SelectSingleNode("code");
            _language = (Language) Enum.Parse(typeof (Language), node.Attributes["class"].Value, true);
        }

        public override void Store(HtmlTextWriter w)
        {
            base.Store(w);
            string ls = _language.ToString().ToLower();
            w.AddAttribute(HtmlTextWriterAttribute.Name, "code");
            w.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "auto");
            w.RenderBeginTag(HtmlTextWriterTag.Span);
            w.AddAttribute(HtmlTextWriterAttribute.Class, ls);
            w.WriteFullBeginTag(string.Concat("pre><code class=\"", ls, "\""));
            w.Write(HttpUtility.HtmlEncode(_text));
            w.WriteFullBeginTag("/code></pre");
            w.RenderEndTag();
        }
    }
}