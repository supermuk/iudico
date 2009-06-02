using System.Web.UI;

namespace IUDICO.DataModel.WebControl
{
    public abstract class TestControlBase : UserControl
    {
        public string QuestionId;

        protected abstract void ApplyStyles();
    }
}