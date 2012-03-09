using System;
using System.Collections;
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
        }

        #region XmlAttributes

        [XmlAttribute(SCORM.Default)]
        public string Default;

        #endregion

        #region XmlElements

        [XmlElement(SCORM.Organization, Namespace = SCORM.ImscpNamespaceV1P3)]
        public List<Organization> _Organizations;

        #endregion

        #region Methods

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

        #endregion
    }
}