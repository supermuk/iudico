using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.IO;
namespace IUDICO.CourseManagment.Models.Manifest
{
    [Serializable]
    public class Manifest
    {
        public Manifest()
        {
            Organizations = new Organizations();
            Resources = new Resources();
            Metadata = new ManifestMetadata();
        }

        [XmlElement(SCORM.Metadata, Namespace = SCORM.ImscpNamespaceV1p3)]
        public ManifestMetadata Metadata;

        [XmlElement(SCORM.Organizations, Namespace=SCORM.ImscpNamespaceV1p3)]
        public Organizations Organizations;

        [XmlElement(SCORM.Resources, Namespace = SCORM.ImscpNamespaceV1p3)]
        public Resources Resources;

        public void Serialize(StreamWriter writer)
        {
            XmlSerializer xs = new XmlSerializer(typeof(IUDICO.CourseManagment.Models.Manifest.Manifest));

            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
            xsn.Add(SCORM.Adlcp, SCORM.AdlcpNamespaceV1p3);
            xsn.Add(SCORM.Imscp, SCORM.ImscpNamespaceV1p3);
            xsn.Add(SCORM.Imsss, SCORM.ImsssNamespace);

            xs.Serialize(writer, this, xsn);
        }
    }
}