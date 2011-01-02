using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;

namespace IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels
{
    [Serializable]
    public class Item
    {
        /// <summary>
        /// Folder item
        /// </summary>
        public Item()
        {
            IsParent = true;
            IsVisible = true;
            Items = new List<Item>();
            Identifier = ConstantStrings.ItemIdPrefix + Guid.NewGuid().ToString();
        }
        
        /// <summary>
        /// Leaf item
        /// </summary>
        /// <param name="resourceId"></param>
        public Item(string resourceId)
        {
            IsParent = false;
            IsVisible = true;
            IdentifierRef = resourceId;
            Identifier = ConstantStrings.ItemIdPrefix + Guid.NewGuid().ToString();
        }

        #region Members

        private readonly bool IsParent;

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

        [XmlElement(SCORM.Title, Namespace=SCORM.ImscpNamespaceV1p3)]
        public string Title;

        [XmlElement(SCORM.Item, Namespace=SCORM.ImscpNamespaceV1p3)]
        public List<Item> Items;

        [XmlElement(SCORM.Metadata, Namespace=SCORM.ImscpNamespaceV1p3)]
        public MetadataModels.Metadata Metadata;

        [XmlElement(SCORM.TimeLimitActionV1p3, Namespace = SCORM.AdlcpNamespaceV1p3)]
        public TimeLimitAction? TimeLimitAction;

        public bool ShouldSerializeTimeLimitAction()
        {
            return TimeLimitAction.HasValue;
        }

        [XmlElement(SCORM.DataFromLmsV1p3, Namespace=SCORM.AdlcpNamespaceV1p3)]
        public string DataFromLMS;

        [XmlElement(SCORM.CompletionThreshold, Namespace=SCORM.AdlcpNamespaceV1p3)]
        public CompletionThreshold CompletionThreshold;

        [XmlElement(SCORM.Sequencing)]
        public Sequencing Sequencing;

        [XmlElement(SCORM.Presentation)]
        public Presentation Presentation;
        
        [XmlElement(SCORM.Data, Namespace=SCORM.AdlcpNamespaceV1p3)]
        public List<Map> Data;

        #endregion

        #region Methods

        public void AddChildItem(Item item)
        {
            if (!IsParent)
            {
                throw new Exception("Can't add child item to leaf item");
            }
            //var idPrefix = string.IsNullOrEmpty(Identifier) ? ConstantStrings.ItemIdPrefix : Identifier;
            //item.Identifier = ConstantStrings.ItemIdPrefix + "_" + Guid.NewGuid().ToString();
            Items.Add(item);
        }

        #endregion
    }
}