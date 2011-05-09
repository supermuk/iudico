using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;

namespace IUDICO.CourseManagement.Models.ManifestModels
{
    public class SequencingPatternManager
    {
        public static Sequencing ApplyDefaultChapterSequencing(Sequencing sequencing)
        {
            if (sequencing.ControlMode == null)
            {
                sequencing.ControlMode = new ControlMode();
            }
            sequencing.ControlMode.Choice = true;
            sequencing.ControlMode.Flow = true;

            return sequencing;
        }

        public static Sequencing ApplyControlChapterSequencing(Sequencing sequencing)
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

        public static Sequencing ApplyRandomSetSequencingPattern(Sequencing sequencing, int selectCount)
        {
            if (sequencing.RandomizationControls == null)
            {
                sequencing.RandomizationControls = new RandomizationControls();
            }
            sequencing.RandomizationControls.ReorderChildren = true;
            sequencing.RandomizationControls.SelectionTiming = Timing.Once;
            sequencing.RandomizationControls.SelectCount = selectCount;

            return sequencing;
        }
    }
}