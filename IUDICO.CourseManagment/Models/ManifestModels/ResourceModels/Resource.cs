using System;
using System.Collections.Generic;
using System.Xml.Serialization;

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

        public Resource(ScormType type, List<File> files)
        {
            Files = files;
            Type = SCORM.Webcontent;
            ScormType = type;
        }

        public Resource(ScormType type, List<File> files, IEnumerable<string> dependOnResourcesIds)
            :this(type, files)
        {
            Dependencies = new List<Dependency>();

            foreach (var resId in dependOnResourcesIds)
            {
                Dependencies.Add(new Dependency(resId));
            }

        }

        [XmlAttribute(SCORM.Identifier)]
        public string Identifier;

        [XmlAttribute(SCORM.Type)]
        public string Type;

        [XmlAttribute(SCORM.Href)]
        public string Href;

        [XmlAttribute(SCORM.Base, Namespace = SCORM.XmlNamespace)]
        public string Base;

        [XmlAttribute(SCORM.ScormTypeV1p3, Namespace = SCORM.AdlcpNamespaceV1p3)]
        public ScormType ScormType;



        [XmlElement(SCORM.Metadata, Namespace = SCORM.ImscpNamespaceV1p3)]
        public MetadataModels.Metadata Metadata;

        [XmlElement(SCORM.File, Namespace = SCORM.ImscpNamespaceV1p3)]
        public List<File> Files;

        [XmlElement(SCORM.Dependency, Namespace = SCORM.ImscpNamespaceV1p3)]
        public List<Dependency> Dependencies;
    }
}