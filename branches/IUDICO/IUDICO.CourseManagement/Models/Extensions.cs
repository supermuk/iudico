using System.Collections.Generic;
using System.Linq;
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
}