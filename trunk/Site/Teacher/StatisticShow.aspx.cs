using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class StatisticShow : ControlledPage<StatisticShowController>
{
    protected override void BindController(StatisticShowController c)
    {
        base.BindController(c);

        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        BindTitle(c.Title, gn => gn);

        c.StatisticTable = Table_Statistic;
    }
}
