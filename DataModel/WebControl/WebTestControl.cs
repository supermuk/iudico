using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.WebControl
{
    public abstract class WebTestControl : WebControl
    {
        private int _id;

        public int Id
        {
            get { return _id; }
        }

        public int AnswerIndex { get; set; }

        public string CreateCodeForTest()
        {
            _id = CreateQuestionId();
            return CreateCodeForTest(_id);
        }

        public abstract string CreateCodeForTest(int testId);

        public abstract string CreateAnswerFillerCode(string answerFillerVaribleName);

        public int CreateQuestionId()
        {
            var q = new TblQuestions();
            ServerModel.DB.Insert(q);

            return q.ID;
        }
    }
}