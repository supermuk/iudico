using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using CourseImport.Common;

namespace IUDICO.DataModel.WebControl
{
    internal class WebSimpleQuestion : WebTestControl
    {
        private readonly List<string> list = new List<string>();
        private string question;
        private bool singleCase;

        public override void Parse(XmlNode node)
        {
            base.Parse(node);

            singleCase = node.Attributes["name"].Value.EndsWith("single");
            question = node.SelectSingleNode("p").InnerText;
            XmlNodeList spans = node.SelectNodes("span");
            if (spans != null)
                foreach (XmlNode s in spans)
                {
                    list.Add(s.InnerText);
                }
        }

        public override void Store(HtmlTextWriter w)
        {
            base.Store(w);
            w.AddAttribute(HtmlTextWriterAttribute.Name, singleCase ? "gen:single" : "gen:multy");
            w.AddAttribute("runat", "server");
            w.RenderBeginTag(HtmlTextWriterTag.Div);
            w.RenderBeginTag(HtmlTextWriterTag.P);
            w.Write(HttpUtility.HtmlEncode(question));
            w.RenderEndTag();
            foreach (string text in list)
            {
                w.AddAttribute(HtmlTextWriterAttribute.Id, Name.ToLower() + "_" + text);
                w.AddAttribute("GroupName", Name.ToLower());
                w.AddAttribute("runat", "server");
                w.RenderBeginTag(singleCase ? "asp:radiobutton" : "asp:checkbox");
                w.RenderEndTag();
                w.Write(HtmlUtility.QuotesEncode(text));
                w.RenderBeginTag(HtmlTextWriterTag.Br);
                w.RenderEndTag();
            }
            w.RenderEndTag();
        }

        public override string CreateCodeForTest(int testId)
        {
            var s = new StringBuilder();

            for (int i = 0; i < list.Count; i++)
            {
                s.AppendFormat("{0}.Checked", Name.ToLower() + "_" + list[i]);

                if (i != list.Count - 1)
                    s.Append(',');
            }

            return string.Format("IUDICO.DataModel.WebTest.SimpleQuestionTest({0}, {1})", testId, s);
        }
    }
}