using System.Collections.Generic;
using System.Web.UI;
using System.Xml;
using LEX.CONTROLS;

namespace IUDICO.DataModel.WebControl
{
    internal class WebComboBox : WebTestControl
    {
        private readonly List<string> _items = new List<string>();

        public override void Parse([NotNull] XmlNode node)
        {
            base.Parse(node);
            foreach (XmlNode sub in node.ChildNodes)
            {
                _items.Add(sub.InnerText);
            }
        }

        public override void Store([NotNull] HtmlTextWriter w)
        {
            base.Store(w);
            w.AddAttribute("runat", "server");
            w.RenderBeginTag("asp:DropDownList");

            int count = _items.Count;
            for (int i = 0; i < count; i++)
            {
                w.RenderBeginTag("asp:ListItem");
                w.Write(_items[i]);
                w.RenderEndTag();
            }

            w.RenderEndTag();
        }

        public override string CreateCodeForTest(int testId)
        {
            return string.Format("IUDICO.DataModel.WebTest.ComboBoxTest({0}.SelectedIndex, {1})", Name, testId);
        }

        public override string CreateAnswerFillerCode(string answerFillerVaribleName)
        {
            return string.Empty;
        }
    }
}