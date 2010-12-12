using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels
{
    [Serializable]
    public class Map
    {
        [XmlAttribute("targetID")]
        public string TargetID;

        [XmlAttribute("readSharedData")]
        public string readSharedData; // = "true";

        [XmlAttribute("writeSharedData")]
        public string WriteSharedData; // = "true";
    }
}