using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel;

public partial class TeacherObjects : ControlledPage<TeacherObjectsController>
{
    protected override void BindController(TeacherObjectsController c)
    {
        base.BindController(c);

        Title = "Teacher objects";

        c.CurriculumsTable = Table_Curriculums;
        c.CoursesTable = Table_Courses;
        c.NotifyLabel = Label_Notify;

        Load += c.PageLoad;
    }
}
