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
            this.OrganizationsList = new List<Organization>();
        }

        #region XmlAttributes

        [XmlAttribute(SCORM.Default)]
        public string Default;

        #endregion

        #region XmlElements

        [XmlElement(SCORM.Organization, Namespace = SCORM.ImscpNamespaceV1P3)]
        public List<Organization> OrganizationsList;

        #endregion

        #region Methods

        public Organization this[string identifier] 
        {
            get
            {
                return this.OrganizationsList.Single(i => i.Identifier == identifier);
            }
        }

        public Organization this[int index]
        {
            get
            {
                return this.OrganizationsList[index];
            }
            set
            {
                this.OrganizationsList[index] = value;
            }
        }

        public string AddOrganization(Organization organization)
        {
            organization.Identifier = ConstantStrings.OrganizationIdPrefix + this.OrganizationsList.Count();
            
            this.OrganizationsList.Add(organization);
            
            return organization.Identifier;
        }

        #endregion
    }
}