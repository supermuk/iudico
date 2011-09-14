using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models;

namespace IUDICO.UnitTests.Fakes
{
    public class FakeUserService : IUserService
    {
        List<Group> groups { get; set; }
        List<User> users { get; set; }

        public FakeUserService()
        {
            groups = new List<Group>();
            groups.Add(new Group() { Name = "Group1", Id = 1 });
            groups.Add(new Group() { Name = "Group2", Id = 2 });
            groups.Add(new Group() { Name = "Group3", Id = 3 });

            users = new List<User>();
            users.Add(new User() { Username = "User1" });
            users.Add(new User() { Username = "User2" });
            users.Add(new User() { Username = "User3" });
        }

        public IEnumerable<Role> GetRoles()
        {
            throw new NotImplementedException();
        }

        public Role GetRole(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsersByGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            return users;
        }

        public User GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetGroupsByUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetGroups()
        {
            return groups;
        }

        public Group GetGroup(int id)
        {
            return groups[id - 1];
        }
    }
}
