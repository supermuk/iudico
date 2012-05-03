using System;
using IUDICO.CourseManagement.Models;
using NUnit.Framework;

namespace IUDICO.UnitTests.CourseManagement.NUnit
{
    [TestFixture]
    public class DateFormatConverterTests : BaseCourseManagementTest
    {
        [Test]
        public void LastYearTest()
        {
            DateTime lastYear = new DateTime(2011, 12, 22);
            string expected = DateFormatConverter.DataConvert(lastYear);
            Assert.AreEqual("12/22/11", expected);
        }

        [Test]
        public void ThisYearTest()
        {
            if (DateTime.Now.Month != 1 && DateTime.Now.Day != 1)
            {
                DateTime thisYear = new DateTime(DateTime.Now.Year, 1, 1);
                string expected = DateFormatConverter.DataConvert(thisYear);
                Assert.AreEqual("Січ 1", expected);
            }
            else
            {
                DateTime thisYear = new DateTime(DateTime.Now.Year, 1, 2);
                string expected = DateFormatConverter.DataConvert(thisYear);
                Assert.AreEqual("Січ 2", expected);
            }
        }
        [Test]
        public void TodayTest()
        {
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 1);
            string expected = DateFormatConverter.DataConvert(today);
            Assert.AreEqual("12:00 AM", expected);

            if (DateTime.Now.Month != 1)
            {
                today = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.Now.Day, 0, 0, 1);
                expected = DateFormatConverter.DataConvert(today);
                Assert.AreNotEqual("12:00 AM", expected);
            }
            else
            {
                today = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, DateTime.Now.Day, 0, 0, 1);
                expected = DateFormatConverter.DataConvert(today);
                Assert.AreNotEqual("12:00 AM", expected);
            }
        }
    }
}
