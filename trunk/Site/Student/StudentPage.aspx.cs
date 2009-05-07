using IUDICO.DataModel;
using IUDICO.DataModel.Controllers.Student;

public partial class StudentPage : ControlledPage<StudentPageController>
{
    protected override void BindController(StudentPageController c)
    {
        base.BindController(c);

        _openTest.Click += c.OpenTestButtonClick;
        _showResult.Click += c.ShowResultButtonClick;
        _curriculumTreeView.SelectedNodeChanged += c.CurriculumnTreeSelectionChanged;
        _rebuildTreeButton.Click += c.RebuildTreeButtonClick;
        _modeChangerButton.Click += c.ModeButtonClick;
        _curriculumCalendar.SelectionChanged += c.SelectedDateChanged;
        Load += c.PageLoad;

        c.ChangeModeButton = _modeChangerButton;
        c.CurriculumnTreeView = _curriculumTreeView;
        c.LastPagesResultTable = _lastPagesResultTable;
        c.CurriculumnCalendar = _curriculumCalendar;
        c.PeriodDescription = _periodDescription;
        c.OpenTestButton = _openTest;

        Bind(_headerLabel, c.UserName, un => string.Format("Student Page For: {0}", un));
        Bind(_descriptionLabel, c.Description);
    }
}
