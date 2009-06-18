using System.Web.UI;

namespace IUDICO.DataModel.WebControl
{
    public abstract class TestControlBase : UserControl
    {
        public int QuestionId;

        protected abstract void ApplyStyles();
    }
}