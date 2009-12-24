using System.Web.UI;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.WebControl
{
    public abstract class WebTestControlBase : WebControlBase
    {
        private int _id;

        public int Id
        {
            get { return _id; }
        }

        public int AnswerIndex { get; set; }

        protected void CreateQuestionId()
        {
            var q = new TblQuestions();
            ServerModel.DB.Insert(q);

            _id = q.ID;
        }

        public override void Store(HtmlTextWriter w)
        {
            CreateQuestionId();

            base.Store(w);

            w.AddAttribute("runat", "server");
            w.AddAttribute("QuestionId", string.Format("{0}", Id));
        }
    }
}