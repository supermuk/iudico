namespace IUDICO.CourseManagement.Models
{
    public class JsTreeNode
    {
        public string data { get; set; }
        public string state { get; set; }
        public JsTreeAttributes attr { get; set; }

        public JsTreeNode(int _id, string _data, bool _folder)
        {
            data = _data;
            state = (_folder ? "closed" : "");
            attr = new JsTreeAttributes { id = "node_" + _id.ToString(), rel = (_folder ? "folder" : "default") };
        }
    }

    public class JsTreeAttributes
    {
        public string id { get; set; }
        public string rel { get; set; }
    }
}