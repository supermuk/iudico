using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.CourseManagment.Models
{
    public class JsTreeNode
    {
        public JsTreeNode(int _id, string _data, bool _folder)
        {
            data = _data;
            state = (_folder ? "closed" : "");
            attr = new JsTreeAttributes {id = _id.ToString(), rel = (_folder ? "folder" : "default")};
        }

        public string data;
        public string state;
        public JsTreeAttributes attr;
    }

    public class JsTreeAttributes
    {
        public string id;
        public string rel;
    }
}