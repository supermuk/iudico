using System;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers.Student;

public partial class ThemeResult : ControlledPage<ThemeResultController>
{
    protected override void BindController(ThemeResultController c)
    {
        base.BindController(c);
        Load += PageLoad;
    }

    public void PageLoad(object sender, EventArgs e)
    {
        if (Controller.ThemeId != 0)
        {
            _headerLabel.Text = "Statistic for Theme";
            _themeResult.ThemeId = Controller.ThemeId;
            _themeResult.CurriculumnName = Controller.CurriculumnName;
            _themeResult.StageName = Controller.StageName;
        }
        else
        {
            throw new Exception("ThemeId is not specified");
        }
    }
}
