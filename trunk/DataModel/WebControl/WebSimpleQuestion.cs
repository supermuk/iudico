using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using CourseImport.Common;
using IUDICO.DataModel.Common;

namespace IUDICO.DataModel.WebControl
{
    internal class WebSimpleQuestion : WebTestControl
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
                {
                    _list.Add(s.InnerText);
                }
        }

        public override void Store(HtmlTextWriter w)
        {
            base.Store(w);
            w.AddAttribute(HtmlTextWriterAttribute.Name, _singleCase ? "gen:single" : "gen:multy");
            w.AddAttribute("runat", "server");
            w.RenderBeginTag(HtmlTextWriterTag.Div);
            w.RenderBeginTag(HtmlTextWriterTag.P);
            w.Write(HttpUtility.HtmlEncode(_question));
            w.RenderEndTag();
            foreach (string text in _list)
            {
                w.AddAttribute(HtmlTextWriterAttribute.Id, CreateId(Name, text) );
                w.AddAttribute("GroupName", Name);
                w.AddAttribute("runat", "server");
                w.RenderBeginTag(_singleCase ? "asp:radiobutton" : "asp:checkbox");
                w.RenderEndTag();
                w.Write(HtmlUtility.QuotesEncode(text));
                w.RenderBeginTag(HtmlTextWriterTag.Br);
                w.RenderEndTag();
            }
            w.RenderEndTag();
        }

        private static string CreateId(string name, string text)
        {
            return (name + "_" + GetMd5Hash(text));
        }

        public override string CreateCodeForTest(int testId)
        {
            var s = new StringBuilder();

            for (int i = 0; i < _list.Count; i++)
            {
                s.AppendFormat("{0}.Checked", CreateId(Name, _list[i]) );

                if (i != _list.Count - 1)
                    s.Append(',');
            }

            return string.Format("IUDICO.DataModel.WebTest.SimpleQuestionTest({0}, {1})", testId, s);
        }

        public override string CreateAnswerFillerCode(string answerFillerVaribleName)
        {
            var s = new StringBuilder();

            for (int i = 0; i < _list.Count; i++)
            {
                s.AppendFormat("{0}", CreateId(Name, _list[i]));

                if (i != _list.Count - 1)
                    s.Append(',');
            }

            return string.Format("{0}.SetAnswer(\"{1}\", {2});", answerFillerVaribleName, Name, s);
        }

        public static string GetMd5Hash(string input)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();

            byte[] bs = StudentHelper.GetEncoding().GetBytes(input);
            bs = x.ComputeHash(bs);

            StringBuilder s = new StringBuilder();

            foreach (byte b in bs)
                s.Append(b.ToString("x2").ToLower());
            
            return s.ToString();
        }
    }
}