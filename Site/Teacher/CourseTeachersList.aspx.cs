using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CourseTeachersList : ControlledPage<CourseTeachersListController>
{
    protected override void BindController(CourseTeachersListController c)
    {
        base.BindController(c);

        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        Bind(Label_PageMessage, c.Message);
        BindTitle(c.Title, gn => gn);
        Bind(Label_SharedBy, c.CourseOwner);

        c.RawUrl = Request.RawUrl;
        c.CanBeSharedTeachers = Table_CanBeShared;
        c.AlreadySharedTeachers = Table_SharedWith;
    }
}
