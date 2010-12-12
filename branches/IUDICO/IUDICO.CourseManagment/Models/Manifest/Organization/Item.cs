using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.Manifest.Organization
{
    [Serializable]
    public class Item
    {
        public Item()
        {
            IsParent = true;
            Items = new List<Item>();
        }
        
        public Item(string resourceId)
        {
            IsParent = false;
            IdentifierRef = resourceId;
        }

        private bool IsParent;
        
        [XmlAttribute(SCORM.Identifier)]
        public string Identifier;

        [XmlAttribute(SCORM.IdentifierRef)]
        public string IdentifierRef;

        [XmlAttribute(SCORM.IsVisible)]
        public string IsVisible; // = "true";

        [XmlAttribute(SCORM.Parameters)]
        public string Parameters;

        [XmlElement(SCORM.Title, Namespace=SCORM.ImscpNamespaceV1p3)]
        public string Title;

        [XmlElement(SCORM.Item, Namespace=SCORM.ImscpNamespaceV1p3)]
        public List<Item> Items;

        [XmlElement(SCORM.Metadata, Namespace=SCORM.ImscpNamespaceV1p3)]
        public Metadata.Metadata Metadata;

        [XmlElement(SCORM.TimeLimitActionV1p3, Namespace = SCORM.AdlcpNamespaceV1p3)]
        public TimeLimitAction TimeLimitAction;

        [XmlElement(SCORM.DataFromLmsV1p3, Namespace=SCORM.AdlcpNamespaceV1p3)]
        public string DataFromLMS;

        [XmlElement(SCORM.CompletionThreshold, Namespace=SCORM.AdlcpNamespaceV1p3)]
        public CompletionThreshold CompletionThreshold;

        [XmlElement(SCORM.Sequencing)]
        public Sequencing.Sequencing Sequencing;

        [XmlElement(SCORM.Presentation)]
        public Presentation Presentation;
        
        [XmlElement(SCORM.Data, Namespace=SCORM.AdlcpNamespaceV1p3)]
        public List<Map> Data;



        public void AddChildItem(Item item)
        {
            if (!IsParent)
            {
                throw new Exception("Can't add child item to leaf item");
            }
            item.Identifier = ConstantStrings.ItemIdPrefix + Guid.NewGuid().ToString();
            Items.Add(item);
        }
    }
}