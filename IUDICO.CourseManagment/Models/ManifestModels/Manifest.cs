using System;
using System.Xml.Serialization;
using System.IO;
using IUDICO.CourseManagement.Models.ManifestModels.MetadataModels;
using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
using IUDICO.CourseManagement.Models.ManifestModels.ResourceModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;

namespace IUDICO.CourseManagement.Models.ManifestModels
{
    [Serializable]
    [XmlRoot(SCORM.Manifest, Namespace = SCORM.ImscpNamespaceV1P3)]
    public class Manifest
    {
        public Manifest()
        {
            Identifier = ConstantStrings.ManifestId;
            Organizations = new Organizations();
            Resources = new Resources();
            Metadata = new ManifestMetadata();
        }

        #region XmlAttributes

        [XmlAttribute(SCORM.Identifier)]
        public string Identifier;

        [XmlAttribute(SCORM.Version)]
        public string Version;

        [XmlAttribute(SCORM.Base, Namespace = SCORM.XmlNamespace)]
        public string Base;

        #endregion

        #region XmlElements

        [XmlElement(SCORM.Metadata, Namespace = SCORM.ImscpNamespaceV1P3)]
        public ManifestMetadata Metadata;

        [XmlElement(SCORM.Organizations, Namespace=SCORM.ImscpNamespaceV1P3)]
        public Organizations Organizations;

        [XmlElement(SCORM.Resources, Namespace = SCORM.ImscpNamespaceV1P3)]
        public Resources Resources;

        [XmlElement(SCORM.SequencingCollection, Namespace = SCORM.ImsssNamespace)]
        public SequencingCollection SequencingCollection;

        #endregion
    }
}