using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IUDICO.CourseMgt.Models.Manifest
{
    [Serializable]
    public class Item
    {
        [XmlAttribute(Strings.Identifier)]
        public string Identifier;

        [XmlAttribute(Strings.IdentifierRef)]
        public string IdentifierRef;

        [XmlAttribute(Strings.IsVisible)]
        public string IsVisible; // = "true";

        [XmlAttribute(Strings.Parameters)]
        public string Parameters;



        [XmlElement(Strings.Title, Namespace=Strings.ImscpNamespaceV1p3)]
        public string Title;

        [XmlElement(Strings.Item, Namespace=Strings.ImscpNamespaceV1p3)]
        public Item Item_;

        [XmlElement(Strings.Metadata, Namespace=Strings.ImscpNamespaceV1p3)]
        public Metadata Metadata;

        [XmlElement(Strings.TimeLimitActionV1p3, Namespace=Strings.AdlcpNamespaceV1p3)]
        public TimeLimitAction? TimeLimitAction;

        [XmlElement(Strings.DataFromLmsV1p3, Namespace=Strings.AdlcpNamespaceV1p3)]
        public string DataFromLMS;

        [XmlElement(Strings.CompletionThreshold, Namespace=Strings.AdlcpNamespaceV1p3)]
        public CompletionThreshold CompletionThreshold;

        [XmlElement(Strings.Sequencing)]
        public Sequencing Sequencing;

        [XmlElement(Strings.Presentation)]
        public Presentation Presentation;
        
        [XmlElement(Strings.Data, Namespace=Strings.AdlcpNamespaceV1p3)]
        public List<Map> Data;

    }
}