using System;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;

public partial class ThemeResultControl : System.Web.UI.UserControl
{
    public int ThemeId { get; set; }

    public string CurriculumnName { get; set; }

    public string StageName { get; set; }

    public ThemeResultControl()
    {
        resultTable = new Table();
        themeName = new Label();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var theme = ServerModel.DB.Load<TblThemes>(ThemeId);
        var user = ServerModel.User.Current;
        var userId = user.ID;
        var userRoles = user.Roles;

        SetHeaderText(theme.Name, CurriculumnName, StageName, user.UserName);

        var pages = ServerModel.DB.Load <TblPages>(ServerModel.DB.LookupIds<TblPages>(theme, null));

        int totalPageRank = 0;
        int totalUserRank = 0;

        foreach (var page in pages)
        {
            if (page.PageTypeRef == (int)FX_PAGETYPE.Practice)
            {
                int userRank = UserResultCalculator.GetUserRank(page, userId);
                totalUserRank += userRank;
                totalPageRank += (int)page.PageRank;

                var row = new TableRow();

                SetPageName(row, page.PageName);
                SetStatus(row, userRank, (int) page.PageRank);
                SetUserRank(row, userRank);
                SetPageRank(row, (int) page.PageRank);
                SetUserAnswersLink(row, page.ID);

                if (userRoles.Contains(FX_ROLE.ADMIN.ToString()) ||
                    userRoles.Contains(FX_ROLE.LECTOR.ToString()) ||
                    userRoles.Contains(FX_ROLE.SUPER_ADMIN.ToString()))
                {
                    SetCorrectAnswersLink(row, page.ID);
                }

                if (UserResultCalculator.IsContainCompiledQuestions(page))
                    SetCompiledDetailsLink(row, page.ID);

                resultTable.Rows.Add(row);
            }
        }
        SetTotalRow(totalPageRank, (totalUserRank < 0) ? 0 : totalUserRank);
    }

    private void SetHeaderText(string theme, string curriculumnName, string stageName, string user)
    {
        themeName.Text = string.Format(@"Statistic for theme: {0}\{1}\{2} for user: {3}", curriculumnName, stageName, theme, user);
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
                                                                                        AnswerFlag = "correct"
                                                                                    })});
        row.Cells.Add(c);
    }

    private static void SetUserAnswersLink(TableRow row, int pageId)
    {
        var c = new TableCell();
        c.Controls.Add(new HyperLink
                           {Text = "User Answers", NavigateUrl = ServerModel.Forms.BuildRedirectUrl(new TestDetailsController
                                                                                    {
                                                                                        BackUrl = string.Empty,
                                                                                        PageId = pageId,
                                                                                        AnswerFlag = "user"
                                                                                    })});
        row.Cells.Add(c);
    }

    private static void SetCompiledDetailsLink(TableRow row, int pageId)
    {
        var c = new TableCell();
        c.Controls.Add(new HyperLink { Text = "Compiled Details", NavigateUrl = ServerModel.Forms.BuildRedirectUrl(new CompiledQuestionsDetailsController
                                                                                                                       {
                                                                                        BackUrl = string.Empty,
                                                                                        PageId = pageId    
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
        resultTable.Rows.Add(row);
        
    }
}
