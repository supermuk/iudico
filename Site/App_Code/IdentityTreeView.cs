using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Security.Permissions;
using System.ComponentModel;
using System.Web.UI;
using IUDICO.DataModel.Controllers;

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


