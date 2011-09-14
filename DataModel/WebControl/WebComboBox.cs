using System.Collections.Generic;
using System.Web.UI;
using System.Xml;
using LEX.CONTROLS;

namespace IUDICO.DataModel.WebControl
{
    internal class WebComboBox : WebTestControlBase
    {
        private readonly List<string> _items = new List<string>();

        public override void Parse([NotNull] XmlNode node)
        {
            base.Parse(node);
            foreach (XmlNode sub in node.ChildNodes)
                _items.Add(sub.InnerText);
        }

        public override void Store([NotNull] HtmlTextWriter w)
        {
            base.Store(w);

            w.RenderBeginTag("it:ComboBoxTest");

            foreach(var i in _items)
                w.WriteLine(string.Format(@"<asp:ListItem Text=""{0}"" />", i));

            w.RenderEndTag();
        }
    }
}