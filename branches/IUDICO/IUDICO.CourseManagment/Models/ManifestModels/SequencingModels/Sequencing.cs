using System;
using System.Xml.Serialization;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RuleModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RollupModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.ObjectiveModels;

namespace IUDICO.CourseManagement.Models.ManifestModels.SequencingModels
{
    [Serializable]
    public class Sequencing
    {
        public Sequencing(SequencingPattern pattern)
        {
            if (pattern == SequencingPattern.OrganizationDefaultSequencingPattern)
            {
                ControlMode = new ControlMode()
                {
                    Choise = true,
                    Flow = true
                };
            }
        }

        #region Members

        private bool InSequencingCollection;

        #endregion

        #region XmlAttributes

        [XmlAttribute(SCORM.Id)]
        public string Id;

        [XmlAttribute(SCORM.IdRef)]
        public string IdRef;

        #endregion

        #region XmlElements

        [XmlElement(SCORM.ControlMode, Namespace = SCORM.ImsssNamespace)]
        public ControlMode ControlMode;

        [XmlElement(SCORM.SequencingRules, Namespace = SCORM.ImsssNamespace)]
        public SequencingRules SequencingRules;

        [XmlElement(SCORM.LimitConditions, Namespace = SCORM.ImsssNamespace)]
        public LimitConditions LimitConditions;

        [XmlElement(SCORM.AuxiliaryResources)]
        public string AuxiliaryResources;

        [XmlElement(SCORM.RollupRules, Namespace = SCORM.ImsssNamespace)]
        public RollupRules RollupRules;

        [XmlElement(SCORM.Objectives, Namespace = SCORM.ImsssNamespace)]
        public Objectives Objectives;

        [XmlElement(SCORM.RandomizationControls, Namespace = SCORM.ImsssNamespace)]
        public RandomizationControls RandomizationControls;

        [XmlElement(SCORM.DeliveryControls, Namespace = SCORM.ImsssNamespace)]
        public DeliveryControls DeliveryControls;

        [XmlElement(SCORM.ConstrainedChoiceConsiderations, Namespace = SCORM.AdlseqNamespace)]
        public ConstrainedChoiceConsiderations ConstrainedChoiceConsiderations;

        [XmlElement(SCORM.RollupConsiderations, Namespace = SCORM.AdlseqNamespace)]
        public RollupConsiderations RollupConsiderations;

        [XmlElement(SCORM.Objectives, Namespace = SCORM.AdlseqNamespace)]
        public string AdlObjectives;

        #endregion
    }
}
