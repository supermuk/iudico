using IUDICO.Common;
using NUnit.Framework;
using System.IO;

namespace IUDICO.UnitTests.LMS.NUnit
{
    [TestFixture]
    class LoggerTests
    {
        [Test]
        public void IsLoggerObjectExists()
        {
            bool ident = false;
            Logger logger = Logger.Instance;

            if (logger == null)
            {
                ident = false;
            }
            else
            {
                ident = true;
            }

            Assert.AreEqual(ident, true);
        }

        /*[Test]
        public void WriteInfoToFile()
        {
            FileStream fs = new FileStream("testInfo.txt", FileMode.Create);

            if (File.Exists("testInfo.txt"))
            {
                Logger.Instance.Info(this, "Logging Info message");
            }

            bool ident = false;

            if (fs.Length == 0)
            {
                ident = false;
            }
            else
            {
                ident = true;
            }

            fs.Close();

            Assert.AreEqual(ident, true);
        }*/
    }
}