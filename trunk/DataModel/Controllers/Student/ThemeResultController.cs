namespace IUDICO.DataModel.Controllers.Student
{
    public class ThemeResultController : ControllerBase
    {
        [ControllerParameter] public string CurriculumnName;

        [ControllerParameter] public string StageName;
        [ControllerParameter] public int ThemeId;
    }
}