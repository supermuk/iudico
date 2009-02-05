﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Web;
using IUDICO.DataModel;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using IUDICO.DataModel.Security;
using IUDICO.UnitTest.Base;
using NUnit.Framework;

namespace IUDICO.UnitTest
{
    [TestFixture]
    public class DBInterOpTest : TestFixtureDB
    {
        [Test]
        public void SimpleDBOperationsTest()
        {
            var r = new TblPermissions
            {
                CanBeDelagated = false
            };
            var r2 = new TblPermissions
            {
                CanBeDelagated = false
            };
            var r3 = new TblPermissions
            {
                CanBeDelagated = false
            };
            ServerModel.DB.Insert(r);
            ServerModel.DB.Insert<TblPermissions>(new[] { r2, r3 });
            var id1 = r.ID;
            var id2 = r2.ID;
            var id3 = r3.ID;

            r = ServerModel.DB.Load<TblPermissions>(id1);
            Assert.IsNotNull(r);
            Assert.IsFalse(r.CanBeDelagated);
            var dt1 = DateTime.Now;
            r.DateTill = dt1;
            ServerModel.DB.Update(r);

            r = ServerModel.DB.Load<TblPermissions>(id1);
            AreEqual(r.DateTill, dt1);

            IList<TblPermissions> ls = ServerModel.DB.Load<TblPermissions>(new[] { id2, id3 });
            ls[0].DateSince = dt1;
            ls[1].DateTill = dt1;
            ServerModel.DB.Update(ls);

            ls = ServerModel.DB.Load<TblPermissions>(new[] {id2, id3});
            AreEqual(ls[0].DateSince, dt1);
            AreEqual(ls[1].DateTill, dt1);
        }
        
        [Test]
        public void BinaryDataOperationsTest()
        {
            var fileContent = new byte[] { 13, 13, 13, 13 };
            var file = new TblFiles
            {
                File = new Binary(fileContent),
            };
            ServerModel.DB.Insert(file);
            var id1 = file.ID;

            var loadedFile = ServerModel.DB.Load<TblFiles>(id1);
            Assert.AreEqual(loadedFile.File.ToArray(), fileContent);

            loadedFile.File = null;
            ServerModel.DB.Update(loadedFile);

            loadedFile = ServerModel.DB.Load<TblFiles>(id1);
            Assert.IsNull(loadedFile.File);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ChangeDataObjectIDTest()
        {
            var t = new TblQuestions();
            t.IsCompiled = true;
            t.ID = 5;
        }

        [Test]
        public void TestFxObject()
        {
            ReadOnlyCollection<FxCourseOperations> f = ServerModel.DB.Fx<FxCourseOperations>();
            Assert.IsNotNull(f);
        }

        [Test]
        [ExpectedException(typeof(DMError))]
        public void TestFxObjectToImutable()
        {
            var co = new FxCourseOperations();
            co.Name = "test";
        }

        [Test]
        public void LookupIDsTest()
        {
            var d = DateTime.Now;
            string email = d.Year.ToString() + d.Month + d.Day + d.Hour + d.Minute + d.Second + d.Millisecond + "@gmail.com";
            var u = new TblUsers
            {
                Email = email,
                FirstName = "Test",
                LastName = "Test",
                Login = email,
                PasswordHash = email
            };
            ServerModel.DB.Insert(u);

            var a1 = new TblUserAnswers { UserRef = u.ID };
            var a2 = (TblUserAnswers)a1.Clone();
            var a3 = (TblUserAnswers)a1.Clone();
            ServerModel.DB.Insert<TblUserAnswers>(new[] {a1, a2, a3});

            var ids = ServerModel.DB.LookupIds<TblUserAnswers>(u, null);
            Assert.AreEqual(ids, new[] { a1.ID, a2.ID, a3.ID });

            var g1 = new TblGroups
             {
                 Name = "TestGroup1"
             };
            var g2 = (TblGroups) g1.Clone();
            ServerModel.DB.Insert<TblGroups>(new[] {g1, g2});

            ServerModel.DB.Link(u, g1);
            ServerModel.DB.Link(g2, u);

            var uIds = ServerModel.DB.LookupMany2ManyIds<TblGroups>(u, null);
            Assert.AreEqual(new[] { g1.ID, g2.ID }, uIds);

            var g1Ids = ServerModel.DB.LookupMany2ManyIds<TblUsers>(g1, null);
            Assert.AreEqual(new[] {u.ID}, g1Ids);

            var g2Ids = ServerModel.DB.LookupMany2ManyIds<TblUsers>(g2, null);
            Assert.AreEqual(new[] {u.ID}, g2Ids);

            ServerModel.DB.UnLink(g1, u);
            var newIds = ServerModel.DB.LookupMany2ManyIds<TblGroups>(u, null);
            Assert.AreEqual(new[] {g2.ID}, newIds);
        }

        [Test]
        public void SubSelectConditionTest()
        {
            var user = new TblUsers
            {
                Email = new Random().Next() + "@" + new Random().Next() + "." + new Random().Next(),
                FirstName = "Test",
                LastName = "User",
                PasswordHash = "hello"
            };

            user.Login = user.Email;
            ServerModel.DB.Insert(user);

            var group1 = new TblGroups
            {
                Name = user.Email + "_g1"
            };
            var group2 = new TblGroups
            {
                Name = user.Email + "_g2"
            };
            ServerModel.DB.Insert((IList<TblGroups>)new[] {group1, group2});

            ServerModel.DB.Link(user, group1);

            var groupIds = ServerModel.DB.LookupMany2ManyIds<TblGroups>(user, null);
            Assert.AreEqual(1, groupIds.Count);
            Assert.Contains(group1.ID, groupIds);

            var groups = ServerModel.DB.Query<TblGroups>(new InCondition(
                DataObject.Schema.ID,
                new SubSelectCondition<RelUserGroups>("GroupRef", 
                    new CompareCondition(
                        DataObject.Schema.UserRef,
                        new ValueCondition<int>(user.ID),
                        COMPARE_KIND.EQUAL
                    )
                ),
                IN_CONDITION_KIND.NOT_IN
            ));

            Assert.Greater(groups.Count, 0);
            Assert.IsNotNull(groups.Find(p => p.ID == group2.ID));
        }

        [Test]
        public void GetGroupPermissionsTest()
        {
            var c = new TblCourses
            {
                Name = "test course",
                Description = "unit test course"
            };
            ServerModel.DB.Insert(c);

            var g = new TblGroups
            {
                Name = "test group"
            };
            ServerModel.DB.Insert(g);

            PermissionsManager.Grand(c, FxCourseOperations.Edit, null, g.ID, new DateTimeInterval(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1)));

            var ids = PermissionsManager.GetObjectsForGroup(SECURED_OBJECT_TYPE.COURSE, g.ID, FxCourseOperations.Edit.ID, DateTime.Now);
            Assert.AreEqual(1, ids.Count);
            Assert.AreEqual(c.ID, ids[0]);

            ids = PermissionsManager.GetObjectsForGroup(SECURED_OBJECT_TYPE.COURSE, g.ID, null, null);
            Assert.AreEqual(1, ids.Count);
            Assert.AreEqual(c.ID, ids[0]);
        }

        protected override bool NeedToRecreateDB
        {
            get
            {
                return false;
            }
        }
    }
}