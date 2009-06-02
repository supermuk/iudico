using System.Web.UI;
using System.Xml;

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

            w.RenderBeginTag("it:CompiledTest");

            if (_text != null)
                w.Write(_text);

            w.RenderEndTag();
        }
    }
}