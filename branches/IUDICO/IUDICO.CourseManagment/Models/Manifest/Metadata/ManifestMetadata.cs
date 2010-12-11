using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IUDICO.CourseManagment.Models.Manifest
{
    public class ManifestMetadata
    {
        public ManifestMetadata()
        {
            Schema = SCORM.SchemaName;
            SchemaVersion = SCORM.SchemaVersion2004;
        }

        [XmlElement(SCORM.Schema, Namespace = SCORM.ImscpNamespaceV1p3)]
        public string Schema;

        [XmlElement(SCORM.SchemaVersion, Namespace = SCORM.ImscpNamespaceV1p3)]
        public string SchemaVersion;

        [XmlElement(SCORM.Manifest)]
        Metadata Metadata;
    }
}
