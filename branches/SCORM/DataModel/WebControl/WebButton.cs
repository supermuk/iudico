using System.Web.UI;
using System.Xml;
using CourseImport.Common;

namespace IUDICO.DataModel.WebControl
{
    internal class WebButton : WebControlBase
    {
        private string _text;

        public override void Parse(XmlNode node)
        {
            base.Parse(node);
            _text = HtmlUtility.QuotesDecode(node.Attributes["value"].Value);
        }

        public override void Store(HtmlTextWriter w)
        {
            base.Store(w);
            w.AddAttribute("Text", HtmlUtility.QuotesEncode(_text));
            w.AddAttribute("runat", "server");
            w.RenderBeginTag("asp:Button");
            w.RenderEndTag();
        }
    }
}