using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IUDICO.UnitTest.Base;

namespace IUDICO.UnitTest
{
    [TestFixture]
    public class WebTest: TestFixtureWeb
    {
        [Test]
        public void Test001()
        {
            Selenium.Open("http://localhost/TestPage1.htm");

            Assert.AreEqual("Selenium Target Page",
                            Selenium.GetTitle(),
                            "Check the title of the browser");
        }
    }
}
