using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IUDICO.CourseManagment.Models.Manifest
{
    [Serializable]
    public class Resources
    {
        public Resources()
        {
            Resources_ = new List<Resource>();

        }

        [XmlElement(SCORM.Resource, Namespace = SCORM.ImscpNamespaceV1p3)]
        public List<Resource> Resources_;

        public string AddResource(Resource resource)
        {
            resource.Identifier = ConstantStrings.ResourceIdPrefix + Resources_.Count.ToString();
            Resources_.Add(resource);
            return resource.Identifier;
        }
    }
}