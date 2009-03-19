namespace IUDICO.DataModel.Controllers.Student
{
    public class StageResultController : ControllerBase
    {
        [ControllerParameter] public string CurriculumnName;
        [ControllerParameter] public int StageId;
    }
}