using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CurriculumTimeline : ControlledPage<CurriculumTimelineController>
{
    protected override void BindController(CurriculumTimelineController c)
    {
        base.BindController(c);

        c.CurriculumTree = TreeView_Curriculum;

        c.DateSinceTextBox = TextBox_DateSince;
        c.DateTillTextBox = TextBox_DateTill;
        c.TimeSinceTextBox = TextBox_TimeSince;
        c.TimeTillTextBox = TextBox_TimeTill;

        c.GrantButton = Button_Grant;

        c.OperationList = DropDownList_Operation;


        Load += c.PageLoad;
    }
}
