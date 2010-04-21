using System.Web.UI;
using System.Web.UI.WebControls;

namespace BoxOver
{
    /// <summary>
    /// BoxOverControlConverter
    /// </summary>
    public class BoxOverControlConverter : ControlIDConverter
    {
        protected override bool FilterControl(Control control)
        {
            return control.GetType() != typeof (BoxOver);
        }
    }
}