using System;
using System.Web.UI;
using System.Xml;

namespace IUDICO.DataModel.WebControl
{
    internal class WebLabel : WebControlBase
    {
        private string _text;

        public override void Parse(XmlNode node)
        {
            base.Parse(node);
            _text = node.InnerXml.Replace("<br />", Environment.NewLine);
        }

        public override void Store(HtmlTextWriter w)
        {
            base.Store(w);
            w.RenderBeginTag(HtmlTextWriterTag.Span);
            w.Write(_text.Replace(Environment.NewLine, "<br />"));
            w.RenderEndTag();
        }
    }
}