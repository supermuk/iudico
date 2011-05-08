﻿using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;

namespace IUDICO.CourseManagement.Models.ManifestModels
{
    public class SequencingPatternManager
    {
        public static Organization ApplyPattern(Manifest manifest, Organization organization, SequencingPattern pattern)
        {
            organization.Sequencing = new Sequencing();
            switch (pattern)
            {
                case SequencingPattern.OrganizationDefaultSequencingPattern:
                    organization = ApplyOrganizationDefaultSequencingPattern(organization);
                    break;
                case SequencingPattern.ControlChapterSequencingPattern:
                    organization = ApplyControlChapterSequencingPattern(organization);
                    break;
            }
            return organization;
        }

        private static Organization ApplyOrganizationDefaultSequencingPattern(Organization organization)
        {
            organization.Sequencing = DefaultSequencing();
            return organization;
        }

        private static Organization ApplyControlChapterSequencingPattern(Organization organization)
        {
            organization.Sequencing = ControlChapterSequencing();
            return organization;
        }

        private static Organization ApplyForcedSequentialOrderSequencingPattern(Organization organization)
        {
            organization.ObjectivesGlobalToSystem = false;
            return organization;
        }

        private static Sequencing DefaultSequencing()
        {
            var seq = new Sequencing();
            seq.ControlMode = new ControlMode()
                                  {
                                      Choice = true,
                                      Flow = true
                                  };
            return seq;
        }

        private static Sequencing ControlChapterSequencing()
        {
            var seq = new Sequencing();
            seq.ControlMode = new ControlMode()
                                  {
                                      Choice = false,
                                      Flow = true,
                                      ForwardOnly = true,
                                      ChoiceExit = false
                                  };
            seq.LimitConditions = new LimitConditions()
                                      {
                                          AttemptLimit = 1
                                      };
            return seq;
        }

        public static Sequencing ApplyControlChapterSequncing(Sequencing sequencing)
        {
            if(sequencing.ControlMode == null)
            {
                sequencing.ControlMode = new ControlMode();
            }
            sequencing.ControlMode.Choice = false;
            sequencing.ControlMode.Flow = true;
            sequencing.ControlMode.ForwardOnly = true;
            sequencing.ControlMode.ChoiceExit = false;


            if(sequencing.LimitConditions == null)
            {
                sequencing.LimitConditions = new LimitConditions();
            }
            sequencing.LimitConditions.AttemptLimit = 1;

            return sequencing;
        }
    }
}