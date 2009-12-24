using System.Web.UI;
using System.Xml;
using CourseImport.Common;
using LEX.CONTROLS;

namespace IUDICO.DataModel.WebControl
{
    internal class WebTextBox : WebTestControlBase
    {
        private string _text;

        public override void Parse([NotNull] XmlNode node)
        {
            base.Parse(node);
            XmlAttribute at = node.Attributes["value"];
            _text = HtmlUtility.QuotesDecode(at != null ? at.Value : node.InnerText);
        }

        public override void Store(HtmlTextWriter w)
        {   
            base.Store(w);

            if (_text != null)
                w.AddAttribute("InnerText", HtmlUtility.QuotesEncode(_text));
            
            w.RenderBeginTag("it:TextBoxTest");
            w.RenderEndTag();
        }
    }
}