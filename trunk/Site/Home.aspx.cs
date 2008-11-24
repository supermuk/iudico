using System;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

namespace IUDICO.Site.Pages
{
    public partial class Home : ControlledPage<HomeController>
    {
        protected override void BindController(HomeController c)
        {
            base.BindController(c);
            Button1.Click += BindToEventHandler(c.Test1);
            Button2.Click += BindToEventHandler(c.Test2);
            Button3.Click += BindToEventHandler(c.Test3);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UserPermissions.UserID = 1;
            if (!IsPostBack && !IsCallback)
            {
                DataBind();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Button3.Text = Controller.SomeControllerValue.ToString();
        }

        public override void DataBind()
        {
            UserPermissions.DataBind();
        }
    }
}

