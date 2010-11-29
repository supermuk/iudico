using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
namespace IUDICO.CourseMgt.Models.Manifest
{
    [Serializable]
    public class Organizations
    {
        [XmlIgnore]
        private Organization DefaultOrganization;

        [XmlAttribute(Strings.Default)]
        public string Default
        {
            get
            {
                return DefaultOrganization.Identifier;
            }
        }

        [XmlElement(Strings.Organization, Namespace=Strings.ImscpNamespaceV1p3)]
        public List<Organization> Organizations_;


    }
}