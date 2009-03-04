using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CurriculumTimeline : ControlledPage<CurriculumTimelineController>
{
    protected override void BindController(CurriculumTimelineController c)
    {
        base.BindController(c);

        c.CurriculumTree = TreeView_Curriculum;
        c.TimeTable = Table_Time;
        c.NotifyLabel = Label_Notify;
        c.OperationDropDownList = DropDownList_Operations;
        c.AddOperationButton = Button_Add;

        Title = "Detailed timeline settings";

        PreRender += c.PagePreRender;
        Load += c.PageLoad;
    }
}
