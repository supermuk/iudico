using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using System.Web.UI.WebControls;

public partial class StatisticSelect : ControlledPage<StatisticSelectController>
{
    protected override void BindController(StatisticSelectController c)
    {
        base.BindController(c);

        Bind(Button_Show, c.ShowButton_Click);
        BindEnabled(Button_Show, c.ShowButtonEnabled);

        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        Bind(Label_PageMessage, c.Message);
        BindTitle(c.Title, gn => gn);

        c.GroupsDropDownList = DropDownList_Groups;
        c.CurriculumsCheckBoxList = CheckBoxCurriculums;
        c.RawUrl = Request.RawUrl;
        c.IsPostBack = IsPostBack;
       
       
    }

    protected void CheckBoxCurriculums_SelectedIndexChanged(object sender, System.EventArgs e)
    {

    }
}
