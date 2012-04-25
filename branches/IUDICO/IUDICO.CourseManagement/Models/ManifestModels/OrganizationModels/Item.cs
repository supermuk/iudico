using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;

namespace IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels
{
    [Serializable]
    public class Item
    {
        public Item()
        {
            this.IsVisible = true;
            this.Items = new List<Item>();
        }
        
        /// <summary>
        /// Leaf item
        /// </summary>
        /// <param name="resourceId"></param>
        public Item(string resourceId)
        {
            this.IsVisible = true;
            this.IdentifierRef = resourceId;
            this.Identifier = ConstantStrings.ItemIdPrefix + Guid.NewGuid();
            this.Items = new List<Item>();
        }

        #region Members

        [XmlIgnore]
        public bool IsParent
        {
            get
            {
                return this.IdentifierRef == null;
            }
        }

        #endregion

        #region XmlAttributes

        [XmlAttribute(SCORM.Identifier)]
        public string Identifier;

        [XmlAttribute(SCORM.IdentifierRef)]
        public string IdentifierRef;

        [XmlAttribute(SCORM.IsVisible)]
        public bool IsVisible; // = true;

        [XmlAttribute(SCORM.Parameters)]
        public string Parameters;

        #endregion

        #region XmlElements

        [XmlElement(SCORM.Title, Namespace = SCORM.ImscpNamespaceV1P3)]
        public string Title;

        [XmlElement(SCORM.Item, Namespace = SCORM.ImscpNamespaceV1P3)]
        public List<Item> Items;

        [XmlElement(SCORM.Metadata, Namespace = SCORM.ImscpNamespaceV1P3)]
        public MetadataModels.Metadata Metadata;

        [XmlElement(SCORM.TimeLimitActionV1P3, Namespace = SCORM.AdlcpNamespaceV1P3)]
        public TimeLimitAction? TimeLimitAction;

        public bool ShouldSerializeTimeLimitAction()
        {
            return this.TimeLimitAction.HasValue;
        }

        [XmlElement(SCORM.DataFromLmsV1P3, Namespace = SCORM.AdlcpNamespaceV1P3)]
        public string DataFromLMS;

        [XmlElement(SCORM.CompletionThreshold, Namespace = SCORM.AdlcpNamespaceV1P3)]
        public CompletionThreshold CompletionThreshold;

        [XmlElement(SCORM.Sequencing, Namespace = SCORM.ImsssNamespace)]
        public Sequencing Sequencing;

        [XmlElement(SCORM.Presentation)]
        public Presentation Presentation;

        [XmlElement(SCORM.Data, Namespace = SCORM.AdlcpNamespaceV1P3)]
        public List<Map> Data;

        #endregion

    }
}