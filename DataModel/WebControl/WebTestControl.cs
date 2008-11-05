using IUDICO.DataModel.Common;

namespace IUDICO.DataModel.WebControl
{
    public abstract class WebTestControl : WebControl
    {
        private readonly int id;

        protected WebTestControl()
        {
            id = UniqueId.Generate();
        }

        public int Id
        {
            get { return id; }
        }

        public int AnswerIndex { get; set; }

        public abstract string CreateCodeForTest();
    }
}