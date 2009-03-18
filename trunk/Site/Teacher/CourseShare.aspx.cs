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

        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        Bind(Label_PageMessage, c.Message);
        BindTitle(c.Title, gn => gn);
        Bind(Button_Update, c.UpdateButton_Click);
        c.Operations = Table_Operations;
    }
}
