using System;
using System.Xml.Serialization;

namespace IUDICO.CourseManagement.Models.Manifest.Organization
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