using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;
using TestingSystem;

/// <summary>
/// Summary description for CompiledQuestionResult
/// </summary>
public partial class CompiledQuestionResult : UserControl
{

    public TblQuestions Question
    {
        set
        {
            BuildCompiledQuestionsResult(value);
        }
        
    }

    private void BuildCompiledQuestionsResult(TblQuestions q)
    {
        var compiledQuestion = ServerModel.DB.Load<TblCompiledQuestions>((int) q.CompiledQuestionRef);
        var userAnswers = ServerModel.DB.Load<TblUserAnswers>(ServerModel.DB.LookupIds<TblUserAnswers>(q, null));

        SetHeader(q.TestName, compiledQuestion);

        if (userAnswers.Count != 0)
        {
            var compiledData =
                ServerModel.DB.Load<TblCompiledQuestionsData>(
                    ServerModel.DB.LookupIds<TblCompiledQuestionsData>(compiledQuestion, null));
            var compileAnswer = GetCompiledAnswer(FindLatestUserAnswer(userAnswers));

            for (int i = 0; i < compiledData.Count; i++)
            {
                SetResults(compiledData[i], compileAnswer[i]);
            }
        }
    }

    private void SetHeader(string name, TblCompiledQuestions ua)
    {
        nameLabel.Text += name;
        timeLimitLabel.Text += ua.TimeLimit.ToString();
        memoryLimitLabel.Text += ua.MemoryLimit.ToString();
        languageLabel.Text += ((FX_LANGUAGE) ua.LanguageRef).ToString();
    }

    private void SetResults(TblCompiledQuestionsData compiledData, TblCompiledAnswers compileAnswer)
    {
        var tableRow = new TableRow();

        var inputCell = new TableCell {Text = compiledData.Input};

        var expectedOutputCell = new TableCell {Text = compiledData.Output};

        var userOutputCell = new TableCell {Text = compileAnswer.Output};

        var timeUsedCell = new TableCell {Text = compileAnswer.TimeUsed.ToString()};

        var memoryUsedCell = new TableCell {Text = compileAnswer.MemoryUsed.ToString()};

        var statusCell = new TableCell {Text = ((Status) compileAnswer.StatusRef).ToString()};

        tableRow.Cells.AddRange(new[]{inputCell, expectedOutputCell, userOutputCell, timeUsedCell, memoryUsedCell, statusCell});

        compiledAnswerTable.Rows.Add(tableRow);
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

    private static IList<TblCompiledAnswers> GetCompiledAnswer(TblUserAnswers ua)
    {
        return ServerModel.DB.Load<TblCompiledAnswers>(ServerModel.DB.LookupIds<TblCompiledAnswers>(ua, null));
    }
}
