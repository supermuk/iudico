namespace IUDICO.CourseManagement.Models
{
    public class JsTreeNode
    {
        public string Data { get; set; }
        public string State { get; set; }
        public JsTreeAttributes Attr { get; set; }

        public JsTreeNode(int id, string data, bool folder)
        {
            Data = data;
            State = (folder ? "closed" : "");
            Attr = new JsTreeAttributes { Id = id.ToString(), Rel = (folder ? "folder" : "default") };
        }
    }

    public class JsTreeAttributes
    {
        public string Id { get; set; }
        public string Rel { get; set; }
    }
}