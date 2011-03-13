﻿using System;
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
            _DefaultOrganizationIndex = 0;
        }

        #region Members

        [XmlIgnore]
        private int _DefaultOrganizationIndex;

        #endregion

        #region XmlAttributes

        [XmlAttribute(SCORM.Default)]
        public string Default
        {
            get
            {
                return _Organizations[_DefaultOrganizationIndex].Identifier;
            }
            set
            {
                for (var i = 0; i < _Organizations.Count; i++)
                {
                    if (_Organizations[i].Identifier != value)
                    {
                        continue;
                    }

                    _DefaultOrganizationIndex = i;
                        
                    return;
                }

                _DefaultOrganizationIndex = 0;
            }
        }

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


        #endregion
    }
}