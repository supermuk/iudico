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
        Bind(OperationsTable, c.PermissionID);
        BindTitle(c.Title, gn => gn);
        
        c.CurriculumTree = TreeView_Curriculum;
    }

    public override void DataBind()
    {
        base.DataBind();
    }
}
