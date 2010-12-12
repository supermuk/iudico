using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.Manifest.Resources
{
    [Serializable]
    public class Dependency
    {
        public Dependency()
        {
        }

        public Dependency(string identidierRef)
        {
            IdentifierRef = identidierRef;
        }

        [XmlAttribute(SCORM.IdentifierRef)]
        public string IdentifierRef;
    }
}
