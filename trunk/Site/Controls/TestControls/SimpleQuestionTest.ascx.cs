using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common.TestingUtils;
using IUDICO.DataModel.WebControl;

namespace Controls.TestControls
{
    [ParseChildren(true, "Items")]
    public partial class SimpleQuestionTest : ItemListTestControlBase, ITestControl
    {
        public string SingleCase;

        public string QuestionText;

        protected void Page_Load(object sender, EventArgs e)
        {   
            ApplyStyles();

            var c = ParseControl(string.Format("<p>{0}</p>", QuestionText));
            _testPanel.Controls.Add(c);

            ListControl listControl = bool.Parse(SingleCase) ? (ListControl)(new RadioButtonList()) : new CheckBoxList();

            listControl.Items.AddRange(Items.ToArray());
            _testPanel.Controls.Add(listControl);
        }

        protected override void ApplyStyles()
        {
            _testPanel.Attributes["Style"] = Attributes["Style"];
        }

        public void SubmitAnswer()
        {
            string ans = string.Empty;

            foreach (var b in Items)
                ans += b.Selected ? "1" : "0";

            TestManager.StartTesting(QuestionId, ans, false);
        }

        public void FillCorrectAnswer()
        {
            var answer = AnswerFiller.GetCorrectAnswer(QuestionId);

            if (!answer.Equals(string.Empty))
            {
                for (int i = 0; i < Items.Count; i++)
                    Items[i].Selected = answer[i].Equals('1');
            }
        }

        public void FillUserAnswer(int userId)
        {
            var answer = AnswerFiller.GetUserAnswer(QuestionId, userId);

            if (!answer.Equals(string.Empty))
            {
                for (int i = 0; i < Items.Count; i++)
                    Items[i].Selected = answer[i].Equals('1');
            }
        }
    }
}