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

        protected void DefaultLogin(string username, string password)
        {
            this.selenium.Open("/");
            this.selenium.Type("id=loginPassword", username);
            this.selenium.Type("id=loginUsername", password);
            this.selenium.Click("//form[contains(@action, '/Account/LoginDefault')]/input[3]");
            this.selenium.WaitForPageToLoad(this.seleniumWait);
        }

        protected void DefaultLogin()
        {
            this.DefaultLogin("lex", "lex");
        }

        protected void LoginOpenId(string openId, string openIdLogin, string openIdPass)
        {
            this.selenium.Type("id=loginIdentifier", openId);
            this.selenium.Click("//form[contains(@action, '/Account/Login')]/input[2]");
            this.selenium.WaitForPageToLoad(this.seleniumWait);

            if (!this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"))
            {
                this.selenium.Type("id=login_user", openIdLogin);
                this.selenium.Type("id=login_password", openIdPass);
                this.selenium.Click("//input[@id='loginlj_submit']");
                this.selenium.WaitForPageToLoad(this.seleniumWait);

                if (this.selenium.GetLocation().Contains("http://www.livejournal.com"))
                {
                    this.selenium.Click("//input[@name='yes:once']");
                    this.selenium.WaitForPageToLoad(this.seleniumWait);
                }
            }
        }
    }
}