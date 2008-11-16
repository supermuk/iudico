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
        }
    }
}

