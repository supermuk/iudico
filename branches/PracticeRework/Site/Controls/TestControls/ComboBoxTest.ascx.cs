using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common.TestingUtils;
using IUDICO.DataModel.WebControl;

namespace Controls.TestControls
{
    [ParseChildren(true, "Items")] 
    public partial class ComboBoxTest : TestControlBase, ITestControl
    {
        private readonly List<ListItem> _items = new List<ListItem>();

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<ListItem> Items
        {
            get { return _items; }
        }

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