namespace IUDICO.CourseManagement.Models
{
    using System.Globalization;

    public class JsTreeNode
    {
        public string Data { get; set; }
        public string State { get; set; }
        public JsTreeAttributes Attr { get; set; }

        public JsTreeNode(int id, string data, bool folder)
        {
            this.Data = data;
            this.State = (folder ? "closed" : string.Empty);
            this.Attr = new JsTreeAttributes { Id = "node_" + id.ToString(CultureInfo.InvariantCulture), Rel = (folder ? "folder" : "default") };
        }
    }

    public class JsTreeAttributes
    {
        public string Id { get; set; }
        public string Rel { get; set; }
    }
}