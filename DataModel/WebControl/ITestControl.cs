namespace IUDICO.DataModel.WebControl
{
    /// <summary>
    /// Summary description for ITestControl
    /// </summary>
    public interface ITestControl
    {
        void SubmitAnswer();

        void FillCorrectAnswer();

        void FillUserAnswer(int userId);
    }
}