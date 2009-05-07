using System;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.DB;

/// <summary>
/// Summary description for CompiledQuestionsDetails
/// </summary>
public partial class CompiledQuestionsDetails : ControlledPage<CompiledQuestionsDetailsController>
{
    private ContentPlaceHolder _placeHolder;

    protected void Page_Init(object sender, EventArgs e)
    {
        Response.AddHeader("Refresh", "5");
    }

    protected override void BindController(CompiledQuestionsDetailsController c)
    {
        base.BindController(c);
        Load += PageLoad;
        _placeHolder = (ContentPlaceHolder) Master.FindControl("MainContent");

    }
    
    public void PageLoad(object sender, EventArgs e)
    {
        if (Controller.PageId != 0)
        {
            BuildStatistic(Controller.PageId);
        }
        else
        {
            throw new Exception("Page Id is not specified");
        }
    }
    
    private void BuildStatistic(int pageId)
    {
        var page = ServerModel.DB.Load<TblPages>(pageId);
        var questions = ServerModel.DB.Load<TblQuestions>(ServerModel.DB.LookupIds<TblQuestions>(page, null));

        foreach (var question in questions)
        {
            if (question.IsCompiled)
            {
                _headerLabel.Text = string.Format("Compilation Details For Question From Page:{0}", page.PageName);
                var cqr = (CompiledQuestionResult)LoadControl("../Controls/CompiledQuestionResult.ascx");
                cqr.Question = question;
                _placeHolder.Controls.Add(cqr);
            }
        }
    }
}
