using System;
using IUDICO.DataModel.WebControl;

namespace Controls.TestControls
{
    public partial class CompiledTest : TestControlBase, ITestControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplyStyles();
        }

        protected override void ApplyStyles()
        {
            _testTextBox.Attributes["Style"] = Attributes["Style"];
        }
    }
}