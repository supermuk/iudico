using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CurriculumTimeline : ControlledPage<CurriculumTimelineController>
{
    protected override void BindController(CurriculumTimelineController c)
    {
        base.BindController(c);

        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        Bind(Label_PageMessage, c.Message);
        Bind(OperationsTable_Operations, c.TimeLineData);
        BindTitle(c.Title, gn => gn);

        c.IsPostBack = IsPostBack;
        c.CurriculumTree = TreeView_Curriculum;
    }
}
