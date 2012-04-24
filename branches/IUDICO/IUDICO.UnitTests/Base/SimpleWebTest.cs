using System;
using System.Configuration;
using System.Globalization;
using System.Text;
using Selenium;
using NUnit.Framework;

namespace IUDICO.UnitTests.Base
{
    public class SimpleWebTest
    {
        protected ISelenium selenium;
        protected string seleniumHost;
        protected int seleniumPort;
        protected string seleniumSpeed;
        protected string seleniumWait;
        protected string browserUrl;
        protected BrowserType browser = BrowserType.Chrome;

        protected StringBuilder verificationErrors;

        public SimpleWebTest()
        {
            this.verificationErrors = new StringBuilder();
            this.browser = (BrowserType)Enum.Parse(typeof(BrowserType), ConfigurationManager.AppSettings["SELENIUM_BROWSER"], true);
            this.seleniumHost = ConfigurationManager.AppSettings["SELENIUM_HOST"];
            this.seleniumPort = int.Parse(ConfigurationManager.AppSettings["SELENIUM_PORT"], CultureInfo.InvariantCulture);
            this.seleniumSpeed = ConfigurationManager.AppSettings["SELENIUM_SPEED"];
            this.seleniumWait = ConfigurationManager.AppSettings["SELENIUM_WAIT"];
            this.browserUrl = ConfigurationManager.AppSettings["SELENIUM_URL"];

            string browserExe;

            switch (this.browser)
            {
                case BrowserType.InternetExplorer:
                    browserExe = "*iexplore";
                    break;
                case BrowserType.Firefox:
                    browserExe = "*firefox";
                    break;

                case BrowserType.Chrome:
                    browserExe = "*chrome";
                    break;

                default:
                    throw new NotSupportedException();
            }

            this.selenium = new DefaultSelenium(this.seleniumHost, this.seleniumPort, browserExe, this.browserUrl);
        }

        [SetUp]
        public virtual void SetupTest()
        {
            this.selenium.Start();

            if (false == string.IsNullOrEmpty(this.seleniumSpeed))
            {
                this.selenium.SetSpeed(this.seleniumSpeed);
            }
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                this.selenium.Stop();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }

            Assert.AreEqual(string.Empty, this.verificationErrors.ToString());
        }
    }
}