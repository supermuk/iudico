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
    private const string pageIdRequestParameter = "pageId";
    private ContentPlaceHolder placeHolder;

    protected override void BindController(CompiledQuestionsDetailsController c)
    {
        base.BindController(c);
        Load += pageLoad;
        placeHolder = (ContentPlaceHolder) Master.FindControl("MainContent");

    }
    public void pageLoad(object sender, EventArgs e)
    {
        if (Request[pageIdRequestParameter] != null)
        {
            buildStatistic(int.Parse(Request[pageIdRequestParameter]));
        }
        else
        {
            throw new Exception("Page Id is not specified");
        }
    }
    private void buildStatistic(int pageId)
    {
        var page = ServerModel.DB.Load<TblPages>(pageId);
        var questions = ServerModel.DB.Load<TblQuestions>(ServerModel.DB.LookupIds<TblQuestions>(page, null));
        pageNameLabel.Text = page.PageName;

        foreach (var question in questions)
        {
            if (question.IsCompiled)
            {
                var cqr = (CompiledQuestionResult)LoadControl("../Controls/CompiledQuestionResult.ascx");
                cqr.Question = question;
                placeHolder.Controls.Add(cqr);
            }
        }
    }
}
