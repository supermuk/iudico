using System;
using IUDICO.DataModel.Common.TestingUtils;
using IUDICO.DataModel.WebControl;

namespace Controls.TestControls
{
    public partial class TextBoxTest : TestControlBase, ITestControl
    {
        //public string InnerText;

        protected void Page_Load(object sender, EventArgs e)
        {
            ApplyStyles();
        }

        protected override void ApplyStyles()
        {
            _testTextBox.Attributes["Style"] = Attributes["Style"];
        }

        public void SubmitAnswer()
        {
            TestManager.StartTesting(QuestionId, _testTextBox.Text, false);
        }

        public void FillCorrectAnswer()
        {
            _testTextBox.Text = AnswerFiller.GetCorrectAnswer(QuestionId);
        }

        public void FillUserAnswer(int userId)
        {
            _testTextBox.Text = AnswerFiller.GetUserAnswer(QuestionId, userId);
        }
    }
}