using System;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.DB;

public partial class StageResult : ControlledPage<StageResultController>
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Controller.StageId != 0)
        {
            _headerLabel.Text = "Statistic for Stage";

            var content = (ContentPlaceHolder)Master.FindControl("MainContent");

            var stage = ServerModel.DB.Load<TblStages>(Controller.StageId);

            var themesId = ServerModel.DB.LookupMany2ManyIds<TblThemes>(stage, null);
            
            foreach (var i in themesId)
            {
                var t = (ThemeResultControl)LoadControl("../Controls/ThemeResultControl.ascx");
                t.ThemeId = i;
                t.StageName = stage.Name;
                t.CurriculumnName = Controller.CurriculumnName;

                content.Controls.Add(t);
            }
        }
        else
        {
            throw new Exception("StageId is not specified");
        }
    }
}
