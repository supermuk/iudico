using System.Web.UI.WebControls;

namespace IUDICO.DataModel.Controllers
{
    public class IdentityTreeView : TreeView
    {
        protected override TreeNode CreateNode()
        {
            return new IdendtityNode();
        }
    }
}


