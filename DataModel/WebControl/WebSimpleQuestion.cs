using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Xml;

namespace IUDICO.DataModel.WebControl
{
    internal class WebSimpleQuestion : WebTestControlBase
    {
        private readonly List<string> _list = new List<string>();

        private string _question;
        private bool _singleCase;

        public override void Parse(XmlNode node)
        {
            base.Parse(node);

            _singleCase = node.Attributes["name"].Value.EndsWith("single");
            _question = node.SelectSingleNode("p").InnerText;
            XmlNodeList spans = node.SelectNodes("span");
            if (spans != null)
                foreach (XmlNode s in spans)
                    _list.Add(s.InnerText);
        }

        public override void Store(HtmlTextWriter w)
        {
            base.Store(w);

            w.AddAttribute("SingleCase", _singleCase.ToString());
            
            string questionText = string.Format("{0}",
                                                HttpUtility.HtmlEncode(_question).Replace("\n", "<br />"));
            
            w.AddAttribute("QuestionText", string.Format("{0}", questionText));

            w.RenderBeginTag("it:SimpleQuestionTest");

            foreach (var i in _list)
                w.WriteLine(string.Format(@"<asp:ListItem Text=""{0}"" />", HttpUtility.HtmlEncode(i)));

            w.RenderEndTag();
        }
    }
}