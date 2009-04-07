using System.Web.UI;
using System.Xml;

namespace IUDICO.DataModel.WebControl
{
    internal class WebCompiledTest : WebTestControl
    {
        private string text;

        public override void Parse(XmlNode node)
        {
            base.Parse(node);
            text = node.InnerText;
        }

        public override void Store(HtmlTextWriter w)
        {
            base.Store(w);
            w.AddAttribute("runat", "server");
            w.AddAttribute("TextMode", "multiline");
            w.RenderBeginTag("asp:TextBox");
            if (!string.IsNullOrEmpty(text))
            {
                w.Write(text);
            }
            w.RenderEndTag();
        }

        public override string CreateCodeForTest(int testId)
        {
            return string.Format("IUDICO.DataModel.WebTest.CompiledTest({0}.Text, {1})", Name, testId);
        }

        public override string CreateAnswerFillerCode(string answerFillerVaribleName)
        {
            return string.Format("{0}.SetAnswer({1});", answerFillerVaribleName, Name);
        }
    }
}