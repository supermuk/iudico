using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;
using IUDICO.DataModel.Security;
using TestingSystem;

public partial class ThemeResultControl : System.Web.UI.UserControl
{
    private const string pageDetailsUrl = "../Student/TestDetails.aspx?pageId={0}";

    private const string compiledDetailsUrl = "../Student/CompiledQuestionsDetails.aspx?pageId={0}";

    private static bool isContainCompiledQuestions;

    public int ThemeId { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        var theme = ServerModel.DB.Load<TblThemes>(ThemeId);
        themeName.Text = theme.Name;


        var userId = ((CustomUser) Membership.GetUser()).ID;
        var pages = ServerModel.DB.Load <TblPages>(ServerModel.DB.LookupIds<TblPages>(theme, null));

        foreach (var page in pages)
        {
            isContainCompiledQuestions = false;
            
            if (page.PageTypeRef == (int)FX_PAGETYPE.Practice)
            {
                var pageNameCell = new TableCell();
                var passStatusCell = new TableCell();
                var pageRankCell = new TableCell();
                var userRankCell = new TableCell();
                var pageDetails = new TableCell();

                SetPageDetailsLink(pageDetails, page.ID);
                SetPageNameAndRank(page, pageNameCell, pageRankCell);
                SetPageResult(page, userId, passStatusCell, userRankCell);

                var row = new TableRow();
                row.Cells.AddRange(new []{pageNameCell, passStatusCell, userRankCell, pageRankCell, pageDetails});
                if (isContainCompiledQuestions)
                {
                    var compileDetails = new TableCell();
                    SetCompiledDetailsLink(compileDetails, page.ID);
                    row.Cells.Add(compileDetails);
                }
                resultTable.Rows.Add(row);

            }
        }
        
    }

    private static int CalculateUserRank(TblQuestions question, int userId)
    {
        int userRank = 0;

        var userAnswers = ServerModel.DB.Load<TblUserAnswers>(ServerModel.DB.LookupIds<TblUserAnswers>(question, null));

        if (userAnswers != null)
        {
            var currUserAnswers = FindUserAnswers(userAnswers, userId);
            if (currUserAnswers.Count != 0)
            {
                var latestUserAnswer = FindLatestUserAnswer(currUserAnswers);

                if (latestUserAnswer.UserAnswer == question.CorrectAnswer)
                {
                    userRank += (int) question.Rank;
                }
                else if (latestUserAnswer.IsCompiledAnswer)
                {
                    var userCompiledAnswers = ServerModel.DB.Load<TblCompiledAnswers>(ServerModel.DB.LookupIds<TblCompiledAnswers>(latestUserAnswer, null));

                    bool allAcepted = true;

                    foreach (var compiledAnswer in userCompiledAnswers)
                    {
                        allAcepted &= (compiledAnswer.StatusRef == (int)Status.Accepted);
                    }
                    if (allAcepted)
                    {
                        userRank += (int)question.Rank;
                    }
                }
            }
            else
            {
                userRank = -1;
            }
        }
        
        return userRank;
    }

    private static TblUserAnswers FindLatestUserAnswer(IList<TblUserAnswers> userAnswers)
    {
        var latestUserAnswer = userAnswers[0];
        foreach (var o in userAnswers)
        {
            if (o.Date > latestUserAnswer.Date)
                latestUserAnswer = o;
        }

        return latestUserAnswer;
    }

    private static void SetPageResult(TblPages page, int userId, TableCell passStatusCell, TableCell userRankCell)
    {
        int userRank = GetUserRank(page, userId);

        if (userRank < 0)
        {
            userRankCell.Text = "NO RANK";
            passStatusCell.Text = "NO RESULT";
        }
        else
        {
            userRankCell.Text = userRank.ToString();
            passStatusCell.Text = userRank >= (int)page.PageRank ? "pass" : "fail";
        }
    }

    private static int GetUserRank(TblPages page, int userId)
    {
        var questionsIDs = ServerModel.DB.LookupIds<TblQuestions>(page, null);
        var questions = ServerModel.DB.Load<TblQuestions>(questionsIDs);

        int userRank = 0;

        foreach (var question in questions)
        {
            isContainCompiledQuestions = question.IsCompiled;

            userRank += CalculateUserRank(question, userId);
        }
        return userRank;
    }

    private static void SetPageNameAndRank(TblPages page, TableCell pageNameCell, TableCell pageRankCell)
    {
        pageNameCell.Text =  page.PageName;

        pageRankCell.Text = page.PageRank.ToString();
    }

    private static IList<TblUserAnswers> FindUserAnswers(IList<TblUserAnswers> userAnswers, int userId)
    {
        var currentUserAnswers = new List<TblUserAnswers>();

        foreach (var o in userAnswers)
        {
            if (o.UserRef == userId)
                currentUserAnswers.Add(o);
        }

        return currentUserAnswers;
    }

    private static void SetPageDetailsLink(TableCell cell, int pageId)
    {
        cell.Controls.Add(new HyperLink{Text = "Details", NavigateUrl = string.Format(pageDetailsUrl, pageId)});
    }

    private static void SetCompiledDetailsLink(TableCell cell, int pageId)
    {
        cell.Controls.Add(new HyperLink { Text = "Compiled Details", NavigateUrl = string.Format(compiledDetailsUrl, pageId) });
    }
}
