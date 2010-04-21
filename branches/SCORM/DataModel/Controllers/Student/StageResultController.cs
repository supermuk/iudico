namespace IUDICO.DataModel.Controllers.Student
{
    /// <summary>
    /// Controller for StageResult.aspx page
    /// </summary>
    public class StageResultController : ControllerBase
    {
        [ControllerParameter] public string CurriculumnName;

        [ControllerParameter] public int StageId;

        [ControllerParameter] public int UserId;
    }
}