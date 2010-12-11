using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IUDICO.CourseManagment.Models.Manifest
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
