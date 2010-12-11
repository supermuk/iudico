using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IUDICO.CourseManagment.Models.Manifest
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
            scormType = type;
        }

        public Resource(ScormType type, List<File> files, List<string> dependOnResourcesIds)
            :this(type, files)
        {
            Dependencies = new List<Dependency>();
            foreach (string resId in dependOnResourcesIds)
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

        private ScormType scormType;

        [XmlAttribute(SCORM.ScormTypeV1p3, Namespace = SCORM.AdlcpNamespaceV1p3)]
        public string ScormType
        {
            get
            {
                switch (scormType)
                {
                    case IUDICO.CourseManagment.Models.Manifest.ScormType.SCO:
                        return "sco";
                    case IUDICO.CourseManagment.Models.Manifest.ScormType.Asset:
                        return "asset";
                    default:
                        return null;
                }
            }
        }


        [XmlElement(SCORM.Metadata, Namespace = SCORM.ImscpNamespaceV1p3)]
        public Metadata Metadata;

        [XmlElement(SCORM.File, Namespace = SCORM.ImscpNamespaceV1p3)]
        public List<File> Files;

        [XmlElement(SCORM.Dependency, Namespace = SCORM.ImscpNamespaceV1p3)]
        public List<Dependency> Dependencies;
    }
}