using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using IUDICO.CourseManagement.Models.ManifestModels.MetadataModels;

namespace IUDICO.CourseManagement.Models.ManifestModels.ResourceModels
{
    [Serializable]
    public class Resource
    {
        public Resource()
        {
            Files = new List<File>();
            Dependencies = new List<Dependency>();
            Type = SCORM.Webcontent;
        }

        #region XmlAttributes

        [XmlAttribute(SCORM.Identifier)]
        public string Identifier;

        [XmlAttribute(SCORM.Type)]
        public string Type;

        [XmlAttribute(SCORM.Href)]
        public string Href;

        [XmlAttribute(SCORM.Base, Namespace = SCORM.XmlNamespace)]
        public string Base;

        [XmlAttribute(SCORM.ScormTypeV1P3, Namespace = SCORM.AdlcpNamespaceV1P3)]
        public ScormType ScormType;

        #endregion

        #region XmlElements

        [XmlElement(SCORM.Metadata, Namespace = SCORM.ImscpNamespaceV1P3)]
        public Metadata Metadata;

        [XmlElement(SCORM.File, Namespace = SCORM.ImscpNamespaceV1P3)]
        public List<File> Files;

        [XmlElement(SCORM.Dependency, Namespace = SCORM.ImscpNamespaceV1P3)]
        public List<Dependency> Dependencies;

        #endregion
    }
}