using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Common.StatisticUtils;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.DB;

public partial class ThemeResultControl : UserControl
{
    public int LearnerSessionId { get; set; }

    public int UserId { get; set; }

    public string CurriculumnName { get; set; }

    public string StageName { get; set; }

    public ThemeResultControl()
    {
        _resultTable = new Table();
        _themeName = new Label();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var learnerSession = ServerModel.DB.Load<TblLearnerSessions>(LearnerSessionId);
        var learnerAttempt = ServerModel.DB.Load<TblLearnerAttempts>(learnerSession.LearnerAttemptRef);
        var theme = ServerModel.DB.Load<TblThemes>(learnerAttempt.ThemeRef);
        var currentUser = ServerModel.User.Current;
        
        if (currentUser != null)
        {
            var user = ServerModel.DB.Load<TblUsers>(UserId);
            SetHeaderText(theme.Name, CurriculumnName, StageName, user.DisplayName);

            var userResults = StatisticManager.GetStatisticForLearnerSession(LearnerSessionId);

            foreach (var ur in userResults)
            {
                if (ur.Name.StartsWith("cmi.interactions."))
                {
                    string[] parts = ur.Name.Split('.');
                }
            }
            
            
            
            
            
            
            
            /*
            var userResults = StatisticManager.GetStatisticForThemeForUser(user.ID, theme.ID);

            foreach (var ur in userResults)
            {
                if (ur.Status != ResultStatus.NotIncluded)
                {
                    totalUserRank += ur.UserRank;
                    totalPageRank += ur.PageRank;

                    var row = new TableRow();

                    SetPageName(row, ur.Item.Title);
                    SetStatus(row, ur.Status);
                    SetUserRank(row, ur.UserRank);
                    //SetPageRank(row, (int) ur.Page.PageRank);
                    SetUserAnswersLink(row, ur.Item.ID, user.ID);

                    if (ServerModel.User.Current.Islector())
                    {
                        SetCorrectAnswersLink(row, ur.Item.ID);
                    }

                    if (StatisticManager.IsContainCompiledQuestions(ur.Item))
                    {
                        SetCompiledDetailsLink(row, ur.Item.ID, user.ID);
                    }

                    _resultTable.Rows.Add(row);
                }
            }
            SetTotalRow(totalPageRank, (totalUserRank < 0) ? 0 : totalUserRank);
             * */
        }
    }

    private void SetHeaderText(string theme, string curriculumnName, string stageName, string user)
    {
        _themeName.Text = string.Format(@"Statistic for theme: {0}\{1}\{2} for user: {3}", curriculumnName, stageName, theme, user);
    }

    private static void SetStatus(TableRow row, ResultStatus status )
    {
        var c = new TableCell {Text = status.ToString()};

        row.Cells.Add(c);
    }

    private static void SetUserRank(TableRow row, int userRank)
    {
        var c = new TableCell
                    {
                        Text = userRank.ToString()
                    };

        row.Cells.Add(c);
    }

    private static void SetPageName(TableRow row, string pageName)
    {
        var c = new TableCell {Text = pageName};

        row.Cells.Add(c);
    }

    private static void SetPageRank(TableRow row, int pageRank)
    {
        var c = new TableCell {Text = pageRank.ToString()};

        row.Cells.Add(c);
    }

    private static void SetCorrectAnswersLink(TableRow row, int pageId)
    {
        var c = new TableCell();
        c.Controls.Add(new HyperLink{Text = "Correct Answers", NavigateUrl = ServerModel.Forms.BuildRedirectUrl(new TestDetailsController
                                                                                    {
                                                                                        BackUrl = string.Empty,
                                                                                        PageId = pageId,
                                                                                        TestType = (int) TestSessionType.CorrectAnswer
                                                                                    })});
        row.Cells.Add(c);
    }

    private static void SetUserAnswersLink(TableRow row, int pageId, int userId)
    {
        var c = new TableCell();
        c.Controls.Add(new HyperLink
                           {Text = "User Answers", NavigateUrl = ServerModel.Forms.BuildRedirectUrl(new TestDetailsController
                                                                                    {
                                                                                        BackUrl = string.Empty,
                                                                                        PageId = pageId,
                                                                                        TestType = (int) TestSessionType.UserAnswer,
                                                                                        UserId = userId
                                                                                    })});
        row.Cells.Add(c);
    }

    private static void SetCompiledDetailsLink(TableRow row, int pageId, int userId)
    {
        var c = new TableCell();
        c.Controls.Add(new HyperLink { Text = "Compiled Details", NavigateUrl = ServerModel.Forms.BuildRedirectUrl(new CompiledQuestionsDetailsController
                                                                                                                       {
                                                                                        BackUrl = string.Empty,
                                                                                        PageId = pageId,
                                                                                        UserId = userId
                                                                                    })});
        row.Cells.Add(c);
    }

    private void SetTotalRow(int totalUserRank, int totalPageRank)
    {
        var row = new TableRow();

        var totalCell = new TableCell{ Text = "Total" };
        var userRankCell = new TableCell { Text = totalUserRank.ToString() };
        var pageRankCell = new TableCell { Text = totalPageRank.ToString() };

        row.Cells.AddRange(new[] { totalCell, new TableCell(), pageRankCell, userRankCell, new TableCell(), new TableCell() });
        _resultTable.Rows.Add(row);
        
    }
}
