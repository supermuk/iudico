using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.MetadataModels
{
    public class ManifestMetadata
    {
        public ManifestMetadata()
        {
            Schema = SCORM.SchemaName;
            SchemaVersion = SCORM.SchemaVersion2004;
        }

        #region XmlElements

        [XmlElement(SCORM.Schema, Namespace = SCORM.ImscpNamespaceV1P3)]
        public string Schema;

        [XmlElement(SCORM.SchemaVersion, Namespace = SCORM.ImscpNamespaceV1P3)]
        public string SchemaVersion;

        [XmlElement(SCORM.Manifest)]
        Metadata _Metadata;

        #endregion
    }
}
