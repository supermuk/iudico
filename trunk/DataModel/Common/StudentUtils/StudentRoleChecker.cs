using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Common.StudentUtils
{
    public class StudentRoleChecker
    {
        public static bool IsCurrentUserLector()
        {
            if(ServerModel.User.Current != null)
                if(ServerModel.User.Current.Roles != null)
                    return ServerModel.User.Current.Roles.Contains(FX_ROLE.LECTOR.ToString());

            return false;
        }

        public static bool IsCurrentUserSuperAdmin()
        {
            if (ServerModel.User.Current != null)
                if (ServerModel.User.Current.Roles != null)
                    return ServerModel.User.Current.Roles.Contains(FX_ROLE.SUPER_ADMIN.ToString());

            return false;
        }
    }
}
