using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CourseDeleteConfirmation : ControlledPage<CourseDeleteConfirmationController>
{
    protected override void BindController(CourseDeleteConfirmationController c)
    {
        base.BindController(c);

        Title = "Course delete confirmation";

        c.DependenciesGridView = GridView_Dependencies;
        c.DeleteButton = Button_Delete;
        c.NotifyLabel = Label_Notify;

        Button_Back.PostBackUrl = c.BackUrl;
        Load += c.PageLoad;
    }
}
