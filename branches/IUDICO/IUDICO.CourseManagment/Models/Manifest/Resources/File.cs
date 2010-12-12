using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.Manifest.Resources
{
    [Serializable]
    public class File
    {
        public File()
        {
        }

        public File(string href)
        {
            Href = href;
        }

        [XmlAttribute(SCORM.Href)]
        public string Href;

        [XmlElement(SCORM.Metadata, Namespace = SCORM.ImscpNamespaceV1p3)]
        public Metadata.Metadata Metadata;
    }
}
