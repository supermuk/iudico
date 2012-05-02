using System;
using System.Configuration;
using System.Globalization;
using System.Text;

using NUnit.Framework;

using Selenium;

namespace IUDICO.UnitTests.Base
{
    public class SimpleWebTest
    {
        public enum BrowserType
        {
            /// <summary>
            /// Internet Explorer browser.
            /// </summary>
            InternetExplorer, 

            /// <summary>
            /// Firefox browser.
            /// </summary>
            Firefox, 

            /// <summary>
            /// Chrome browser
            /// </summary>
            Chrome
        }

        protected ISelenium selenium;

        protected string seleniumHost;

        protected int seleniumPort;

        protected string seleniumSpeed;

        protected int seleniumWait;

        protected string browserUrl;

        protected BrowserType browser = BrowserType.Chrome;

        protected StringBuilder verificationErrors;

        protected string SeleniumWait
        {
            get
            {
                return this.seleniumWait.ToString();
            }
        }

        public SimpleWebTest()
        {
            this.verificationErrors = new StringBuilder();
            this.browser =
                (BrowserType)Enum.Parse(typeof(BrowserType), ConfigurationManager.AppSettings["SELENIUM_BROWSER"], true);
            this.seleniumHost = ConfigurationManager.AppSettings["SELENIUM_HOST"];
            this.seleniumPort = int.Parse(
                ConfigurationManager.AppSettings["SELENIUM_PORT"], CultureInfo.InvariantCulture);
            this.seleniumSpeed = ConfigurationManager.AppSettings["SELENIUM_SPEED"];
            this.seleniumWait = int.Parse(
                ConfigurationManager.AppSettings["SELENIUM_WAIT"], CultureInfo.InvariantCulture);
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
                this.Logout();
            }
            catch
            {
            }

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

            if (this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"))
            {
                return;
            }

            this.selenium.Type("id=loginPassword", username);
            this.selenium.Type("id=loginUsername", password);
            this.selenium.Click("id=loginDefaultButton");

            this.selenium.WaitForPageToLoad(this.SeleniumWait);
        }

        protected void DefaultLogin()
        {
            this.DefaultLogin("lex", "lex");
        }

        protected void LoginOpenId(string openId, string openIdLogin, string openIdPass)
        {
            this.selenium.Open("/");
            this.selenium.Type("id=loginIdentifier", openId);
            this.selenium.Click("id=loginOpenIdButton");
            this.selenium.WaitForPageToLoad((this.seleniumWait * 5).ToString());

            if (this.selenium.IsElementPresent("//a[contains(@href, '/Account/Index')]"))
            {
                return;
            }

            if (!this.selenium.IsElementPresent("//input[@id='loginlj_submit']"))
            {
                return;
            }

            this.selenium.Type("id=login_user", openIdLogin);
            this.selenium.Type("id=login_password", openIdPass);
            this.selenium.Click("//input[@id='loginlj_submit']");

            this.selenium.WaitForPageToLoad((this.seleniumWait * 6).ToString());

            if (this.selenium.GetLocation().Contains("http://www.livejournal.com"))
            {
                this.selenium.Click("//input[@name='yes:once']");

                this.selenium.WaitForPageToLoad((this.seleniumWait * 6).ToString());
            }
        }

        protected void Logout()
        {
            if (this.selenium.IsElementPresent("//a[contains(@href, '/Account/Logout')]"))
            {
                this.selenium.Click("//a[contains(@href, '/Account/Logout')]");
                this.selenium.WaitForPageToLoad(this.SeleniumWait);
            }
        }
    }
}