using System;
using IUDICO.DataModel.WebControl;

namespace Controls.TestControls
{
    public partial class TextBoxTest : TestControlBase, ITestControl
    {
        public string InnerText;

        protected void Page_Load(object sender, EventArgs e)
        {
            _testTextBox.Text = InnerText;
            ApplyStyles();
        }

        protected override void ApplyStyles()
        {
            _testTextBox.Attributes["Style"] = Attributes["Style"];
        }
    }
}