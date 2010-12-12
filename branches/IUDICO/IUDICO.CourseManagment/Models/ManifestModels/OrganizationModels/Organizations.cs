using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels
{
    [Serializable]
    public class Organizations
    {
        public Organizations()
        {
            _Organizations = new List<Organization>();

            AddOrganization(new Organization());

            _DefaultOrganizationIndex = 0;
        }

        [XmlIgnore]
        private int _DefaultOrganizationIndex;

        [XmlAttribute(SCORM.Default)]
        public string Default
        {
            get
            {
                return _Organizations[_DefaultOrganizationIndex].Identifier;
            }
            set
            {
                for(int i = 0; i < _Organizations.Count; i++)
                {
                    if(_Organizations[i].Identifier == value)
                    {
                        _DefaultOrganizationIndex = i;
                        
                        return;
                    }
                }

                _DefaultOrganizationIndex = 0;
            }
        }

        [XmlElement(SCORM.Organization, Namespace = SCORM.ImscpNamespaceV1p3)]
        public List<Organization> _Organizations;

        public Organization this[string identifier] 
        {
            get
            {
                return _Organizations.Single(i => i.Identifier == identifier);
            }
        }

        public Organization this[int index]
        {
            get
            {
                return _Organizations[index];
            }
            set
            {
                _Organizations[index] = value;
            }
        }

        public string AddOrganization(Organization organization)
        {
            organization.Identifier = ConstantStrings.OrganizationIdPrefix + _Organizations.Count();
            _Organizations.Add(organization);
            return organization.Identifier;
        }
    }
}