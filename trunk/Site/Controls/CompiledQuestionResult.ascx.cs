﻿using System.Collections.Generic;
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
        var compiledQuestion = ServerModel.DB.Load<TblCompiledQuestions>((int) q.CompiledQuestionRef);
        var userAnswers = ServerModel.DB.Load<TblUserAnswers>(ServerModel.DB.LookupIds<TblUserAnswers>(q, null));
        
        var answersForCurrentUser = new List<TblUserAnswers>();
        
        if (ServerModel.User.Current != null)
        {
            foreach (var uAns in userAnswers)
                if (uAns.UserRef == userId)
                    answersForCurrentUser.Add(uAns);
        }


        SetHeader(q.TestName, compiledQuestion);

        if (userAnswers.Count != 0)
            SetResults(answersForCurrentUser);
        else
            SetNoResultStatus(q);
    }

    private void SetNoResultStatus(TblQuestions q)
    {
        _compiledAnswerTable.Visible = false;
        _statusLabel.Visible = true;
        _statusLabel.Text = string.Format("You not compile {0} yet", q.TestName);
    }

    private void SetResults(IList<TblUserAnswers> userAnswers)
    {
        var compileAnswer = StudentRecordFinder.GetCompiledAnswersForAnswer(FindLatestUserAnswer(userAnswers));

        foreach (var ca in compileAnswer)
            SetTestCaseResult(StudentRecordFinder.GetCompiledQuestionDataForCompiledAnswer(ca), ca);
    }


    private void SetHeader(string name, TblCompiledQuestions ua)
    {
        _headerLabel.Text = string.Format("Name:{0}; Time Limit:{1}; Memory Limit:{2}; Language:{3};",
        name, ua.TimeLimit, ua.MemoryLimit, LanguageHelper.FxLanguagesToLanguage(ua.LanguageRef));
    }

    private void SetTestCaseResult(TblCompiledQuestionsData compiledData, TblCompiledAnswers compileAnswer)
    {
        var inputCell = new TableCell {Text = compiledData.Input};

        var expectedOutputCell = new TableCell {Text = compiledData.Output};

        var userOutputCell = new TableCell {Text = compileAnswer.Output};

        var timeUsedCell = new TableCell {Text = compileAnswer.TimeUsed.ToString()};

        var memoryUsedCell = new TableCell {Text = compileAnswer.MemoryUsed.ToString()};

        var statusCell = new TableCell {Text = ((Status) compileAnswer.StatusRef).ToString()};


        var tableRow = new TableRow();
        tableRow.Cells.AddRange(new[]{inputCell, expectedOutputCell, userOutputCell, timeUsedCell, memoryUsedCell, statusCell});


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
}
