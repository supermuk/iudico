using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

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
