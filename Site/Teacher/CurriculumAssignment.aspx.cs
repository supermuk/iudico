using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;
using System.Web.UI.WebControls;

public partial class CurriculumAssignment : ControlledPage<CurriculumAssignmentController>
{
    protected override void BindController(CurriculumAssignmentController c)
    {
        base.BindController(c);

        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        Bind(Label_PageMessage, c.Message);
        BindTitle(c.Title, gn => gn);
        BindVisible(Table_Main, c.MainTableVisible);
        Bind(Button_AddGroup, c.AddGroup);
        
        Bind(AssignmentTable,c.VisibleGroupID);
        c.GroupList = GroupList;
    }
    public override void DataBind()
    {
        base.DataBind();

        foreach (TblGroups group in ServerModel.DB.Query<TblGroups>(null))
        {
            GroupList.Items.Add(new ListItem(group.Name, group.ID.ToString()));
        }
    }
}
