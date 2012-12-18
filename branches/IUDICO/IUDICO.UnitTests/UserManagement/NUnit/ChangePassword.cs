using IUDICO.Common.Models.Shared;
using IUDICO.UserManagement.Models;

using NUnit.Framework;

namespace IUDICO.UnitTests.UserManagement.NUnit
{
    using System;

    [TestFixture]
    public class ChangePassword
    {
        protected UserManagementTests tests = UserManagementTests.GetInstance();

        [Test]
        public void ChangePasswordCorrect()
        {
            tests = UserManagementTests.Update();
            var model = new ChangePasswordModel { OldPassword = "123", ConfirmPassword = "321", NewPassword = "321" };
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.ChangeCurrentUser("panza");
            this.tests.Storage.CreateUser(temp);
            this.tests.ChangeCurrentUser("name");
            this.tests.Storage.ChangePassword(model);

            Assert.AreEqual(
                this.tests.Storage.GetUser("name").Password,
                this.tests.Storage.EncryptPassword("321"));
            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }
        
        /// <summary>
        /// Incorrect old password.
        /// </summary>
        [Test]
        public void ChangePasswordWithInvalidDataTest()
        {
            this.tests = UserManagementTests.Update();
            var model = new ChangePasswordModel { OldPassword = "111", ConfirmPassword = "321", NewPassword = "321" };
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.ChangeCurrentUser("panza");
            this.tests.Storage.CreateUser(temp);
            this.tests.ChangeCurrentUser("name");

            try
            {
                this.tests.Storage.ChangePassword(model);
            }
            catch (Exception e)
            {
                this.tests.Storage.DeleteUser(u => u.Username == "name");

                if (e.Message == "Old password is wrong.")
                {
                    Assert.Pass();
                }

                Assert.Fail();
            }

            this.tests.Storage.DeleteUser(u => u.Username == "name");
            Assert.Fail();
        }

        /// <summary>
        /// Empty old password.
        /// </summary>
        [Test]
        public void ChangePasswordWithBlankDataTest()
        {
            this.tests = UserManagementTests.Update();
            var model = new ChangePasswordModel { OldPassword = string.Empty, ConfirmPassword = "321", NewPassword = "321" };
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.ChangeCurrentUser("panza");
            this.tests.Storage.CreateUser(temp);
            this.tests.ChangeCurrentUser("name");

            try
            {
                this.tests.Storage.ChangePassword(model);
            }
            catch (Exception e)
            {
                this.tests.Storage.DeleteUser(u => u.Username == "name");

                if (e.Message == "Old password can't be empty.")
                {
                    Assert.Pass();
                }

                Assert.Fail();
            }

            this.tests.Storage.DeleteUser(u => u.Username == "name");
            Assert.Fail();
        }

        [Test]
        [ExpectedException(typeof(Exception), ExpectedMessage = "Password can't be empty.")]
        public void ChangePasswordWithBlankNewPassword()
        {
            this.tests = UserManagementTests.Update();
            var model = new ChangePasswordModel { OldPassword = "123", ConfirmPassword = "321", NewPassword = string.Empty };
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.ChangeCurrentUser("panza");
            this.tests.Storage.CreateUser(temp);
            this.tests.ChangeCurrentUser("name");

            this.tests.Storage.ChangePassword(model);

            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        [ExpectedException(typeof(Exception), ExpectedMessage = "Password confirmation can't be empty.")]
        public void ChangePasswordWithBlankConfirmPassword()
        {
            this.tests = UserManagementTests.Update();
            var model = new ChangePasswordModel { OldPassword = "123", ConfirmPassword = string.Empty, NewPassword = "321" };
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.ChangeCurrentUser("panza");
            this.tests.Storage.CreateUser(temp);
            this.tests.ChangeCurrentUser("name");

            this.tests.Storage.ChangePassword(model);

            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        [Test]
        [ExpectedException(typeof(Exception), ExpectedMessage = "New password should be the same as password confirmation.")]
        public void ChangePasswordWithInvalidConfirmPassword()
        {
            this.tests = UserManagementTests.Update();
            var model = new ChangePasswordModel { OldPassword = "123", ConfirmPassword = "111", NewPassword = "321" };
            var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

            this.tests.ChangeCurrentUser("panza");
            this.tests.Storage.CreateUser(temp);
            this.tests.ChangeCurrentUser("name");

            this.tests.Storage.ChangePassword(model);

            this.tests.Storage.DeleteUser(u => u.Username == "name");
        }

        //[Test]
        //public void ChangePasswordIncorrect()
        //{
        //    tests = UserManagementTests.Update();
        //    var model = new ChangePasswordModel { OldPassword = "123", ConfirmPassword = "323", NewPassword = "321" };
        //    var temp = new User { Username = "name", Email = "mail@mail.com", Password = "123" };

        //    this.tests.ChangeCurrentUser("panza");
        //    this.tests.Storage.CreateUser(temp);
        //    this.tests.ChangeCurrentUser("name");
        //    this.tests.Storage.ChangePassword(model);

        //    Assert.AreEqual(
        //        this.tests.Storage.GetUser("name").Password, 
        //        this.tests.Storage.EncryptPassword("123"));
        //    this.tests.Storage.DeleteUser(u => u.Username == "name");
        //}

    }
}