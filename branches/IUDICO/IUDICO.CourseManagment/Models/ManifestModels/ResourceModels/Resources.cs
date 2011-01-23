using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.ResourceModels
{
    [Serializable]
    public class Resources
    {
        public Resources()
        {
            _Resources = new List<Resource>();
        }

        #region XmlElements

        [XmlElement(SCORM.Resource, Namespace = SCORM.ImscpNamespaceV1P3)]
        public List<Resource> _Resources;

        #endregion

        #region Methods

        public string AddResource(Resource resource)
        {
            resource.Identifier = ConstantStrings.ResourceIdPrefix + _Resources.Count;

            _Resources.Add(resource);

            return resource.Identifier;
        }

        #endregion
    }
}