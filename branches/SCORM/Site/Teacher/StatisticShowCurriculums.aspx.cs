using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.Controllers.Teacher;

public partial class Teacher_StatisticShowCurriculums : ControlledPage<StatisticShowCurriculumsController>
{
    protected override void BindController(StatisticShowCurriculumsController c)
    {
        
        base.BindController(c);

        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        BindTitle(c.Title, gn => gn);

        c.StatisticTable = TableCurriculums;
    }
}

