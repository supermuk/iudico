using System.Collections.Generic;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    public class Admin_EditGroupController : ControllerBase
    {
        [ControllerParameter]
        public int GroupID;

        [PersistantField] 
        public TblGroups Group;

        [PersistantField] 
        public IVariable<string> GroupName = string.Empty.AsVariable();

        public override void Initialize()
        {
            base.Initialize();
            Group = ServerModel.DB.Load<TblGroups>(GroupID);
            GroupName.Value = Group.Name;
        }

        public IList<TblUsers> GetUsers()
        {
            return ServerModel.DB.Load<TblUsers>(ServerModel.DB.LookupMany2ManyIds<TblUsers>(Group, null));
        }

        public void ApplyChanges()
        {
            if (Group.Name != GroupName.Value)
            {
                Group.Name = GroupName.Value;
                ServerModel.DB.Update(Group);
            }
        }
    }
}
