using System;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.DB;

public partial class CurriculumnResult : ControlledPage<CurriculumnResultController>
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Controller.CurriculumnId != 0)
        {
            _headerLabel.Text = "Statistic for Curriculumn";

            var content = (ContentPlaceHolder)Master.FindControl("MainContent");

            var curriculumn = ServerModel.DB.Load<TblCurriculums>(Controller.CurriculumnId);

            var stages = ServerModel.DB.Load<TblStages>(ServerModel.DB.LookupIds<TblStages>(curriculumn, null));

            foreach (var stage in stages)
            {
                var themesId = ServerModel.DB.LookupMany2ManyIds<TblThemes>(stage, null);

                foreach (var i in themesId)
                {
                    var t = (ThemeResultControl) LoadControl("../Controls/ThemeResultControl.ascx");
                    t.ThemeId = i;
                    t.StageName = stage.Name;
                    t.CurriculumnName = curriculumn.Name;

                    content.Controls.Add(t);
                }
            }
        }
        else
        {
            throw new Exception("StageId is not specified");
        }
    }
}
