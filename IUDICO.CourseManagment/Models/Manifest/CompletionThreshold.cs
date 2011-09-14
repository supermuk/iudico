using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IUDICO.CourseManagment.Models.Manifest
{
    [Serializable]
    public class CompletionThreshold
    {
        
        [XmlAttribute("completedByMeasure")]
        public bool CompletedByMeasure; //false

        [XmlAttribute("minProgressMeasure")]
        public float MinProgressMeasure;

        [XmlAttribute("progressWeight")]
        public float ProgressWeight;
    }
}