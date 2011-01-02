﻿using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.ResourceModels
{
    [Serializable]
    public class File
    {
        public File()
        {
        }

        public File(string href)
        {
            Href = href;
        }

        #region XmlAttributes

        [XmlAttribute(SCORM.Href)]
        public string Href;

        #endregion

        #region XmlElement

        [XmlElement(SCORM.Metadata, Namespace = SCORM.ImscpNamespaceV1p3)]
        public MetadataModels.Metadata Metadata;

        #endregion
    }
}