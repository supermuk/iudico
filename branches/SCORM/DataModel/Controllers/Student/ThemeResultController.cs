namespace IUDICO.DataModel.Controllers.Student
{
    public class ThemeResultController : ControllerBase
    {
        [ControllerParameter] public int ThemeId;

        [ControllerParameter] public int UserId;
    }
}