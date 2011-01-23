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
            _IsParent = true;
            IsVisible = true;
            Items = new List<Item>();
            Identifier = ConstantStrings.ItemIdPrefix + Guid.NewGuid();
        }
        
        /// <summary>
        /// Leaf item
        /// </summary>
        /// <param name="resourceId"></param>
        public Item(string resourceId)
        {
            _IsParent = false;
            IsVisible = true;
            IdentifierRef = resourceId;
            Identifier = ConstantStrings.ItemIdPrefix + Guid.NewGuid();
        }

        #region Members

        private readonly bool _IsParent;

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

        [XmlElement(SCORM.Title, Namespace=SCORM.ImscpNamespaceV1P3)]
        public string Title;

        [XmlElement(SCORM.Item, Namespace=SCORM.ImscpNamespaceV1P3)]
        public List<Item> Items;

        [XmlElement(SCORM.Metadata, Namespace=SCORM.ImscpNamespaceV1P3)]
        public MetadataModels.Metadata Metadata;

        [XmlElement(SCORM.TimeLimitActionV1P3, Namespace = SCORM.AdlcpNamespaceV1P3)]
        public TimeLimitAction? TimeLimitAction;

        public bool ShouldSerializeTimeLimitAction()
        {
            return TimeLimitAction.HasValue;
        }

        [XmlElement(SCORM.DataFromLmsV1P3, Namespace=SCORM.AdlcpNamespaceV1P3)]
        public string DataFromLMS;

        [XmlElement(SCORM.CompletionThreshold, Namespace=SCORM.AdlcpNamespaceV1P3)]
        public CompletionThreshold CompletionThreshold;

        [XmlElement(SCORM.Sequencing)]
        public Sequencing Sequencing;

        [XmlElement(SCORM.Presentation)]
        public Presentation Presentation;
        
        [XmlElement(SCORM.Data, Namespace=SCORM.AdlcpNamespaceV1P3)]
        public List<Map> Data;

        #endregion

        #region Methods

        public void AddChildItem(Item item)
        {
            if (!_IsParent)
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