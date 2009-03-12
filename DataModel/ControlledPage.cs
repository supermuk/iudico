using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Controllers;
using LEX.CONTROLS;

namespace IUDICO.DataModel
{
    public interface IControlledPage
    {
        bool IsFirstTimeRequest { get; }
    }

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

    public abstract class ControlledPage<ControllerType> : Page, IControlledPage
        where ControllerType : ControllerBase, new()
    {
        /// <summary>
        /// Do nothing. It's required to override in derived class to bind exactly controls realy need it
        /// </summary>
        public override void DataBind()
        {
        }

        public bool IsFirstTimeRequest
        {
            get { return !IsPostBack && !IsCallback; }
        }

        protected ControlledPage()
        {
            if ((User == null || User.Identity == null || !User.Identity.IsAuthenticated) &&
                typeof(ControllerType) != typeof(LoginController))
            {
                FormsAuthentication.RedirectToLoginPage();
                HttpContext.Current.Response.End();
            }
            Controller = new ControllerType();
        }

        protected void BindChecked([NotNull] ICheckBoxControl checkBox, IValue<bool> value)
        {
            CheckBindingAllowed();
            checkBox.Checked = value.Value;
            value.Changed += (iv, v) => checkBox.Checked = v;
        }

        protected void BindChecked2Ways([NotNull] ICheckBoxControl checkBox, IVariable<bool> value)
        {
            BindChecked(checkBox, value);
            checkBox.CheckedChanged += (c, e) => value.Value = ((ICheckBoxControl)c).Checked;
        }

        protected void BindEnabled([NotNull]System.Web.UI.WebControls.WebControl c, IValue<bool> enable)
        {
            CheckBindingAllowed();
            c.Enabled = enable.Value;
            enable.Changed += (iv, v) => c.Enabled = v;
        }   

        protected void BindEnabled([NotNull]System.Web.UI.WebControls.WebControl c, bool enable)
        {
            CheckBindingAllowed();
            c.Enabled = enable;
        }

        protected void BindVisible([NotNull]System.Web.UI.WebControls.WebControl c, IValue<bool> visible)
        {
            CheckBindingAllowed();
            c.Visible = visible.Value;
            visible.Changed += (iv, v) => c.Visible = v;
        }

        protected void Bind([NotNull]ITextControl c, [NotNull] IValue<string> text)
        {
            CheckBindingAllowed();
            c.Text = text.Value;
            text.Changed += (v, newVal) => c.Text = newVal;
        }

        protected void Bind2Ways([NotNull] TextBox tb, [NotNull] IVariable<string> text)
        {
            Bind(tb, text);
            tb.TextChanged += (o, e) => text.Value = tb.Text;
        }

        protected void Bind([NotNull]ITextControl c, [CanBeNull]string text)
        {
            CheckBindingAllowed();
            c.Text = text;
        }

        protected void Bind<T>([NotNull] ITextControl c, [NotNull] IValue<T> value, [NotNull] Func<T, string> presentator)
            where T : IComparable<T>
        {
            CheckBindingAllowed();
            value.Changed += (v, newVal) => c.Text = presentator(newVal);
        }

        protected void BindTitle<T>([NotNull] IValue<T> value, [NotNull] Func<T, string> presentator)
            where T : IComparable<T>
        {
            CheckBindingAllowed();
            value.Changed += (v, newVal) => Title = presentator(newVal);
        }

        public void Bind([NotNull] IButtonControl button, Action action)
        {
            CheckBindingAllowed();
            button.Click += (o, e) => action();
        }

        protected virtual void BindController(ControllerType c)
        {
            CheckBindingAllowed();
        }

        protected override void LoadViewState(object savedState)
        {
            var p = PersistantStateMetaData.Get(typeof(ControllerType));
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
            var p = PersistantStateMetaData.Get(typeof(ControllerType));
            if (!p.IsEmpty)
            {
                r = new Pair(r, p.SaveStateFor(Controller));
            }
            return r;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack && !IsCallback)
            {
                Controller.Initialize();
            }
            Controller.Loaded();
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (!IsPostBack && !IsCallback)
            {
                DataBind();
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            ControllerParametersUtility<ControllerType>.LoadParametrs(Controller, Request.QueryString, Server);
            base.OnPreInit(e);
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            __BindingAllowed = true;
            BindController(Controller);
            __BindingAllowed = false;
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            if (Controller.RedirectUrl.IsNotNull())
            {
                Response.Redirect(Controller.RedirectUrl);
            }
            else
                base.OnPreRenderComplete(e);
        }

        protected void CheckBindingAllowed()
        {
            if (!__BindingAllowed)
            {
                throw new DMError("Binding is not allowed");
            }
        }

        protected readonly ControllerType Controller;
        private bool __BindingAllowed;
    }

    public abstract class TeacherPage<ControllerType> : ControlledPage<ControllerType>
        where ControllerType : BaseTeacherController, new()
    {
        public Label Label_PageCaption;

        public override void DataBind()
        {
            Bind(Label_PageCaption, Controller.Caption);
        }
    }
}