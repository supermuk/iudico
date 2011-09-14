using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.Controllers.Teacher;

public partial class Teacher_StatisticShowCurriculums : ControlledPage<StatisticShowCurriculumsController>
{
    protected override void BindController(StatisticShowCurriculumsController c)
    {

        base.BindController(c);
        Bind2Ways(TextBox_FindStud, c.Find_StudName);
        Bind(Button_FindStud, c.Button_FindStud_Click);
        Bind(Button_Sort, c.Button_Sort_Click);
        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        BindTitle(c.Title, gn => gn);

        c.StatisticTable = TableCurriculums;
    }
}

