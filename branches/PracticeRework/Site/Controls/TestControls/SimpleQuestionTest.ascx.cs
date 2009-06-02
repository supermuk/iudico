using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.WebControl;

namespace Controls.TestControls
{
    [ParseChildren(true, "Items")] 
    public partial class SimpleQuestionTest : TestControlBase, ITestControl
    {
        public string SingleCase;

        public string QuestionText;

        private readonly List<ListItem> _items = new List<ListItem>();

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<ListItem> Items
        {
            get { return _items; }
        }

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
    }
}