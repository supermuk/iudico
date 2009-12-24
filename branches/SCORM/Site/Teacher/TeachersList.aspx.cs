using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class TeachersList : ControlledPage<TeachersListController>
{
    protected override void BindController(TeachersListController c)
    {
        base.BindController(c);

        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        Bind(Label_PageMessage, c.Message);
        BindTitle(c.Title, gn => gn);
        Bind(Label_SharedBy, c.ObjectOwner);

        c.RawUrl = Request.RawUrl;
        c.CanBeSharedTeachers = Table_CanBeShared;
        c.AlreadySharedTeachers = Table_SharedWith;
    }
}
