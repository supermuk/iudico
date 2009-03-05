using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class StatisticShow : ControlledPage<StatisticShowController>
{
    protected override void BindController(StatisticShowController c)
    {
        base.BindController(c);

        Title = "Statistic page";
        c.NotifyLabel = Label_Notify;
        c.StatisticTable = Table_Statistic;

        Load += c.PageLoad;
    }
}
