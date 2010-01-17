using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using IUDICO.DataModel;
using IUDICO.DataModel.Common.StatisticUtils;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.DB;

public partial class ThemeResultControl : UserControl
{
    public TblLearnerAttempts LearnerAttempt { get; set; }

    public IList<TblLearnerSessions> LearnerSessions { get; set; }

    public TblUsers User { get; set; }

    public TblThemes Theme { get; set; }

    public string CurriculumnName { get; set; }

    public string StageName { get; set; }

    public ThemeResultControl()
    {
        _resultTable = new Table();
        _themeName = new Label();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var currentUser = ServerModel.User.Current;
        
        if (currentUser != null)
        {
            SetHeaderText(Theme.Name, CurriculumnName, StageName, User.DisplayName);
            Dictionary<int, TableRow> rows = new Dictionary<int, TableRow>();

            foreach (TblLearnerSessions learnerSession in LearnerSessions)
            {
                List<TblLearnerSessionsVars> userResults = StatisticManager.GetStatisticForLearnerSession(learnerSession.ID);
                TblItems item = ServerModel.DB.Load<TblItems>(learnerSession.ItemRef);
                
                string correntAnswer = null;
                string userAnswer = null;
                string result = null;

                foreach (var ur in userResults)
                {
                    if (ur.Name.StartsWith("cmi.interactions."))
                    {
                        string[] parts = ur.Name.Split('.');
                        int questionNum = Convert.ToInt32(parts[2]);

                        switch (parts[3])
                        {
                            case "correct_responses":
                                correntAnswer = ur.Value;
                                break;
                            case "learner_response":
                                userAnswer = ur.Value;
                                break;
                            case "result":
                                result = ur.Value;
                                break;
                        }
                    }
                }

                if (correntAnswer == null)
                {
                    continue;
                }

                var row = new TableRow();

                row.Cells.Add(new TableCell { Text = item.Title });
                row.Cells.Add(new TableCell { Text = userAnswer });
                row.Cells.Add(new TableCell { Text = correntAnswer });
                row.Cells.Add(new TableCell { Text = result });

                rows[item.ID] = row;
            }

            foreach (KeyValuePair<int, TableRow> kvp in rows)
            {
                _resultTable.Rows.Add(kvp.Value);
            }
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
