using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IUDICO.CourseManagment.Models.Manifest
{
    [Serializable]
    public class Organization
    {
        public Organization()
        {
            Items = new List<Item>();
        }

        [XmlAttribute(SCORM.Identifier)]
        public string Identifier;

        [XmlAttribute(SCORM.Structure)]
        public string Structure;// = "hierarchical";

        [XmlAttribute(SCORM.ObjectivesGlobalToSystem)]
        public string ObjectivesGlobalToSystem; // = "true";

        [XmlAttribute(SCORM.SharedDataGlobalToSystem)]
        public string SharedDataGlobalToSystem;// = "true";



        [XmlElement(SCORM.Title, Namespace=SCORM.ImscpNamespaceV1p3)]
        public string Title;

        [XmlElement(SCORM.Item, Namespace=SCORM.ImscpNamespaceV1p3)]
        public List<Item> Items;

        [XmlElement(SCORM.Metadata, Namespace = SCORM.ImscpNamespaceV1p3)]
        public Metadata Metadata;

        [XmlElement(SCORM.CompletionThreshold, Namespace = SCORM.AdlcpNamespaceV1p3)]
        public CompletionThreshold CompletionThreshold;
        
        [XmlElement(SCORM.Sequencing, Namespace=SCORM.ImsssNamespace)]
        public Sequencing Sequencing;



        public void AddItem(Item item)
        {
            Items.Add(item);
        }
    }

}