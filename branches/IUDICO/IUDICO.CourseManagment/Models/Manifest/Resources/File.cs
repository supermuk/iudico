using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IUDICO.CourseManagment.Models.Manifest
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
        public Metadata Metadata;
    }
}
