using IUDICO.DataModel;
using IUDICO.DataModel.Controllers.Student;

public partial class StudentPage : ControlledPage<StudentPageController>
{
    protected override void BindController(StudentPageController c)
    {
        base.BindController(c);

        openTest.Click += c.OpenTestButton_Click;
        showResult.Click += c.ShowResultButton_Click;
        curriculumTreeView.SelectedNodeChanged += c.CurriculumnTree_SelectionChanged;
        rebuildTreeButton.Click += c.RebuildTreeButton_Click;
        modeChangerButton.Click += c.ModeButton_Click;
        curriculumCalendar.SelectionChanged += c.SelectedDateChanged;
        Load += c.Page_Load;

        c.ChangeModeButton = modeChangerButton;
        c.CurriculumnTreeView = curriculumTreeView;
        c.LastPagesResultTable = lastPagesResultTable;
        c.CurriculumnCalendar = curriculumCalendar;
        c.PeriodDescription = periodDescription;
        c.OpenTestButton = openTest;

        Bind(headerLabel, c.UserName, un => string.Format("Student Page For: {0}", un));
        Bind(descriptionLabel, c.Description);
    }
}
