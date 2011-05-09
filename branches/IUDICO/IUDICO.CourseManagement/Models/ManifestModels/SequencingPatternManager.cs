using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;

namespace IUDICO.CourseManagement.Models.ManifestModels
{
    public class SequencingPatternManager
    {
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