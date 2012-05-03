using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;

namespace IUDICO.Common.Models.Services
{
    public interface IUserService : IService
    {
        #region User methods

        IEnumerable<User> GetUsersByGroup(Group group);
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsers(Func<User, bool> predicate);
         
        User GetCurrentUser();
        IEnumerable<Group> GetGroupsByUser(User user);

        #endregion

        IEnumerable<Role> GetCurrentUserRoles();

        #region Group Methods

        IEnumerable<Group> GetGroups();
        Group GetGroup(int id);

        #endregion

        IEnumerable<UserTopicRating> GetRatings(User user, IEnumerable<int> topicIds);
    }
}
