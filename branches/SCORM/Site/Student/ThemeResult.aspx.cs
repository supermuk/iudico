using System;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.DB;

public partial class ThemeResult : ControlledPage<ThemeResultController>
{
    protected override void BindController(ThemeResultController c)
    {
        base.BindController(c);
        Load += PageLoad;
    }

    public void PageLoad(object sender, EventArgs e)
    {
        if (Controller.ThemeId == 0)
        {
            throw new Exception("Wrong request (Theme ID not specified)");
        }

        if (Controller.UserId == 0)
        {
            throw new Exception("Wrong request (User ID not specified)");
        }

        TblThemes theme = ServerModel.DB.Load<TblThemes>(Controller.ThemeId);
        TblStages stage = ServerModel.DB.Load<TblStages>(theme.StageRef);
        TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(stage.CurriculumRef);

        _themeResult.ThemeId = Controller.ThemeId;
        _themeResult.UserId = Controller.UserId;
        _themeResult.StageName = stage.Name;
        _themeResult.CurriculumnName = curriculum.Name;

        _headerLabel.Text = string.Format("Statistic for Theme: {0}", theme.Name);
    }
}
