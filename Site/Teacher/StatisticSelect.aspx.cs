using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel;

public partial class StatisticSelect : ControlledPage<StatisticSelectController>
{
    protected override void BindController(StatisticSelectController c)
    {
        base.BindController(c);

        Title = "Statistic selection";

        c.CurriculumsDropDownList = DropDownList_Curriculums;
        c.GroupsDropDownList = DropDownList_Groups;
        c.ShowButton = Button_Show;
        c.NotifyLabel = Label_Notify;

        Load += c.PageLoad;
    }
}
