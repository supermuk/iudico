using System;
using System.Collections.Generic;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    public class Admin_EditUserController : ControllerBase
    {
        public override void Initialize()
        {
            base.Initialize();

            User = ServerModel.DB.Load<TblUsers>(UserID);
            var roles = ServerModel.User.ByLogin(User.Login).Roles;

            StudentRoleChecked.Value = roles.Contains(FxRoles.STUDENT.Name);
            TrainerRoleChecked.Value = roles.Contains(FxRoles.TRAINER.Name);
            LectorRoleChecked.Value = roles.Contains(FxRoles.LECTOR.Name);
            AdminRoleChecked.Value = roles.Contains(FxRoles.ADMIN.Name);
            SuperAdminRoleChecked.Value = roles.Contains(FxRoles.SUPER_ADMIN.Name);
        }

        public void ApplyRoles()
        {
            var roles = ServerModel.DB.LookupMany2ManyIds<FxRoles>(User, null);
            Action<IValue<bool>, FxRoles> updateProc = (v, r) =>
            {
                if (v.Value != roles.Contains(r.ID))
                {
                    if (v.Value)
                    {
                        ServerModel.DB.Link(User, r);
                    }
                    else
                    {
                        ServerModel.DB.UnLink(User, r);
                    }
                }
            };

            updateProc(StudentRoleChecked, FxRoles.STUDENT);
            updateProc(TrainerRoleChecked, FxRoles.TRAINER);
            updateProc(LectorRoleChecked, FxRoles.LECTOR);
            updateProc(AdminRoleChecked, FxRoles.ADMIN);
            updateProc(SuperAdminRoleChecked, FxRoles.SUPER_ADMIN);
            ServerModel.User.NotifyUpdated(User);
        }

        public IList<TblGroups> GetGroups()
        {
            return ServerModel.DB.Load<TblGroups>(ServerModel.DB.LookupMany2ManyIds<TblGroups>(User, null));
        }

        [PersistantField]
        public readonly IVariable<bool> StudentRoleChecked = false.AsVariable();
        [PersistantField]
        public readonly IVariable<bool> TrainerRoleChecked = false.AsVariable();
        [PersistantField]
        public readonly IVariable<bool> LectorRoleChecked = false.AsVariable();
        [PersistantField]
        public readonly IVariable<bool> AdminRoleChecked = false.AsVariable();
        [PersistantField]
        public readonly IVariable<bool> SuperAdminRoleChecked = false.AsVariable();

        [PersistantField]
        public TblUsers User;

        [ControllerParameter]
        public int UserID;
    }

}
