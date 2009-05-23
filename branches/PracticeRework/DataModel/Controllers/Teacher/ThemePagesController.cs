using System;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common.TestRequestUtils;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers.Teacher
{
    public class ThemePagesController : ControllerBase
    {
        [ControllerParameter]
        public int ThemeId;

        public readonly IVariable<string> ThemeName = string.Empty.AsVariable();

        public Table ThemePagesTable { get; set; }

        public void PageLoad(object sender, EventArgs e)
        {
            var theme = ServerModel.DB.Load<TblThemes>(ThemeId);
            ThemeName.Value = theme.Name;

            var pagesIds = ServerModel.DB.LookupIds<TblPages>(theme, null);

            for (int i = 1; i <= pagesIds.Count; i++)
            {
                var page = ServerModel.DB.Load<TblPages>(pagesIds[i - 1]);
                var row = new TableRow();

                var number = new TableCell {Text = i.ToString()};
                var name = new TableCell {Text = page.PageName};
                var correctAnswers = new TableCell();
                var rank = new TableCell();

                if (page.PageTypeRef == (int?)FX_PAGETYPE.Practice)
                {
                    rank.Text = page.PageRank.ToString();

                    
                    correctAnswers.Controls.Add(new HyperLink
                    {
                        Text = "Correct Answers",
                        NavigateUrl = ServerModel.Forms.BuildRedirectUrl(new TestDetailsController
                        {
                            BackUrl = string.Empty,
                            PageId = page.ID,
                            TestType = (int)TestSessionType.CorrectAnswer
                        })
                    });
                }

                row.Cells.AddRange(new[] { number, name, rank, correctAnswers });

                ThemePagesTable.Rows.Add(row);
            }

        }
    }
}
