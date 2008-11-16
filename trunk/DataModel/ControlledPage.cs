using System;
using System.Web.Security;
using System.Web.UI;
using IUDICO.DataModel.Controllers;

namespace IUDICO.DataModel
{
    ///<summary>
    /// Base class for all projects' page
    ///</summary>
    public abstract class ControlledPage<ControllerType> : Page
        where ControllerType : PageControllerBase, new()
    {
        protected ControlledPage()
        {
            if (
                (User == null  || User.Identity == null || !User.Identity.IsAuthenticated) &&
                typeof(ControllerType) != typeof(LoginController))
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            Controller = new ControllerType();
        }

        protected EventHandler BindToEventHandler(Action a)
        {
            return (o, e) => a();
        }

        protected virtual void BindController(ControllerType c)
        {
        }

        protected override void LoadViewState(object savedState)
        {
            var d = (object[])savedState;
            base.LoadViewState(d[0]);
            Controller.LoadState(d[1]);
        }

        protected override object SaveViewState()
        {
            return new[]
            {
                base.SaveViewState(),
                Controller.SaveState()
            };
        }

        protected override void OnInitComplete(System.EventArgs e)
        {
            base.OnInitComplete(e);
            BindController(Controller);
        }

        protected readonly ControllerType Controller;
    }
}