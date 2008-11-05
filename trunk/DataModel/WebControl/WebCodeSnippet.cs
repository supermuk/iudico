using System.Web.UI;
using System.Xml;

namespace IUDICO.DataModel.WebControl
{
    internal class WebCodeSnippet : WebControl
    {
        private string htmlCode;

        public override void Parse(XmlNode node)
        {
            base.Parse(node);
            htmlCode = node.InnerXml;
        }

        public override void Store(HtmlTextWriter w)
        {
            base.Store(w);
            w.AddAttribute(HtmlTextWriterAttribute.Name, "snippet");
            w.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "auto");
            w.RenderBeginTag(HtmlTextWriterTag.Div);
            w.Write(htmlCode);
            w.RenderEndTag();
        }
    }
}