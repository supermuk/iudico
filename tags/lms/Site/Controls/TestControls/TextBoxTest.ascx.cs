using System;
using IUDICO.DataModel.Common.TestingUtils;
using IUDICO.DataModel.WebControl;

namespace Controls.TestControls
{
    public partial class TextBoxTest : TextBoxTestControlBase, ITestControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplyStyles();

            if (_testTextBox.Text == string.Empty)
                _testTextBox.Text = InnerText;
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