using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
namespace IUDICO.CourseManagment.Models.Manifest
{
    [Serializable]
    public class Manifest
    {
        [XmlElement(Strings.Metadata, Namespace = Strings.ImscpNamespaceV1p3)]
        public Metadata Metadata;

        [XmlElement(Strings.Organizations, Namespace=Strings.ImscpNamespaceV1p3)]
        public Organizations Organizations;

        [XmlElement(Strings.Resources, Namespace = Strings.ImscpNamespaceV1p3)]
        public Resources Resources;
    }
}