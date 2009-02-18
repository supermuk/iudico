using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CurriculumTimeline : ControlledPage<CurriculumTimelineController>
{
    protected override void BindController(CurriculumTimelineController c)
    {
        base.BindController(c);

        c.CurriculumTree = TreeView_Curriculum;
        c.NotifyLabel = Label_Notify;

        c.RemoveButton = Button_Remove;
        c.GrantButton = Button_Grant;

        Load += c.PageLoad;
    }
}
