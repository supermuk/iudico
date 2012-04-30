using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.ResourceModels
{
    [Serializable]
    public class Dependency
    {
        public Dependency()
        {
        }

        public Dependency(string identidierRef)
        {
            this.IdentifierRef = identidierRef;
        }

        #region XmlAttributes

        [XmlAttribute(SCORM.IdentifierRef)]
        public string IdentifierRef;

        #endregion
    }
}
