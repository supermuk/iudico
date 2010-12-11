using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
namespace IUDICO.CourseManagment.Models.Manifest
{
    [Serializable]
    public class Organizations
    {
        public Organizations()
        {
            Organizations_ = new List<Organization>();
            AddOrganization(new Organization());
            DefaultOrganizationIndex = 0;
        }

        [XmlIgnore]
        private int DefaultOrganizationIndex;

        [XmlAttribute(SCORM.Default)]
        public string Default
        {
            get
            {
                return Organizations_[DefaultOrganizationIndex].Identifier;
            }
            set
            {
                for(int i = 0; i < Organizations_.Count; i++)
                {
                    if(Organizations_[i].Identifier == value)
                    {
                        DefaultOrganizationIndex = i;
                        return;
                    }
                }
                DefaultOrganizationIndex = 0;
            }
        }

        [XmlElement(SCORM.Organization, Namespace = SCORM.ImscpNamespaceV1p3)]
        public List<Organization> Organizations_;

        public Organization this[string identifier] 
        {
            get
            {
                return Organizations_.Single(i => i.Identifier == identifier);
            }
        }

        public Organization this[int index]
        {
            get
            {
                return Organizations_[index];
            }
            set
            {
                Organizations_[index] = value;
            }
        }

        public string AddOrganization(Organization organization)
        {
            organization.Identifier = ConstantStrings.OrganizationIdPrefix + Organizations_.Count();
            Organizations_.Add(organization);
            return organization.Identifier;
        }
    }
}