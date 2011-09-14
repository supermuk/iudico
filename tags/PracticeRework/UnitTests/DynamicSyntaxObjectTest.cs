using System.ComponentModel;
using IUDICO.DataModel.Common;
using IUDICO.UnitTest.Base;
using NUnit.Framework;

namespace IUDICO.UnitTest
{
    [TestFixture]
    public class DynamicSyntaxObjectTest : TestFixture
    {
        [Test]
        public void TestDynamicObject()
        {
            var view = new DynamicClassView();
            view.DefineProperty("prop1", typeof(string));
            view.DefineProperty("p2", typeof(int));
            var o = view.Add();

            var props1 = TypeDescriptor.GetProperties(o);
            Assert.AreEqual(2, props1.Count);

            var props2 = view.GetItemProperties(null);
            Assert.AreEqual(2, props2.Count);

            Assert.AreEqual(props1, props2);
        }
    }
}
