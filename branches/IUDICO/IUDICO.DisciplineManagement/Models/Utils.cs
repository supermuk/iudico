using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using System.Linq;

namespace IUDICO.DisciplineManagement.Models
{
    /// <summary>
    /// Utility class.
    /// </summary>
    public static class Utils
    {
        public static IList<ShareUser> ToShareUsers(this IEnumerable<User> users, bool isShared)
        {
            return users.Select(user => new ShareUser
                {
                    Id = user.Id,
                    Name = user.Name,
                    Roles = user.Roles.Select(i => i.ToString()).ToArray(),
                    Shared = isShared
                }
            )
            .ToList();
        }
    }
}