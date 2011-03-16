using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels
{
    [Serializable]
    public class Organizations : IEnumerable<Organization>, IEnumerator<Organization>
    {
        public Organizations()
        {
            _Organizations = new List<Organization>();
            _DefaultOrganizationIndex = 0;
        }

        #region Members

        [XmlIgnore]
        private int _DefaultOrganizationIndex;

        [XmlIgnore]
        private int _Position;

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

        public string AddOrganization(Organization organization)
        {
            organization.Identifier = ConstantStrings.OrganizationIdPrefix + _Organizations.Count();
            
            _Organizations.Add(organization);
            
            return organization.Identifier;
        }

        #endregion

        #region Implementation of IEnumerable

        public IEnumerator<Organization> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
        }

        #endregion

        #region Implementation of IEnumerator

        public bool MoveNext()
        {
            if (_Position < _Organizations.Count - 1)
            {
                _Position++;
                
                return true;
            }

            return false;
        }

        public void Reset()
        {
            _Position = -1;
        }

        public Organization Current
        {
            get
            {
                return _Organizations[_Position];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        #endregion
    }
}