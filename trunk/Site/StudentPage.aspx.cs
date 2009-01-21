using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class StudentPage : ControlledPage<StudentPageController>
{
    protected override void BindController(StudentPageController c)
    {
        base.BindController(c);
        openTest.Click += c.openTestButton_Click;
        showResult.Click += c.showResultButton_Click;
        Load += c.page_Load;
        c.CurriculumnTreeView = curriculumTreeView;
        c.Response = Response;
    }
}
