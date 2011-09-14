using System.Web.UI;
using System.Xml;
using CourseImport.Common;

namespace IUDICO.DataModel.WebControl
{
    internal class WebCompiledTest : WebTestControlBase
    {
        private string _text;

        public override void Parse(XmlNode node)
        {
            base.Parse(node);
            _text = node.InnerText;
        }

        public override void Store(HtmlTextWriter w)
        {
            base.Store(w);

            if (_text != null)
                w.AddAttribute("InnerText", HtmlUtility.QuotesEncode(_text));

            w.RenderBeginTag("it:CompiledTest");
            w.RenderEndTag();
        }
    }
}