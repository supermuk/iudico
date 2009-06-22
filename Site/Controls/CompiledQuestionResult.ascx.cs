using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.DB;
using TestingSystem;

/// <summary>
/// Summary description for CompiledQuestionResult
/// </summary>
public partial class CompiledQuestionResult : UserControl
{
    public void BuildCompiledQuestionsResult(TblQuestions q, int userId)
    {
        var compiledQuestion = ServerModel.DB.Load<TblCompiledQuestions>((int)q.CompiledQuestionRef);
        var answersForCurrentUser = StudentRecordFinder.GetUserAnswersForQuestion(q, userId);

        SetHeader(q.TestName, compiledQuestion);
        SetTableHeader();

        if (answersForCurrentUser != null && answersForCurrentUser.Count != 0)
        {
            var lastUserAnswer = FindLatestUserAnswer(answersForCurrentUser);
            var compileAnswer = StudentRecordFinder.GetCompiledAnswersForAnswer(lastUserAnswer);

            if (compileAnswer != null && compileAnswer.Count != 0)
            {
                SetResults(compileAnswer);
            }
            else
            {
                SetNoResultStatus(q);
            }
        }
        else
        {
            SetNoResultStatus(q);
        }
    }

    private void SetNoResultStatus(TblQuestions q)
    {
        _compiledAnswerTable.Visible = false;
        _statusLabel.Visible = true;
        _statusLabel.Text = string.Format("You not compile {0} yet", q.TestName);
    }

    private void SetResults(IList<TblCompiledAnswers> compileAnswer)
    {
        foreach (var ca in compileAnswer)
            SetTestCaseResult(StudentRecordFinder.GetCompiledQuestionDataForCompiledAnswer(ca), ca);
    }


    private void SetHeader(string name, TblCompiledQuestions ua)
    {
        _headerLabel.Text = string.Format("Name:{0}; Time Limit:{1}; Memory Limit:{2}; Language:{3};",
        name, ua.TimeLimit, ua.MemoryLimit, LanguageHelper.FxLanguagesToLanguage(ua.LanguageRef));
    }

    private void SetTableHeader()
    {
        var tableRow = new TableRow();

        if (ServerModel.User.Current.Islector())
        {
            var inputCell = new TableCell { Text = "Input" };

            var expectedOutputCell = new TableCell { Text = "Expected Output" };

            tableRow.Cells.AddRange(new[] { inputCell, expectedOutputCell });
        }

        var userOutputCell = new TableCell { Text = "User Output" };

        var timeUsedCell = new TableCell { Text = "Time Used" };

        var memoryUsedCell = new TableCell { Text = "Memory Used" };

        var statusCell = new TableCell { Text = "Status" };


        tableRow.Cells.AddRange(new[] { userOutputCell, timeUsedCell, memoryUsedCell, statusCell });

        _compiledAnswerTable.Rows.Add(tableRow);
    }

    private void SetTestCaseResult(TblCompiledQuestionsData compiledData, TblCompiledAnswers compileAnswer)
    {
        var tableRow = new TableRow();

        if (ServerModel.User.Current.Islector())
        {
            var inputCell = new TableCell { Text = MakeSpaceAndEnterVisible(compiledData.Input) };

            var expectedOutputCell = new TableCell { Text = MakeSpaceAndEnterVisible(compiledData.Output) };

            tableRow.Cells.AddRange(new[] { inputCell, expectedOutputCell });
        }

        var userOutputCell = new TableCell
        {
            Text = MakeSpaceAndEnterVisible(compileAnswer.Output)
        };

        var timeUsedCell = new TableCell { Text = compileAnswer.TimeUsed.ToString() };

        var memoryUsedCell = new TableCell { Text = compileAnswer.MemoryUsed.ToString() };

        var statusCell = new TableCell { Text = ((Status)compileAnswer.StatusRef).ToString() };


        tableRow.Cells.AddRange(new[] { userOutputCell, timeUsedCell, memoryUsedCell, statusCell });

        _compiledAnswerTable.Rows.Add(tableRow);
    }

    private static TblUserAnswers FindLatestUserAnswer(IList<TblUserAnswers> userAnswers)
    {
        var latestUserAnswer = userAnswers[0];
        foreach (var o in userAnswers)
            if (o.Date > latestUserAnswer.Date)
                latestUserAnswer = o;

        return latestUserAnswer;
    }

    private string MakeSpaceAndEnterVisible(string input)
    {
        return input.Replace("\r", "\\r").Replace("\n", "\\n");
    }
}
