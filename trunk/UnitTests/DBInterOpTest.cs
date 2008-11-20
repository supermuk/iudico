using System;
using System.Collections.Generic;
using System.Data.Linq;
using IUDICO.DataModel;
using IUDICO.DataModel.DB;
using IUDICO.UnitTest.Base;
using NUnit.Framework;

namespace IUDICO.UnitTest
{
    [TestFixture]
    public class DBInterOpTest : TestFixtureBase
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

        protected override bool NeedToRecreateDB
        {
            get
            {
                return false;
            }
        }
    }
}