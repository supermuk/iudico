using System;
using System.Text;
using NUnit.Framework;
using Selenium;

namespace IUDICO.UnitTests.UserManagement.Selenium
{
	[TestFixture]
	public class Users
	{
		private ISelenium selenium;
		private StringBuilder verificationErrors;

		[SetUp]
		public void SetupTest()
		{
			selenium = new DefaultSelenium("localhost", 4444, "*chrome", UpgradeSeleniumTester.browserURL);
			selenium.Start();
			verificationErrors = new StringBuilder();
		}

		[TearDown]
		public void TeardownTest()
		{
			try
			{
				selenium.Stop();
			}
			catch (Exception)
			{
				// Ignore errors if unable to close the browser
			}

			Assert.AreEqual("", verificationErrors.ToString());
		}

		[Test]
		public void InitTable()
		{
			selenium.Open("/");
			selenium.Type("id=loginPassword", "lex");
			selenium.Type("id=loginUsername", "lex");
			selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
			selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
			selenium.Click("//a[contains(@href, '/User/Index')]");
			selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
			System.Threading.Thread.Sleep(3000);
			Assert.IsTrue(selenium.IsElementPresent("id=myDataTable_length"));
		}

		[Test]
		public void InitTableWithUsers()
		{
			selenium.Open("/");
			selenium.Type("id=loginPassword", "lex");
			selenium.Type("id=loginUsername", "lex");
			selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
			selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
			selenium.Click("//a[contains(@href, '/User/Index')]");
			selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
			System.Threading.Thread.Sleep(3000);
			Assert.IsTrue(selenium.IsElementPresent("id=myDataTable_info"));
		}

		[Test]
		public void InitSortableTable()
		{
			selenium.Open("/");
			selenium.Type("id=loginPassword", "lex");
			selenium.Type("id=loginUsername", "lex");
			selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
			selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
			selenium.Click("//a[contains(@href, '/User/Index')]");
			selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
			System.Threading.Thread.Sleep(3000);
			Assert.IsTrue(selenium.IsElementPresent("css=div.DataTables_sort_wrapper"));
		}

		[Test]
		public void InitSearch()
		{
			selenium.Open("/");
			selenium.Type("id=loginPassword", "lex");
			selenium.Type("id=loginUsername", "lex");
			selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
			selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
			selenium.Click("//a[contains(@href, '/User/Index')]");
			selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
			System.Threading.Thread.Sleep(3000);
			Assert.IsTrue(selenium.IsElementPresent("id=myDataTable_filter"));
		}

		[Test]
		public void InitRolesContainer()
		{
			selenium.Open("/");
			selenium.Type("id=loginPassword", "lex");
			selenium.Type("id=loginUsername", "lex");
			selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
			selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
			selenium.Click("//a[contains(@href, '/User/Index')]");
			selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
			System.Threading.Thread.Sleep(3000);
			Assert.IsTrue(selenium.IsElementPresent("css=span.ui-icon.ui-icon-triangle-1-e"));
		}

		[Test]
		public void LoadUsersPage()
		{
			selenium.Open("/");
			selenium.Type("id=loginPassword", "lex");
			selenium.Type("id=loginUsername", "lex");
			selenium.Click("//div[@id='logindisplay']/form[2]/input[3]");
			selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
			selenium.Click("//a[contains(@href, '/User/Index')]");
			selenium.WaitForPageToLoad(UpgradeSeleniumTester.browserWait);
			Assert.IsTrue(selenium.IsElementPresent("css=h2"));
		}
	}
}
