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
        if (Controller.LearnerSessionId == 0)
        {
            throw new Exception("Wrong request (LearnerSession ID not specified)");
        }

        TblLearnerSessions learnerSession = ServerModel.DB.Load<TblLearnerSessions>(Controller.LearnerSessionId);
        TblLearnerAttempts learnerAttempt = ServerModel.DB.Load<TblLearnerAttempts>(learnerSession.LearnerAttemptRef);
        TblThemes theme = ServerModel.DB.Load<TblThemes>(learnerAttempt.ThemeRef);
        TblStages stage = ServerModel.DB.Load<TblStages>(theme.StageRef);
        TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(stage.CurriculumRef);

        _themeResult.LearnerSessionId = Controller.LearnerSessionId;
        _themeResult.UserId = learnerAttempt.UserRef;
        _themeResult.StageName = stage.Name;
        _themeResult.CurriculumnName = curriculum.Name;

        _headerLabel.Text = string.Format("Statistic for Theme: {0}", theme.Name);
    }
}
