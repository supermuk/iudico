namespace IUDICO.CourseManagement.Models
{
    using System.Globalization;

    public class JsTreeNode
    {
        public string data { get; set; }
        public string state { get; set; }
        public JsTreeAttributes attr { get; set; }

        public JsTreeNode(int id, string data, bool folder)
        {
            this.data = data;
            this.state = (folder ? "closed" : string.Empty);
            this.attr = new JsTreeAttributes { id = "node_" + id.ToString(CultureInfo.InvariantCulture), rel = (folder ? "folder" : "default") };
        }
    }

    public class JsTreeAttributes
    {
        public string id { get; set; }
        public string rel { get; set; }
    }
}