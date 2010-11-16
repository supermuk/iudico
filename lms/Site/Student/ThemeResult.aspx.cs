using System;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.DB;
using System.Collections.Generic;

public partial class ThemeResult : ControlledPage<ThemeResultController>
{
    protected override void BindController(ThemeResultController c)
    {
        base.BindController(c);
        Load += PageLoad;
    }

    public void PageLoad(object sender, EventArgs e)
    {
        if (Controller.LearnerAttemptId == 0)
        {
            throw new Exception("Wrong request (LearnerAttempt ID not specified)");
        }

        TblLearnerAttempts learnerAttempt = ServerModel.DB.Load<TblLearnerAttempts>(Controller.LearnerAttemptId);
        IList<TblLearnerSessions> learnerSessions = ServerModel.DB.Load<TblLearnerSessions>(ServerModel.DB.LookupIds<TblLearnerSessions>(learnerAttempt, null));
        TblUsers user = ServerModel.DB.Load<TblUsers>(learnerAttempt.UserRef);
        
        TblThemes theme = ServerModel.DB.Load<TblThemes>(learnerAttempt.ThemeRef);
        TblStages stage = ServerModel.DB.Load<TblStages>(theme.StageRef);
        TblCurriculums curriculum = ServerModel.DB.Load<TblCurriculums>(stage.CurriculumRef);

        _themeResult.LearnerAttempt = learnerAttempt;
        _themeResult.LearnerSessions = learnerSessions;
        _themeResult.User = user;
        _themeResult.Theme = theme;
        _themeResult.StageName = stage.Name;
        _themeResult.CurriculumnName = curriculum.Name;

        _headerLabel.Text = string.Format("Statistic for Theme: {0}", theme.Name);
    }
}
