using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CourseShare : ControlledPage<CourseShareController>
{
    protected override void BindController(CourseShareController c)
    {
        base.BindController(c);

        Title = "Course share";

        c.OperationsTable = Table_Operations;
        c.TeachersTable = Table_Teachers;
        c.NotifyLabel = Label_Notify;

        Load += c.PageLoad;
    }
}
