using System.Xml.Serialization;

namespace IUDICO.Common.Models
{
    public class NodeProperty
    {
        [XmlIgnore]
        public int NodeId { get; set; }
        [XmlIgnore]
        public int CourseId { get; set; }
        [XmlIgnore]
        public string Type { get; set; }
    }
}