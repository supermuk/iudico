using System;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using System.Web.Security;

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
            DataBind();
        }

        public override void DataBind()
        {
            UserPermissions.DataBind();
        }
    }
}

