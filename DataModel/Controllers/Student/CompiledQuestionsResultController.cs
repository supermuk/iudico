namespace IUDICO.DataModel.Controllers.Student
{
    public class CompiledQuestionsDetailsController : ControllerBase
    {
        [ControllerParameter] public int PageId;

        [ControllerParameter] public int UserId;
    }
}