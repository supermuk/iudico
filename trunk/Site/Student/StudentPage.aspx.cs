using IUDICO.DataModel;
using IUDICO.DataModel.Controllers.Student;

public partial class StudentPage : ControlledPage<StudentPageController>
{
    protected override void BindController(StudentPageController c)
    {
        base.BindController(c);

        openTest.Click += c.OpenTestButtonClick;
        showResult.Click += c.ShowResultButtonClick;
        curriculumTreeView.SelectedNodeChanged += c.CurriculumnTreeSelectionChanged;
        rebuildTreeButton.Click += c.RebuildTreeButtonClick;
        modeChangerButton.Click += c.ModeButtonClick;
        curriculumCalendar.SelectionChanged += c.SelectedDateChanged;
        Load += c.PageLoad;

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
