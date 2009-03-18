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

        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        Bind(Label_PageMessage, c.Message);
        BindTitle(c.Title, gn => gn);

        c.CurriculumsTable = Table_Curriculums;
        c.CoursesTable = Table_Courses;
    }
}
