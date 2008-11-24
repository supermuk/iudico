using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Controllers;

namespace IUDICO.DataModel
{
    ///<summary>
    /// Base class for all projects' page
    ///</summary>
    /// 
    public abstract class ControlledPage : ControlledPage<DefaultController>
    {
        protected override void LoadViewState(object savedState)
        {
            var p = PersistantStateMetaData.Get(GetType());
            if (p.IsEmpty)
            {
                base.LoadViewState(savedState);
            }
            else
            {
                var d = (Pair)savedState;
                base.LoadViewState(d.First);
                p.LoadStateFor(this, d.Second);
            }
        }

        protected override object SaveViewState()
        {
            var r = base.SaveControlState();
            var p = PersistantStateMetaData.Get(GetType());
            if (!p.IsEmpty)
            {
                r = new Pair(r, p.SaveStateFor(this));
            }
            return r;
        }
    }

    public abstract class ControlledPage<ControllerType> : Page
        where ControllerType : ControllerBase, new()
    {
        protected ControlledPage()
        {
            if ((User == null  || User.Identity == null || !User.Identity.IsAuthenticated) &&
                typeof(ControllerType) != typeof(LoginController))
            {
                FormsAuthentication.RedirectToLoginPage();
                HttpContext.Current.Response.End();
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
            var p = PersistantStateMetaData.Get(typeof (ControllerType));
            if (p.IsEmpty)
            {
                base.LoadViewState(savedState);
            }
            else
            {
                var d = (Pair)savedState;
                base.LoadViewState(d.First);
                p.LoadStateFor(Controller, d.Second);
            }
        }

        protected override object SaveViewState()
        {
            var r = base.SaveViewState();
            var p = PersistantStateMetaData.Get(typeof (ControllerType));
            if (!p.IsEmpty)
            {
                r = new Pair(r, p.SaveStateFor(Controller));
            }
            return r;
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            BindController(Controller);
        }

        /// <summary>
        /// Do nothing. It's required to override in derived class to bind exactly controls realy need it
        /// </summary>
        public override void DataBind()
        {
        }

        protected readonly ControllerType Controller;
    }
}