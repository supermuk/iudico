using System;
using System.Xml.Serialization;
using System.IO;
using IUDICO.CourseManagement.Models.Manifest.Metadata;
using IUDICO.CourseManagement.Models.Manifest.Organization;

namespace IUDICO.CourseManagement.Models.Manifest
{
    [Serializable]
    [XmlRoot(SCORM.Manifest)]
    public class Manifest
    {
        public Manifest()
        {
            Organizations = new Organizations();
            Resources = new Resources.Resources();
            Metadata = new ManifestMetadata();
        }

        [XmlElement(SCORM.Metadata, Namespace = SCORM.ImscpNamespaceV1p3)]
        public ManifestMetadata Metadata;

        [XmlElement(SCORM.Organizations, Namespace=SCORM.ImscpNamespaceV1p3)]
        public Organizations Organizations;

        [XmlElement(SCORM.Resources, Namespace = SCORM.ImscpNamespaceV1p3)]
        public Resources.Resources Resources;

        public void Serialize(StreamWriter writer)
        {
            var xs = new XmlSerializer(typeof(Manifest));

            var xsn = new XmlSerializerNamespaces();
            xsn.Add(SCORM.Adlcp, SCORM.AdlcpNamespaceV1p3);
            xsn.Add(SCORM.Imscp, SCORM.ImscpNamespaceV1p3);
            xsn.Add(SCORM.Imsss, SCORM.ImsssNamespace);

            xs.Serialize(writer, this, xsn);
        }
    }
}