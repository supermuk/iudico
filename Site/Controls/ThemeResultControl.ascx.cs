using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Common.TestingUtils;
using IUDICO.DataModel.Common.TestRequestUtils;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;

public partial class ThemeResultControl : UserControl
{
    public int ThemeId { get; set; }

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
        var theme = ServerModel.DB.Load<TblThemes>(ThemeId);
        var currentUser = ServerModel.User.Current;
        
        if (currentUser != null)
        {
            var user = ServerModel.DB.Load<TblUsers>(UserId);
            var currentUserRoles = currentUser.Roles;

            SetHeaderText(theme.Name, CurriculumnName, StageName, user.DisplayName);

            var pages = ServerModel.DB.Load <TblPages>(ServerModel.DB.LookupIds<TblPages>(theme, null));

            int totalPageRank = 0;
            int totalUserRank = 0;

            foreach (var page in pages)
            {
                if (page.PageTypeRef == (int)FX_PAGETYPE.Practice)
                {
                    int userRank = UserResultCalculator.GetUserRank(page, user.ID);
                    totalUserRank += (userRank < 0 ? 0 : userRank);
                    totalPageRank += (int)page.PageRank;

                    var row = new TableRow();

                    SetPageName(row, page.PageName);
                    SetStatus(row, userRank, (int) page.PageRank);
                    SetUserRank(row, userRank);
                    SetPageRank(row, (int) page.PageRank);
                    SetUserAnswersLink(row, page.ID, user.ID);

                    if (currentUserRoles.Contains(FX_ROLE.ADMIN.ToString()) ||
                        currentUserRoles.Contains(FX_ROLE.LECTOR.ToString()) ||
                        currentUserRoles.Contains(FX_ROLE.SUPER_ADMIN.ToString()))
                    {
                        SetCorrectAnswersLink(row, page.ID);
                    }

                    if (UserResultCalculator.IsContainCompiledQuestions(page))
                        SetCompiledDetailsLink(row, page.ID, user.ID);

                    _resultTable.Rows.Add(row);
                }
            }
            SetTotalRow(totalPageRank, (totalUserRank < 0) ? 0 : totalUserRank);
        }
    }

    private void SetHeaderText(string theme, string curriculumnName, string stageName, string user)
    {
        _themeName.Text = string.Format(@"Statistic for theme: {0}\{1}\{2} for user: {3}", curriculumnName, stageName, theme, user);
    }

    private static void SetStatus(TableRow row, int userRank, int pageRank)
    {
        var c = new TableCell();

        if (userRank < 0)
        {
            c.Text = "NO RESULT";
        }
        else
        {
            c.Text = userRank >= pageRank ? "pass" : "fail";
        }

        row.Cells.Add(c);
    }

    private static void SetUserRank(TableRow row, int userRank)
    {
        var c = new TableCell
                    {
                        Text = (userRank < 0 ? "NO RANK" : userRank.ToString())
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
