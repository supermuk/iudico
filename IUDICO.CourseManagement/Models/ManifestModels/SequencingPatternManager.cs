using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;

namespace IUDICO.CourseManagement.Models.ManifestModels
{
    public class SequencingPatternManager
    {
        public static Sequencing ApplyDefaultChapterSequencing(Sequencing sequencing)
        {
            sequencing.ControlMode = new ControlMode();
            sequencing.LimitConditions = null;
            sequencing.RandomizationControls = null;

            sequencing.ControlMode.Choice = true;
            sequencing.ControlMode.Flow = true;

            return sequencing;
        }

        public static Sequencing ApplyControlChapterSequencing(Sequencing sequencing)
        {
            sequencing.ControlMode = new ControlMode();
            sequencing.LimitConditions = new LimitConditions();
            sequencing.RandomizationControls = null;

            sequencing.ControlMode.Choice = false;
            sequencing.ControlMode.Flow = true;
            sequencing.ControlMode.ForwardOnly = true;
            sequencing.ControlMode.ChoiceExit = false;


            sequencing.LimitConditions.AttemptLimit = 1;

            return sequencing;
        }

        public static Sequencing ApplyRandomSetSequencingPattern(Sequencing sequencing, int selectCount)
        {
            sequencing.RandomizationControls = new RandomizationControls();
            sequencing.ControlMode = new ControlMode();
            sequencing.LimitConditions = null;

            sequencing.RandomizationControls.ReorderChildren = true;
            sequencing.RandomizationControls.SelectionTiming = Timing.Once;
            sequencing.RandomizationControls.SelectCount = selectCount;

            return sequencing;
        }
    }
}