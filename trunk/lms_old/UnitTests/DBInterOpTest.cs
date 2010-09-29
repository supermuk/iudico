using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
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
            using (var c = new DataObjectCleaner())
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
                c.Insert(r);
                c.Insert<TblPermissions>(new[] {r2, r3});
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

                IList<TblPermissions> ls = ServerModel.DB.Load<TblPermissions>(new[] {id2, id3});
                ls[0].DateSince = dt1;
                ls[1].DateTill = dt1;
                ServerModel.DB.Update(ls);

                ls = ServerModel.DB.Load<TblPermissions>(new[] {id2, id3});
                AreEqual(ls[0].DateSince, dt1);
                AreEqual(ls[1].DateTill, dt1);
            }
        }
        
        [Test]
        public void BinaryDataOperationsTest()
        {
            using (var c = new DataObjectCleaner())
            {
                var fileContent = new byte[] { 13, 13, 13, 13 };
                var file = new TblFiles
                               {
                                   File = new Binary(fileContent),
                               };
                c.Insert(file);
                var id1 = file.ID;

                var loadedFile = ServerModel.DB.Load<TblFiles>(id1);
                Assert.AreEqual(loadedFile.File.ToArray(), fileContent);

                loadedFile.File = null;
                ServerModel.DB.Update(loadedFile);

                loadedFile = ServerModel.DB.Load<TblFiles>(id1);
                Assert.IsNull(loadedFile.File);
            }
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
            using (var c = new DataObjectCleaner())
            {
                TblUsers u = GetUniqueUserForTesting();
                c.Insert(u);

                var a1 = new TblUserAnswers 
                { 
                    UserRef = u.ID, 
                    AnswerTypeRef = FxAnswerType.EmptyAnswer.ID 
                };
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
                c.Insert<TblGroups>(new[] {g1, g2});

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
        }

        [Test]
        public void SubSelectConditionTest()
        {
            using (var c = new DataObjectCleaner())
            {
                var user = GetUniqueUserForTesting();
                c.Insert(user);

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

                var groups = ServerModel.DB.Query<TblGroups>(new InCondition<int>(
                     DataObject.Schema.ID,
                     new SubSelectCondition<RelUserGroups>("GroupRef", 
                                                           new CompareCondition<int>(
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
        }

        [Test]
        public void GetGroupPermissionsTest()
        {
            using (var cl = new DataObjectCleaner())
            {
                var c = new TblCourses
                            {
                                Name = "test course",
                                Description = "unit test course"
                            };
                cl.Insert(c);

                var g = new TblGroups
                            {
                                Name = "test group"
                            };
                cl.Insert(g);

                PermissionsManager.Grand(c, FxCourseOperations.Modify, null, g.ID, new DateTimeInterval(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1)));

                var ids = PermissionsManager.GetObjectsForGroup(SECURED_OBJECT_TYPE.COURSE, g.ID, FxCourseOperations.Modify.ID, DateTime.Now);
                Assert.AreEqual(1, ids.Count);
                Assert.AreEqual(c.ID, ids[0]);

                ids = PermissionsManager.GetObjectsForGroup(SECURED_OBJECT_TYPE.COURSE, g.ID, null, null);
                Assert.AreEqual(1, ids.Count);
                Assert.AreEqual(c.ID, ids[0]);
            }
        }

        [Test]
        public void SoftDeleteTest()
        {
            var obj = new TblGroups {Name = "test group"};
            ServerModel.DB.Insert(obj);

            var id = obj.ID;
            Assert.Greater(id, 0);

            ServerModel.DB.Delete<TblGroups>(id);

            // Check that object exists in database
            var groups = new List<TblGroups>(from g in ServerModel.DB.TblGroups where g.ID == id select g);
            Assert.AreEqual(1, groups.Count);

            // Check that it is not retrieving via query
            var objs = ServerModel.DB.Query<TblGroups>(
                new CompareCondition<int>(
                    DataObject.Schema.ID, 
                    new ValueCondition<int>(id),
                    COMPARE_KIND.EQUAL));
            Assert.IsEmpty(objs);

            // ... and via Load
            var ok = false;
            try
            {
                ServerModel.DB.Load<TblGroups>(id);
            }
            catch
            {
                ok = true;
            }
            Assert.IsTrue(ok);
        }

        [Test]
        public void SoftDeleteLookupCascadeDeleteTest()
        {
            using (var c = new DataObjectCleaner())
            {
                var u = GetUniqueUserForTesting();
                var g = new TblGroups {Name = "test group"};

                ServerModel.DB.Insert(u);
                c.Insert(g);

                ServerModel.DB.Link(u, g);

                ServerModel.DB.Delete<TblUsers>(u.ID);

                var ids = ServerModel.DB.LookupMany2ManyIds<TblUsers>(g, null);
                Assert.AreEqual(0, ids.Count);
            }
        }

        [Test]
        public void GetUsersAndGroupForObjectTest()
        {
            using (var c = new DataObjectCleaner())
            {
                var g = new TblGroups {Name = "test group"};
                c.Insert(g);
                
                var user = GetUniqueUserForTesting();
                c.Insert(user);

                var group = new TblGroups {Name = "test owner group"};
                c.Insert(group);

                PermissionsManager.Grand(g, FxGroupOperations.View, user.ID, null, DateTimeInterval.Full);
                PermissionsManager.Grand(g, FxGroupOperations.ChangeMembers, null, group.ID, DateTimeInterval.Full);

                var ids = PermissionsManager.GetUsersForObject(SECURED_OBJECT_TYPE.GROUP, g.ID, null, null);
                Assert.AreEqual(1, ids.Count);
                Assert.AreEqual(user.ID, ids[0]);

                var gids = PermissionsManager.GetGroupsForObject(SECURED_OBJECT_TYPE.GROUP, g.ID, null, null);
                Assert.AreEqual(1, ids.Count);
                Assert.AreEqual(group.ID, gids[0]);
            }
        }

        [Test]
        public void SafeDeleteAndPermissionManagerTest_GetObjectsForUser()
        {
            using (var c = new DataObjectCleaner())
            {
                var u = GetUniqueUserForTesting();
                c.Insert(u);

                var g = new TblGroups {Name = "g1"};
                ServerModel.DB.Insert(g);

                PermissionsManager.Grand(g, FxGroupOperations.View, u.ID, null, DateTimeInterval.Full);

                var res1 = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.GROUP, u.ID, null, null);
                Assert.AreEqual(1, res1.Count);
                Assert.AreEqual(g.ID, res1[0]);

                ServerModel.DB.Delete<TblGroups>(g.ID);

                var res2 = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.GROUP, u.ID, null, null);
                Assert.AreEqual(0, res2.Count);
            }
        }

        [Test]
        public void SafeDeleteAndPermissionManagerTest_GetUsersForObject()
        {
            using (var c = new DataObjectCleaner())
            {
                var u = GetUniqueUserForTesting();
                c.Insert(u);

                var g = new TblGroups { Name = "TestGroup1" };
                ServerModel.DB.Insert(g);

                PermissionsManager.Grand(g, FxGroupOperations.View, u.ID, null, DateTimeInterval.Full);
                PermissionsManager.Grand(g, FxGroupOperations.Rename, u.ID, null, DateTimeInterval.Full);

                var res1 = PermissionsManager.GetUsersForObject(SECURED_OBJECT_TYPE.GROUP, g.ID, null, null);
                Assert.AreEqual(1, res1.Count);
                Assert.AreEqual(u.ID, res1[0]);

                ServerModel.DB.Delete<TblUsers>(u.ID);

                var res2 = PermissionsManager.GetUsersForObject(SECURED_OBJECT_TYPE.GROUP, g.ID, null, null);
                Assert.AreEqual(0, res2.Count);
            }
        }

        [Test]
        public void SafeDeleteAndPermissionManagerTest_GetObjectsForGroup()
        {
            using (var c = new DataObjectCleaner())
            {
                var course = new TblCourses { Name = "TestCourse2" };
                ServerModel.DB.Insert(course);

                var g = new TblGroups { Name = "TestGroup2" };
                ServerModel.DB.Insert(g);

                PermissionsManager.Grand(course, FxCourseOperations.Use, null, g.ID, DateTimeInterval.Full);
                PermissionsManager.Grand(course, FxCourseOperations.Modify, null, g.ID, DateTimeInterval.Full);

                var res1 = PermissionsManager.GetObjectsForGroup(SECURED_OBJECT_TYPE.COURSE, g.ID, null, null);
                Assert.AreEqual(1, res1.Count);
                Assert.AreEqual(course.ID, res1[0]);

                ServerModel.DB.Delete<TblCourses>(course.ID);

                var res2 = PermissionsManager.GetObjectsForGroup(SECURED_OBJECT_TYPE.COURSE, g.ID, null, null);
                Assert.AreEqual(0, res2.Count);
            }
        }

        [Test]
        public void SafeDeleteAndPermissionManagerTest_GetGroupsForObject()
        {
            using (var c = new DataObjectCleaner())
            {
                var course = new TblCourses { Name = "TestCourse3" };
                ServerModel.DB.Insert(course);

                var g = new TblGroups { Name = "TestGroup3" };
                ServerModel.DB.Insert(g);

                PermissionsManager.Grand(course, FxCourseOperations.Use, null, g.ID, DateTimeInterval.Full);
                PermissionsManager.Grand(course, FxCourseOperations.Modify, null, g.ID, DateTimeInterval.Full);

                var res1 = PermissionsManager.GetGroupsForObject(SECURED_OBJECT_TYPE.COURSE, course.ID, null, null);
                Assert.AreEqual(1, res1.Count);
                Assert.AreEqual(g.ID, res1[0]);

                ServerModel.DB.Delete<TblCourses>(course.ID);

                var res2 = PermissionsManager.GetGroupsForObject(SECURED_OBJECT_TYPE.COURSE, course.ID, null, null);
                Assert.AreEqual(0, res2.Count);
            }
        }

        [Test]
        public void DelegatePermissionTest()
        {
            using (var c = new DataObjectCleaner())
            {
                var u = GetUniqueUserForTesting();
                c.Insert(u);

                var u2 = GetUniqueUserForTesting();
                c.Insert(u2);

                var g = new TblGroups {Name = "delegated group"};
                c.Insert(g);

                PermissionsManager.Grand(g, FxGroupOperations.Rename, u.ID, null, DateTimeInterval.Full);

                PermissionsManager.Delegate(u.ID, g, FxGroupOperations.Rename, u2.ID, null, DateTimeInterval.Full);

                var GroupIds = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.GROUP, u2.ID, null, null);
                Assert.AreEqual(1, GroupIds.Count);
                Assert.AreEqual(g.ID, GroupIds[0]);

                var courseIds = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.COURSE, u2.ID, null, null);
                Assert.AreEqual(0, courseIds.Count);
            }
        }

        [Test]
        public void GetObjectForUserIncludedInGroupTest()
        {
            using (var c = new DataObjectCleaner())
            {
                var user = GetUniqueUserForTesting();
                c.Insert(user);

                var group = new TblGroups {Name = "test group with user"};
                c.Insert(group);
                ServerModel.DB.Link(user, group);

                var course = new TblCourses
                {
                    Name = "test_course",
                    Description = "test description"
                };
                c.Insert(course);

                PermissionsManager.Grand(course, FxCourseOperations.Use, null, group.ID, DateTimeInterval.Full);
                Assert.AreEqual(0, PermissionsManager.GetObjectsForGroup(SECURED_OBJECT_TYPE.COURSE, group.ID, FxCourseOperations.Modify.ID, null).Count);

                var ids1 = PermissionsManager.GetObjectsForGroup(SECURED_OBJECT_TYPE.COURSE, group.ID, FxCourseOperations.Use.ID, null);
                Assert.AreEqual(1, ids1.Count);
                Assert.AreEqual(course.ID, ids1[0]);

                var ids2 = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.COURSE, user.ID, null, null);                
                Assert.AreEqual(1, ids2.Count);
                Assert.AreEqual(course.ID, ids2[0]);

                var ids3 = PermissionsManager.GetObjectsForUser(SECURED_OBJECT_TYPE.COURSE, user.ID, FxCourseOperations.Use.ID, null);
                Assert.AreEqual(1, ids3.Count);
                Assert.AreEqual(course.ID, ids3[0]);
            }
        }

        protected override bool NeedToRecreateDB
        {
            get
            {
                return false;
            }
        }

        private static TblUsers GetUniqueUserForTesting()
        {
            var d = DateTime.Now;
            string uname = (d.Year%100).ToString() + d.Second + d.Millisecond + d.Ticks + new Random().Next(99);
            return new TblUsers
            {
                Email = uname + "@gmail.com",
                FirstName = "Test",
                LastName = "Test",
                Login = uname,
                PasswordHash = uname
            };
        }
    }
}