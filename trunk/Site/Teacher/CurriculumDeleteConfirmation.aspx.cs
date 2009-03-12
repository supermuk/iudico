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

        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        Bind(Label_PageMessage, c.Message);
        BindTitle(c.Title, gn => gn);
        Bind(Button_Delete, c.DeleteButton_Click);

        c.GroupsBulletedList = BulletedList_Groups;

        Button_Back.PostBackUrl = c.BackUrl;
    }

    public override void DataBind()
    {
        base.DataBind();

        BulletedList_Groups.DataSource = Controller.GetGroups();
        BulletedList_Groups.DataBind();
    }
}
