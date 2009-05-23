using System.Web.UI;
using System.Xml;
using CourseImport.Common;
using LEX.CONTROLS;

namespace IUDICO.DataModel.WebControl
{
    internal class WebTextBox : WebTestControl
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
            if (!string.IsNullOrEmpty(_text))
            {
                w.AddAttribute(HtmlTextWriterAttribute.Value, HtmlUtility.QuotesEncode(_text));
            }

            w.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            w.AddAttribute("runat", "server");
            w.RenderBeginTag("asp:TextBox");
            w.RenderEndTag();
        }

        public override string CreateCodeForTest(int testId)
        {
            return string.Format("IUDICO.DataModel.WebTest.TextBoxTest({0}.Text, {1})", Name, testId);
        }

        public override string CreateAnswerFillerCode(string answerFillerVaribleName)
        {
            return string.Format("{0}.SetAnswer({1});", answerFillerVaribleName, Name);
        }
    }
}