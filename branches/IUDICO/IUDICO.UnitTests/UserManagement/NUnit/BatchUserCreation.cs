using System.Collections.Generic;
using System.IO;
using System.Linq;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Notifications;
using Moq;
using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    [TestFixture]
    public class BatchUserCreation
    {
        protected UserManagementTests _Tests = UserManagementTests.GetInstance();

        [Test]
        public void CreateUsersFromCSV()
        {
            _Tests = UserManagementTests.GetInstance();
            var path = Path.Combine(Path.GetTempPath(), "users.csv");
            var text = "Username,Email\nipe,ip@interlogic.com.ua\nvladykx,vladykx@gmail.com";

            File.WriteAllText(path, text);

            var users = new List<User>
                            {
                                new User {Username = "ipe", Email = "ip@interlogic.com.ua"},
                                new User {Username = "vladykx", Email = "vladykx@gmail.com"},
                            };

            _Tests.MockStorage.Setup(s => s.GetCurrentUser()).Returns(_Tests.Storage.GetUser(u => u.Username == "panza"));

            _Tests.Storage.CreateUsersFromCSV(path);

            Assert.IsTrue(_Tests.TestUsers(_Tests.Storage.GetUsers(), users));
        }
    }
}
