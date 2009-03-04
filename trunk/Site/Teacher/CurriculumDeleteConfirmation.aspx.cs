using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CurriculumDeleteConfirmation : ControlledPage<CurriculumDeleteConfirmationController>
{
    protected override void BindController(CurriculumDeleteConfirmationController c)
    {
        base.BindController(c);

        Title = "urriculum delete confirmation";

        c.GroupsBulletedList = BulletedList_Groups;
        c.DeleteButton = Button_Delete;
        c.NotifyLabel = Label_Notify;

        Button_Back.PostBackUrl = c.BackUrl;
        Load += c.PageLoad;
    }
}
