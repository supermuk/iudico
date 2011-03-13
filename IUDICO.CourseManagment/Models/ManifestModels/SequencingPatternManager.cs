using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
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
                                      Choise = true,
                                      Flow = true
                                  };
            return seq;
        }

        private static Sequencing ControlChapterSequencing()
        {
            var seq = new Sequencing();
            seq.ControlMode = new ControlMode()
                                  {
                                      Choise = false,
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

    }
}