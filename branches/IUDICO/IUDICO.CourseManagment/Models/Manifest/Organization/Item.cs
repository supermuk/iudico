using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace IUDICO.CourseManagment.Models.Manifest
{
    [Serializable]
    public class Item
    {
        public Item()
        {
            IsParent = true;
            Items = new List<Item>();
        }
        
        public Item(string resourceId)
        {
            IsParent = false;
            IdentifierRef = resourceId;
        }

        private bool IsParent;

        
        
        [XmlAttribute(SCORM.Identifier)]
        public string Identifier;

        [XmlAttribute(SCORM.IdentifierRef)]
        public string IdentifierRef;

        [XmlAttribute(SCORM.IsVisible)]
        public string IsVisible; // = "true";

        [XmlAttribute(SCORM.Parameters)]
        public string Parameters;



        [XmlElement(SCORM.Title, Namespace=SCORM.ImscpNamespaceV1p3)]
        public string Title;

        [XmlElement(SCORM.Item, Namespace=SCORM.ImscpNamespaceV1p3)]
        public List<Item> Items;

        [XmlElement(SCORM.Metadata, Namespace=SCORM.ImscpNamespaceV1p3)]
        public Metadata Metadata;

        private TimeLimitAction? timeLimitAction;

        [XmlElement(SCORM.TimeLimitActionV1p3, Namespace = SCORM.AdlcpNamespaceV1p3)]
        public string TimeLimitAction
        {
            get
            {
                switch (timeLimitAction)
                {
                    case IUDICO.CourseManagment.Models.Manifest.TimeLimitAction.ContinueNoMessage:
                        return "continue,no message";
                    case IUDICO.CourseManagment.Models.Manifest.TimeLimitAction.ContinueWithMessage:
                        return "continue,message";
                    case IUDICO.CourseManagment.Models.Manifest.TimeLimitAction.ExitNoMessage:
                        return "exit,no message";
                    case IUDICO.CourseManagment.Models.Manifest.TimeLimitAction.ExitWithMessage:
                        return "exit,message";
                    default:
                        return null;
                }
            }
        }

        [XmlElement(SCORM.DataFromLmsV1p3, Namespace=SCORM.AdlcpNamespaceV1p3)]
        public string DataFromLMS;

        [XmlElement(SCORM.CompletionThreshold, Namespace=SCORM.AdlcpNamespaceV1p3)]
        public CompletionThreshold CompletionThreshold;

        [XmlElement(SCORM.Sequencing)]
        public Sequencing Sequencing;

        [XmlElement(SCORM.Presentation)]
        public Presentation Presentation;
        
        [XmlElement(SCORM.Data, Namespace=SCORM.AdlcpNamespaceV1p3)]
        public List<Map> Data;



        public void AddChildItem(Item item)
        {
            if (!IsParent)
            {
                throw new Exception("Can't add child item to leaf item");
            }
            Items.Add(item);
        }
    }
}