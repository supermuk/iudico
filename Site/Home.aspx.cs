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
            TestPersistedListButton.Click += BindToEventHandler(c.TestPersistedList);

            Bind(PersistedListLabel, c.PersistedCollection);
            Bind(lbIncrement, c.PersistedInt, i => i.ToString());
        }

        protected override void OnLoad(EventArgs e)
        {
            UserPermissions.UserID = ServerModel.User.Current.ID;
            base.OnLoad(e);
        }

        public override void DataBind()
        {
            UserPermissions.DataBind();
        }
    }
}

