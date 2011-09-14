using System.Web.UI;
using System.Web.UI.WebControls;

namespace BoxOver
{
    public class BoxOverControlConverter : ControlIDConverter
    {
        protected override bool FilterControl(Control control)
        {
            return control.GetType() != typeof (BoxOver);
        }
    }
}