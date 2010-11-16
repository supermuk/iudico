using System;
using System.Web.UI;
using IUDICO.DataModel.Common.TestingUtils;
using IUDICO.DataModel.WebControl;

namespace Controls.TestControls
{
    [ParseChildren(true, "Items")]
    public partial class ComboBoxTest : ItemListTestControlBase, ITestControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplyStyles();
            _testDropDownList.Items.AddRange(Items.ToArray());
        }

        protected override void ApplyStyles()
        {
            _testDropDownList.Attributes["Style"] = Attributes["Style"];
        }

        public void SubmitAnswer()
        {
            TestManager.StartTesting(QuestionId, _testDropDownList.SelectedIndex.ToString(), false);
        }

        public void FillCorrectAnswer()
        {
            _testDropDownList.SelectedIndex = Convert.ToInt32(AnswerFiller.GetCorrectAnswer(QuestionId));
        }

        public void FillUserAnswer(int userId)
        {
            _testDropDownList.SelectedIndex = Convert.ToInt32(AnswerFiller.GetUserAnswer(QuestionId, userId));
        }
    }
}