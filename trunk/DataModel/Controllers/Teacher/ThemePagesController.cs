using System;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;

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
                var rank = new TableCell {Text = page.PageRank.ToString()};

                var correctAnswers = new TableCell();
                correctAnswers.Controls.Add(new HyperLink{Text = "Correct Answers", NavigateUrl = ServerModel.Forms.BuildRedirectUrl(new TestDetailsController
                                                                                    {
                                                                                        BackUrl = string.Empty,
                                                                                        PageId = page.ID,
                                                                                        AnswerFlag = "correct"
                                                                                    })});
                row.Cells.AddRange(new[] { number, name, rank, correctAnswers});

                ThemePagesTable.Rows.Add(row);
            }

        }
    }
}
