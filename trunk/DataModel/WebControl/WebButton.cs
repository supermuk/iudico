using System.Web.UI;
using System.Xml;
using CourseImport.Common;

namespace IUDICO.DataModel.WebControl
{
    internal class WebButton : WebControl
    {
        private string text;

        public override void Parse(XmlNode node)
        {
            base.Parse(node);
            text = HtmlUtility.QuotesDecode(node.Attributes["value"].Value);
        }

        public override void Store(HtmlTextWriter w)
        {
            base.Store(w);
            w.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            w.AddAttribute("Text", HtmlUtility.QuotesEncode(text));
            w.AddAttribute("runat", "server");
            w.AddAttribute(HtmlTextWriterAttribute.Onclick, "onClick");
            w.RenderBeginTag("asp:Button");
            w.RenderEndTag();
        }
    }
}