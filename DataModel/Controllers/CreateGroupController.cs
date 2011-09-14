using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;

namespace IUDICO.DataModel.Controllers
{
    /// <summary>
    /// Controller for CreateGroup.aspx page
    /// </summary>
    public class CreateGroupController : ControllerBase
    {
        public int Create(string name)
        {
            var group = new TblGroups { Name = name };
            ServerModel.DB.Insert(group);

            int uID = ServerModel.User.Current.ID;
            PermissionsManager.Grand(group, FxGroupOperations.ChangeMembers, uID, null, DateTimeInterval.Full);
            PermissionsManager.Grand(group, FxGroupOperations.Rename, uID, null, DateTimeInterval.Full);
            PermissionsManager.Grand(group, FxGroupOperations.View, uID, null, DateTimeInterval.Full);

            return group.ID;
        }
    }
}
