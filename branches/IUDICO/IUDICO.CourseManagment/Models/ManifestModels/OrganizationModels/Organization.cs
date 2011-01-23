using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;

namespace IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels
{
    [Serializable]
    public class Organization
    {
        public Organization()
        {
            Items = new List<Item>();
            ObjectivesGlobalToSystem = true;
            SharedDataGlobalToSystem = true;
        }

        #region XmlAttributes

        [XmlAttribute(SCORM.Identifier)]
        public string Identifier;

        [XmlAttribute(SCORM.Structure)]
        public string Structure;// = "hierarchical";

        [XmlAttribute(SCORM.ObjectivesGlobalToSystem)]
        public bool ObjectivesGlobalToSystem; // = true;

        [XmlAttribute(SCORM.SharedDataGlobalToSystem)]
        public bool SharedDataGlobalToSystem;// = true;

        #endregion

        #region XmlElements

        [XmlElement(SCORM.Title, Namespace=SCORM.ImscpNamespaceV1P3)]
        public string Title;

        [XmlElement(SCORM.Item, Namespace=SCORM.ImscpNamespaceV1P3)]
        public List<Item> Items;

        [XmlElement(SCORM.Metadata, Namespace = SCORM.ImscpNamespaceV1P3)]
        public MetadataModels.Metadata Metadata;

        [XmlElement(SCORM.CompletionThreshold, Namespace = SCORM.AdlcpNamespaceV1P3)]
        public CompletionThreshold CompletionThreshold;
        
        [XmlElement(SCORM.Sequencing, Namespace=SCORM.ImsssNamespace)]
        public Sequencing Sequencing;

        #endregion

        #region Methods

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void ApplySequencingPattern(SequencingPattern pattern)
        {
            Sequencing = new Sequencing(SequencingPattern.OrganizationDefaultSequencingPattern);
        }


        #endregion
    }
}