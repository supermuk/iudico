namespace IUDICO.DataModel.Controllers.Student
{
    public class CurriculumnResultController : ControllerBase
    {
        [ControllerParameter] public int CurriculumnId;

        [ControllerParameter] public int UserId;
    }
}