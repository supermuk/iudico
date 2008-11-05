/// <summary>
/// Summary description for CorrectAnswerEntity
/// </summary>
public class QuestionEntity
{
    private readonly string correctAnswer;
    private readonly int id;
    private readonly int isCompiled;
    private readonly int pageRef;
    private readonly int compiledQuestionRef;
    private readonly int rank;
    private readonly string testName;

    private QuestionEntity(int id, int pageRef, string testName, int compiledQuestionRef, int rank)
    {
        this.id = id;
        this.pageRef = pageRef;
        this.testName = testName;
        this.rank = rank;
        isCompiled = 1;
        this.compiledQuestionRef = compiledQuestionRef;
    }

    private QuestionEntity(int id, int pageRef, string testName, string correctAnswer, int rank)
    {
        this.id = id;
        this.pageRef = pageRef;
        this.testName = testName;
        this.correctAnswer = correctAnswer;
        this.rank = rank;
        isCompiled = 0;
    }

    public static QuestionEntity newCompiledQuestion(int id, int pageRef, string testName, int compiledQuestionRef, int rank)
    {
        return new QuestionEntity(id, pageRef, testName, compiledQuestionRef, rank);
    }

    public static QuestionEntity newQuestion(int id, int pageRef, string testName, string correctAnswer, int rank)
    {
        return new QuestionEntity(id, pageRef, testName, correctAnswer, rank);
    }

    public int Rank
    {
        get { return rank; }
    }

    public string CorrectAnswer
    {
        get { return correctAnswer; }
    }

    public string TestName
    {
        get { return testName; }
    }

    public int PageRef
    {
        get { return pageRef; }
    }

    public int Id
    {
        get { return id; }
    }

    public int CompiledQuestionRef
    {
        get { return compiledQuestionRef; }
    }

    public int IsCompiled
    {
        get { return isCompiled; }
    }
}