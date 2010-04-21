namespace IUDICO.DataModel.Controllers.Student
{
    /// <summary>
    /// Controller for CompiledQuestionsDetails.aspx page 
    /// </summary>
    public class CompiledQuestionsDetailsController : ControllerBase
    {
        [ControllerParameter] public int PageId;

        [ControllerParameter] public int UserId;
    }
}