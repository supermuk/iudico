using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IUDICO.CourseManagment.Models.Manifest
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