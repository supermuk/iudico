using System;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;

/// <summary>
/// Summary description for CompiledQuestionsDetails
/// </summary>
public partial class CompiledQuestionsDetails : ControlledPage<CompiledQuestionsDetailsController>
{
    private ContentPlaceHolder placeHolder;

    protected override void BindController(CompiledQuestionsDetailsController c)
    {
        base.BindController(c);
        Load += PageLoad;
        placeHolder = (ContentPlaceHolder) Master.FindControl("MainContent");

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
                headerLabel.Text = string.Format("Compilation Details For Question From Page:{0}", page.PageName);
                var cqr = (CompiledQuestionResult)LoadControl("../Controls/CompiledQuestionResult.ascx");
                cqr.Question = question;
                placeHolder.Controls.Add(cqr);
            }
        }
    }
}
