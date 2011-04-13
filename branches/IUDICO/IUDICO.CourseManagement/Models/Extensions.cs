using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using IUDICO.Common.Models;

namespace IUDICO.CourseManagement.Models
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<JsTreeNode> ToJsTrees(this IEnumerable<Node> list)
        {
            return (from n in list select new JsTreeNode(n.Id, n.Name, n.IsFolder)).AsEnumerable();
        }
    }

    public static class XmlSerializerExtensions
    {
        public static XElement SerializeToXElemet(this XmlSerializer xs, object  o)
        {
            var d = new XDocument();
            using (var w = d.CreateWriter())
            {
                xs.Serialize(w, o);
            }
            var e = d.Root;
            e.Remove();
            return e;
        }
        public static object DeserializeXElement(this XmlSerializer xs, XElement xe)
        {
            object o;

            using (var r = xe.CreateReader())
            {
                o = xs.Deserialize(r);
                r.Close();
            }

            return o;
        }
    }

}