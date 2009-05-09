using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    public class BaseTeacherController : ControllerBase
    {
        public IVariable<string> Caption = string.Empty.AsVariable();
        public IVariable<string> Description = string.Empty.AsVariable();
        public IVariable<string> Message = "Default message".AsVariable();
        public IVariable<string> Title = string.Empty.AsVariable();
        public string RawUrl = "";
        public bool IsPostBack = false;

        public override void Loaded()
        {
            base.Loaded();

            Message.Value = "";
        }
    }


}
