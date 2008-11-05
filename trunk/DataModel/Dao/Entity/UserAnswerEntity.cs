using System;
using IUDICO.DataModel.Common;

/// <summary>
/// Summary description for UserAnswersEntity
/// </summary>
public class UserAnswerEntity
{
    private readonly int id;
    private readonly int userRef;
    private readonly string userAnswer;
    private readonly int questionRef;
    private readonly DateTime date;
    private readonly int isCompiledAnswer;
    private readonly int compiledAnswerRef;

    public UserAnswerEntity(int userRef, int questionRef, string userAnswer, bool isCompiledAnswer, int compiledAnswerRef)
    {
        id = UniqueId.Generate();
        this.userRef = userRef;
        this.questionRef = questionRef;
        this.userAnswer = userAnswer;
        date = DateTime.Now;
        this.isCompiledAnswer = isCompiledAnswer ? 1 : 0;
        this.compiledAnswerRef = compiledAnswerRef;
    }

    public DateTime Date
    {
        get { return date; }
    }

    public string UserAnswer
    {
        get { return userAnswer; }
    }

    public int QuestionRef
    {
        get { return questionRef; }
    }

    public int UserRef
    {
        get { return userRef; }
    }

    public int Id
    {
        get { return id; }
    }

    public int IsCompiledAnswer
    {
        get { return isCompiledAnswer; }
    }

    public int CompiledAnswerRef
    {
        get { return compiledAnswerRef; }
    }
}