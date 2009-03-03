using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.WebControl
{
    public abstract class WebTestControl : WebControl
    {
        private int id;

        public int Id
        {
            get { return id; }
        }

        public int AnswerIndex { get; set; }

        public string CreateCodeForTest()
        {
            id = CreateQuestionID();
            return CreateCodeForTest(id);
        }

        public abstract string CreateCodeForTest(int testId);

        public abstract string CreateAnswerFillerCode(string answerFillerVaribleName);

        public int CreateQuestionID()
        {
            var q = new TblQuestions();
            ServerModel.DB.Insert(q);

            return q.ID;
        }
    }
}