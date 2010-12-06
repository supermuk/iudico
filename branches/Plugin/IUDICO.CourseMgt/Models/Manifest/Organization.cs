using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IUDICO.CourseMgt.Models.Manifest
{
    [Serializable]
    public class Organization
    {
        [XmlAttribute(Strings.Identifier)]
        public string Identifier;

        [XmlAttribute(Strings.Structure)]
        public string Structure;// = "hierarchical";

        [XmlAttribute(Strings.ObjectivesGlobalToSystem)]
        public string ObjectivesGlobalToSystem; // = "true";

        [XmlAttribute(Strings.SharedDataGlobalToSystem)]
        public string SharedDataGlobalToSystem;// = "true";

        [XmlElement(Strings.Title, Namespace=Strings.ImscpNamespaceV1p3)]
        public string Title;

        [XmlElement(Strings.Item, Namespace=Strings.ImscpNamespaceV1p3)]
        public Item Item;

        [XmlElement(Strings.Metadata, Namespace = Strings.ImscpNamespaceV1p3)]
        public Metadata Metadata;

        //public CompletionThreashold CompletionThreashold;
        //public Sequencing Sequencing;
    }
}