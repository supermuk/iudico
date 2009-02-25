using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class StudentPage : ControlledPage<StudentPageController>
{
    protected override void BindController(StudentPageController c)
    {
        base.BindController(c);
        openTest.Click += c.openTestButton_Click;
        showResult.Click += c.showResultButton_Click;
        curriculumTreeView.SelectedNodeChanged += c.curriculumnTree_SelectionChanged;
        rebuildTreeButton.Click += c.rebuildTreeButton_Click;
        modeChangerButton.Click += c.modeButton_Click;
        curriculumCalendar.SelectionChanged += c.selectedDateChanged;
        Load += c.page_Load;
        c.ChangeModeButton = modeChangerButton;
        c.CurriculumnTreeView = curriculumTreeView;
        c.LastPagesResult = lastPagesResultTable;
        c.CurriculumnCalendar = curriculumCalendar;
        c.OpenTestButton = openTest;
        c.Response = Response;
    }
}
