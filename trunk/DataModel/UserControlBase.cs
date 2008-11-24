using System.Web.UI;
using IUDICO.DataModel.Controllers;

namespace IUDICO.DataModel
{
    public class ControlledUserControl<TController> : UserControl
        where TController : ControllerBase, new()
    {
        protected TController Controller = new TController();

        protected virtual void BindController(TController c)
        {
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            BindController(Controller);
        }
    }
}
