using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Serialization;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels
{
    [MetadataType(typeof(Metadata))]
    public partial class ControlMode : NodeProperty
    {

        private sealed class Metadata
        {
            [LocalizedDisplayName("Choice")]
            public bool Choice { get; set; } // = true
            [DisplayName("Choice Exit")]
            public bool ChoiceExit { get; set; } // = true
            [DisplayName("Flow")]
            public bool Flow { get; set; } // = false
            [LocalizedDisplayName("ForwardOnly")]
            public bool ForwardOnly { get; set; }
            [LocalizedDisplayName("UseCurrentAttemptObjectiveInfo")]
            public bool UseCurrentAttemptObjectiveInfo { get; set; }
            [LocalizedDisplayName("UseCurrentAttemptProgressInfo")]
            public bool UseCurrentAttemptProgressInfo { get; set; }

            [ScaffoldColumn(false)]
            [XmlIgnore]
            public int NodeId { get; set; }
            [ScaffoldColumn(false)]
            [XmlIgnore]
            public int CourseId { get; set; }
            [ScaffoldColumn(false)]
            [XmlIgnore]
            public string Type { get; set; }
        }
    }

    [MetadataType(typeof(Metadata))]
    public partial class LimitConditions : NodeProperty
    {
        private sealed class Metadata
        {
            [LocalizedDisplayName("AttemptLimit")]
            public int AttemptLimit { get; set; } // = true
            [LocalizedDisplayName("Choice Attempt Absolute Duration Limit")]
            public string AttemptAbsoluteDurationLimit { get; set; }

            [ScaffoldColumn(false)]
            [XmlIgnore]
            public int NodeId { get; set; }
            [ScaffoldColumn(false)]
            [XmlIgnore]
            public int CourseId { get; set; }
            [ScaffoldColumn(false)]
            [XmlIgnore]
            public string Type { get; set; }

        }
    }

    [MetadataType(typeof(Metadata))]
    public partial class ConstrainedChoiceConsiderations : NodeProperty
    {
        private sealed class Metadata
        {
            [LocalizedDisplayName("PreventActivation")]
            public bool PreventActivation { get; set; }
            [LocalizedDisplayName("ConstrainChoice")]
            public bool ConstrainChoice { get; set; }

            [ScaffoldColumn(false)]
            [XmlIgnore]
            public int NodeId { get; set; }
            [ScaffoldColumn(false)]
            [XmlIgnore]
            public int CourseId { get; set; }
            [ScaffoldColumn(false)]
            [XmlIgnore]
            public string Type { get; set; }
        }
    }

    [MetadataType(typeof(Metadata))]
    public partial class RandomizationControls : NodeProperty
    {
        public IEnumerable<SelectListItem> TimingList
        {
            get
            {
                var list = new List<SelectListItem>
                               {
                                   new SelectListItem
                                       {
                                           Text =IUDICO.CourseManagement.Localization.getMessage(Enum.GetName(typeof (Timing), Timing.Never)),
                                           Value = Timing.Never.ToString()
                                       },
                                   new SelectListItem
                                       {
                                           Text = IUDICO.CourseManagement.Localization.getMessage(Enum.GetName(typeof (Timing), Timing.Once)),
                                           Value = Timing.Once.ToString()
                                       },
                                   new SelectListItem
                                       {
                                           Text = IUDICO.CourseManagement.Localization.getMessage(Enum.GetName(typeof (Timing), Timing.OnEachNewAttempt)),
                                           Value = Timing.OnEachNewAttempt.ToString()
                                       }
                               };
                
                return list.AsEnumerable();
            }
        }
        private sealed class Metadata
        {
            [DropDownList(OptionLabel = "Randomization Timing", SourceProperty = "TimingList")]
            public Timing RandomizationTiming { get; set; } // = Timing.Never;
            [LocalizedDisplayName("SelectCount")]
            public int SelectCount { get; set; }
            [DisplayName("Reorder Children")]
            public bool ReorderChildren { get; set; } // = false;
            [DropDownList(OptionLabel = "Selection Timing", SourceProperty = "TimingList")]
            public Timing SelectionTiming { get; set; }// = Timing.Never;

            [ScaffoldColumn(false)]
            [XmlIgnore]
            public int NodeId { get; set; }
            [ScaffoldColumn(false)]
            [XmlIgnore]
            public int CourseId { get; set; }
            [ScaffoldColumn(false)]
            [XmlIgnore]
            public string Type { get; set; }
        }
    }


    [MetadataType(typeof(Metadata))]
    public partial class DeliveryControls : NodeProperty
    {
        private sealed class Metadata
        {
            [DisplayName("Tracked")]
            public bool Tracked { get; set; } // = true;
            [DisplayName("Completion Set By Content")]
            public bool CompletionSetByContent { get; set; } // = false;
            [DisplayName("Objective Set By Content")]
            public bool ObjectiveSetByContent { get; set; } // = false

            [ScaffoldColumn(false)]
            [XmlIgnore]
            public int NodeId { get; set; }
            [ScaffoldColumn(false)]
            [XmlIgnore]
            public int CourseId { get; set; }
            [ScaffoldColumn(false)]
            [XmlIgnore]
            public string Type { get; set; }
        }
    }
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {

        public LocalizedDisplayNameAttribute(string displayNameKey)
            : base(displayNameKey)
        {
           
        }
     
        public override string DisplayName
        {
            get
            {
                return IUDICO.CourseManagement.Localization.getMessage(base.DisplayName);
            }
        }
    }
    
}