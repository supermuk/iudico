using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.WebControl
{
    public abstract class WebTestControl : WebControl
    {
        private readonly int id;

        protected WebTestControl()
        {
            var q = new TblQuestions();
            ServerModel.DB.Insert(q);

            id = q.ID;
        }

        public int Id
        {
            get { return id; }
        }

        public int AnswerIndex { get; set; }

        public abstract string CreateCodeForTest();
    }
}