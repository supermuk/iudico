namespace IUDICO.DataModel.Controllers.Student
{
    /// <summary>
    /// Controller for CurriculumnResult.aspx page
    /// </summary>
    public class CurriculumnResultController : ControllerBase
    {
        [ControllerParameter] public int CurriculumnId;

        [ControllerParameter] public int UserId;
    }
}