using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using IUDICO.DataModel;
using IUDICO.DataModel.Common.StatisticUtils;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Common;

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
            //Dictionary<int, TableRow> rows = new Dictionary<int, TableRow>();
            List<TableRow> rows = new List<TableRow>();

            foreach (TblLearnerSessions learnerSession in LearnerSessions)
            {
                //List<TblVarsInteractions> userResults = StatisticManager.GetStatisticForLearnerSession(learnerSession.ID);
                TblItems item = ServerModel.DB.Load<TblItems>(learnerSession.ItemRef);
                
                string correctAnswer = null;
                string userAnswer = null;
                string result = null;

                CmiDataModel cmiDataModel = new CmiDataModel(learnerSession.ID, ServerModel.User.Current.ID, false);
                List<TblVarsInteractions> interactionsCollection = cmiDataModel.GetCollection<TblVarsInteractions>("interactions.*.*");
                List<TblVarsInteractionCorrectResponses> interactionCorrectResponsesCollection = cmiDataModel.GetCollection<TblVarsInteractionCorrectResponses>("interactions.*.correct_responses.*");
                int count=int.Parse(cmiDataModel.GetValue("interactions._count"));

                for (int i = 0, j = 0; i < count; i++)
                {
                  if (interactionCorrectResponsesCollection[i].Value!=null)
                  {
                    correctAnswer = interactionCorrectResponsesCollection[i].Value;
                  }
                  else
                  {
                    continue;
                  }
                  for (; j < interactionsCollection.Count && i == interactionsCollection[j].Number; j++)
                  {
                    if (interactionsCollection[j].Name == "learner_response")
                    {
                      userAnswer = interactionsCollection[j].Value;
                    }
                    else if (interactionsCollection[j].Name == "result")
                    {
                      result = interactionsCollection[j].Value;
                    }
                  }

                  var row = new TableRow();
                  row.Cells.Add(new TableCell { Text = item.Title });
                  row.Cells.Add(new TableCell { Text = userAnswer });
                  row.Cells.Add(new TableCell { Text = correctAnswer });
                  row.Cells.Add(new TableCell { Text = result });

                  rows.Add(row);
                }
                /*for (int i = 0; i < int.Parse(cmiDataModel.GetValue("interactions._count"));i++)
                {
                  correctAnswer = cmiDataModel.GetValue(String.Format("interactions.{0}.correct_responses.0.pattern", i));
                  userAnswer = cmiDataModel.GetValue(String.Format("interactions.{0}.learner_response", i));
                  result = cmiDataModel.GetValue(String.Format("interactions.{0}.result", i));

                  if (correctAnswer == null)
                  {
                    continue;
                  }

                  var row = new TableRow();
                  row.Cells.Add(new TableCell { Text = item.Title });
                  row.Cells.Add(new TableCell { Text = userAnswer });
                  row.Cells.Add(new TableCell { Text = correctAnswer });
                  row.Cells.Add(new TableCell { Text = result });

                  rows.Add(row);
                }*/
            }

            foreach(TableRow row in rows)
            {
              _resultTable.Rows.Add(row);
            }
        }
    }

    private void SetHeaderText(string theme, string curriculumnName, string stageName, string user)
    {
        _themeName.Text = string.Format(@"Statistic for theme: {0}\{1}\{2} for user: {3}", curriculumnName, stageName, theme, user);
        _startDateTime.Text = string.Format("Date Time: {0}", LearnerAttempt.Started.ToString());
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
