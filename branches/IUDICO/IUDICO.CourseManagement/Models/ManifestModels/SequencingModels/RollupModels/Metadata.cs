using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Serialization;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RollupModels
{
    [MetadataType(typeof(Metadata))]
    public partial class RollupRules : NodeProperty
    {

        private sealed class Metadata
        {
            [DisplayName("Rollup Objective Satisfied")]
            public bool RollupObjectiveSatisfied { get; set; }// = true;
            [DisplayName("Rollup Objective Satisfied")]
            public bool RollupProgressCompletion { get; set; } // = true;
            [DisplayName("Objective Measure Weight")]
            public float ObjectiveMeasureWeight { get; set; }
            [DisplayName("Rollup rules")]
            [UIHint("PropertiesList")]
            public IEnumerable<NodeProperty> _RollupRules { get; set; }

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
    public partial class RollupRule : NodeProperty
    {
        public IEnumerable<SelectListItem> ChildActivitySetList
        {
            get
            {
                var list = new List<SelectListItem>
                               {
                                   new SelectListItem
                                       {
                                           Text = Enum.GetName(typeof (ChildActivitySet), ChildActivitySet.All),
                                           Value = ChildActivitySet.All.ToString()
                                       },
                                   new SelectListItem
                                       {
                                           Text = Enum.GetName(typeof (ChildActivitySet), ChildActivitySet.Any),
                                           Value = ChildActivitySet.Any.ToString()
                                       },
                                   new SelectListItem
                                       {
                                           Text = Enum.GetName(typeof (ChildActivitySet), ChildActivitySet.AtLeastCount),
                                           Value = ChildActivitySet.AtLeastCount.ToString()
                                       },
                                   new SelectListItem
                                       {
                                           Text =
                                               Enum.GetName(typeof (ChildActivitySet), ChildActivitySet.AtLeastPercent),
                                           Value = ChildActivitySet.AtLeastPercent.ToString()
                                       },
                                   new SelectListItem
                                       {
                                           Text = Enum.GetName(typeof (ChildActivitySet), ChildActivitySet.None),
                                           Value = ChildActivitySet.None.ToString()
                                       }
                               };

                return list.AsEnumerable();
            }
        }

        private sealed class Metadata
        {
            [DisplayName("Child Activity Set")]
            [DropDownList(SourceProperty = "ChildActivitySetList")]
            public ChildActivitySet ChildActivitySet { get; set; }
            [DisplayName("Minimum")]
            public int MinimumCount { get; set; } // = 0;
            [DisplayName("Maximum")]
            public double MinimumPercent { get; set; } // = 0.0000;

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
}