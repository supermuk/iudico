using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class Teacher_CurriculumAssignment : ControlledPage<CurriculumAssignmentController>
{
    protected override void BindController(CurriculumAssignmentController c)
    {
        base.BindController(c);

        Title = "Curriculum assignment";

        c.AssigmentsTable = Table_Assignments;
        c.MainTable = Table_Main;
        c.NotifyLabel = Label_Notify;

        Load += c.PageLoad;
    }
}
