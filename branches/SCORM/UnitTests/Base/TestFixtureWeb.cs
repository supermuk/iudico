using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Threading;
using Selenium;
using NUnit.Framework;
using System.Text;

namespace IUDICO.UnitTest.Base
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
    }

    /// <summary>
    /// Tester class for testing Web application using Selenium. The class provides a fluent interface 
    /// for testing.
    /// </summary>
    public class TestFixtureWeb : TestFixtureDB
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumTesterBase"/> class.
        /// </summary>
        public TestFixtureWeb()
            : base()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            browserType = (BrowserType)Enum.Parse(
                                            typeof(BrowserType),
                                            ConfigurationManager.AppSettings["SELENIUM_BROWSER"],
                                            true);
            testMachine = ConfigurationManager.AppSettings["SELENIUM_HOST"];
            seleniumPort = int.Parse(
                ConfigurationManager.AppSettings["SELENIUM_PORT"],
                CultureInfo.InvariantCulture);
            seleniumSpeed = ConfigurationManager.AppSettings["SELENIUM_SPEED"];
            browserUrl = ConfigurationManager.AppSettings["SELENIUM_URL"];

            string browserExe;

            switch (browserType)
            {
                case BrowserType.InternetExplorer:
                    browserExe = "*iexplore";
                    break;
                case BrowserType.Firefox:
                    browserExe = "*firefox";
                    break;

                default:
                    throw new NotSupportedException();
            }

            selenium = new DefaultSelenium(testMachine, seleniumPort, browserExe, browserUrl);
            //selenium.SetTimeout("1000000");

            Console.WriteLine("Started Selenium session (browser type={0})", browserType);
        }

        [TestFixtureSetUp]
        protected override void InitializeFixture()
        {
            base.InitializeFixture();
            verificationErrors = new StringBuilder();
            
            selenium.Start();

            // sets the speed of execution of GUI commands
            if (false == String.IsNullOrEmpty(seleniumSpeed))
                selenium.SetSpeed(seleniumSpeed);
        }

        [TestFixtureTearDown]
        protected override void  FinializeFixture()
        {
            base.FinializeFixture();

            try
            {
                selenium.Stop();
            }
            catch (SeleniumException)
            {

            }
        }

        /// <summary>
        /// Gets the underlying Selenium object.
        /// </summary>
        /// <value>The underlying Selenium object.</value>
        public ISelenium Selenium
        {
            get { return selenium; }
        }

        /// <summary>
        /// Check the Ansfer confirmation
        /// </summary>
        /// <param name="confirmationText">Confirmation text</param>
        /// <param name="confirm">is it confirmed ?</param>
        /// <returns>The same <see cref="SeleniumTesterBase"/> object.</returns>
        public TestFixtureWeb AnswerToConfirmation(string confirmationText, bool confirm)
        {
            Assert.IsTrue(selenium.IsConfirmationPresent());
            Assert.AreEqual(confirmationText, selenium.GetConfirmation());

            if (confirm)
                selenium.ChooseOkOnNextConfirmation();
            else
                selenium.ChooseCancelOnNextConfirmation();

            return this;
        }

        /// <summary>
        /// Asserts that the button with controlId is available on the page.
        /// </summary>
        /// <param name="controlId">Id of the button.</param>
        /// <returns>The same <see cref="SeleniumTesterBase"/> object.</returns>
        public TestFixtureWeb AssertButtonAvailable(string controlId)
        {
            Assert.IsFalse(
                selenium.IsElementPresent(
                    String.Format(
                        CultureInfo.InvariantCulture,
                        "xpath=//a[contains(@href,'{0}')]",
                        controlId)));

            return this;
        }

        /// <summary>
        /// Asserts that the DropDown contains given values.
        /// </summary>
        /// <param name="controlId">Id of dropDown control to assert.</param>
        /// <param name="parameters">Selection values that dropDown has to contain.</param>
        /// <returns>The same <see cref="SeleniumTesterBase"/> object.</returns>
        public TestFixtureWeb AssertDropDownContainsValues(string controlId, params string[] parameters)
        {
            string locatorForDropDown = string.Format(
                CultureInfo.CurrentCulture,
                "xpath=//select[contains(@id,'{0}')]",
                controlId);
            string[] itemValues = selenium.GetSelectOptions(locatorForDropDown);
            List<string> list = new List<string>(itemValues);
            Assert.AreEqual(parameters.Length, itemValues.Length);
            foreach (string parameter in parameters)
                Assert.IsTrue(list.Contains(parameter));

            return this;
        }

        /// <summary>
        /// Asserts the page displayed a specified list of validation error codes. If at least one error is missing, 
        /// the method fails.
        /// </summary>
        /// <param name="errorCodes">The validation error codes which have to be present on the page.</param>
        /// <returns>The same <see cref="SeleniumTesterBase"/> object.</returns>
        public TestFixtureWeb AssertHasErrors(params string[] errorCodes)
        {
            List<string> errorCodesList = new List<string>(errorCodes);

            foreach (string errorCode in errorCodesList)
            {
                Assert.IsTrue(selenium.IsTextPresent(errorCode), "Error '{0}' is missing.", errorCode);
            }

            return this;
        }

        /// <summary>
        /// Asserts the specified HTML element has the expected text.
        /// </summary>
        /// <param name="locator">The locator for the HTML element.</param>
        /// <param name="expectedText">The expected text.</param>
        /// <returns>The same <see cref="SeleniumTesterBase"/> object.</returns>
        public TestFixtureWeb AssertHtmlText(string locator, string expectedText)
        {
            string actualText = Selenium.GetText(locator);
            Assert.AreEqual(expectedText, actualText, "Unexpected HTML text");

            return this;
        }

        public TestFixtureWeb AssertHasText(string expectedText)
        {
            Assert.AreEqual(true, Selenium.GetBodyText().Contains(expectedText), "Text not found");

            return this;
        }

        /// <summary>
        /// Asserts that the application is currently on the specified page.
        /// </summary>
        /// <param name="pageName">The name of the page.</param>
        /// <param name="pageId">The ID of the dialog.</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb AssertIsOnPage(string pageName, string pageId)
        {
            Uri absoluteUrl = new Uri(selenium.GetLocation());
            string localPath = absoluteUrl.LocalPath;
            string assertMessage = String.Format(
                CultureInfo.InvariantCulture,
                "Page '{0}' was expected, actually it is '{1}'.",
                pageName,
                Path.GetFileName(localPath));

            Assert.AreEqual(
                pageName,
                Path.GetFileName(localPath),
                assertMessage);

            if (pageId != null)
            {
                AssertLabelText("DialogIdLabel", pageId);
            }

            return this;
        }

        /// <summary>
        /// Asserts that the Label with specific Id has a specific Text set
        /// </summary>
        /// <param name="controlId">ControlId (or part of it) of the label we are chcking</param>
        /// <param name="labelText">Text to which we are checking against</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb AssertLabelText(string controlId, string labelText)
        {
            string text = selenium.GetText("xpath=//span[contains(@id,'" + controlId + "')]");
            Assert.AreEqual(
                labelText,
                text,
                "Label Text '{0}' was expected, actually it is '{1}'",
                labelText,
                text);

            return this;
        }

        /// <summary>
        /// Asserts that the TextBox with specific Id has a specific Value set
        /// </summary>
        /// <param name="controlId">ControlId (or part of it) of the textbox we are chcking</param>
        /// <param name="textBoxValue">Value to which we are checking against</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb AssertTextBoxValue(string controlId, string textBoxValue)
        {
            string text = selenium.GetValue("xpath=//input[contains(@id,'" + controlId + "')]");
            Assert.AreEqual(
                textBoxValue,
                text,
                "TextBox Text '{0}' was expected, actually it is '{1}",
                textBoxValue,
                text);

            return this;
        }

        /// <summary>
        /// Asserts that the value is part of selection.
        /// </summary>
        /// <param name="value">value to select, which is checked for its appearance.</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb AssertIsLabelTextAvailable(string value)
        {
            Assert.IsTrue(
                selenium.IsElementPresent(
                    "xpath=//label[text()='" + value + "']"),
                "Value '{0}' is not available",
                value);
            return this;
        }

        /// <summary>
        /// Asserts that the value is not part of selection.
        /// </summary>
        /// <param name="controlValue">value to select, which is checked for its appearance.</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb AssertIsOptionUnavailable(string controlValue)
        {
            Assert.IsFalse(selenium.IsElementPresent("xpath=//input[contains(@value,'" + controlValue + "')]"));
            return this;
        }

        /// <summary>
        /// Asserts that the Label doesn't exist
        /// </summary>
        /// <param name="controlId">ControlId (or part of it) of the label we are checking if it exists</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb AssertNoLabel(string controlId)
        {
            string text = null;
            try
            {
                text = selenium.GetText("xpath=//span[contains(@id,'" + controlId + "')]");
            }
            catch (SeleniumException)
            {
            }

            Assert.AreEqual(
                null,
                text,
                "Label with Id '{0}' was not expected but was found.",
                controlId);
            return this;
        }

        /// <summary>
        /// Asserts that the RadioButton contains given values.
        /// </summary>
        /// <param name="groupName">Name of group of radio buttons.</param>
        /// <param name="parameters">Raddio button values that have to be in given group.</param>
        /// <returns>The same <see cref="SeleniumTesterBase"/> object.</returns>
        public TestFixtureWeb AssertRadioButtonContainsValues(string groupName, params string[] parameters)
        {
            decimal count = selenium.GetXpathCount(
                String.Format(
                    CultureInfo.InvariantCulture,
                    "//input[@type='radio' and @name='{0}']",
                    groupName));

            Assert.AreEqual(
                parameters.Length,
                count,
                "Radio button '{0}' does not contain the expected number of values.",
                groupName);

            foreach (string parameter in parameters)
            {
                if (false == String.IsNullOrEmpty(parameter))
                    Assert.IsTrue(
                        selenium.IsElementPresent(
                            String.Format(
                                CultureInfo.InvariantCulture,
                                "xpath=//input[@type='radio' and @name='{0}' and @value='{1}']",
                                groupName,
                                parameter)),
                        "Radio button '{0}' does not contain the expected value '{1}'.",
                        groupName,
                        parameter);
            }

            return this;
        }

        /// <summary>
        /// Asserts that the RadioButton contains disabled value.
        /// </summary>
        /// <param name="groupName">Name of group of radio buttons.</param>
        /// <param name="value">Value of the selected radio button.</param>
        /// <returns>The same <see cref="SeleniumTesterBase"/> object.</returns>
        public TestFixtureWeb AssertIsRadioButtonValueDisabled(string groupName, string value)
        {
            try
            {
                Assert.IsFalse(
                    selenium.IsEditable(
                        String.Format(
                            CultureInfo.InvariantCulture,
                            "xpath=//input[@type='radio' and @name='{0}' and @value='{1}']",
                            groupName,
                            value)),
                    "Radio button '{0}' value '{1}' is not disabled.",
                    groupName,
                    value);
            }
            catch (SeleniumException)
            {
                Assert.Fail("Radio button '{0}' dose not contain value '{1}'.", groupName, value);
            }

            return this;
        }

        /// <summary>
        /// Asserts that specified value in radio is selected.
        /// </summary>
        /// <param name="controlValue">the Value property of the radio button</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb AssertIsRadioButtonValueSelected(string controlValue)
        {
            Assert.IsTrue(
                selenium.IsChecked(
                    String.Format(
                        CultureInfo.InvariantCulture,
                        "xpath=//input[@type='radio' and @value='{0}']",
                        controlValue)));
            return this;
        }

        /// <summary>
        /// Assert checkbox state
        /// </summary>
        /// <param name="selected">true if checkbox is seleced, otherwise false</param>
        /// <param name="locator">checkbox control locator (e.g."xpath=//input[contains(@id,'ctl00_MainContentPlaceholder_ContentPanel1_CheckBox1')]")</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb AssertCheckBoxState(bool selected, string locator)
        {
            Assert.AreEqual(selected, selenium.IsChecked(locator));
            return this;
        }

        /// <summary>
        /// Asserts that the drop down control has the specified value selected.
        /// </summary>
        /// <param name="dropDownControlId">The ID of the drop down control.</param>
        /// <param name="expectedValue">The expected value.</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb AssertSelectedDropDownValue(string dropDownControlId, object expectedValue)
        {
            string locatorForDropDown = string.Format(
                CultureInfo.CurrentCulture,
                "xpath=//select[contains(@id,'{0}')]",
                dropDownControlId);
            string selectedValue = selenium.GetSelectedValue(locatorForDropDown);

            Assert.AreEqual(expectedValue.ToString(), selectedValue);

            return this;
        }

        /// <summary>
        /// Presses the "Back" button on the browser.
        /// </summary>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb BrowserBackButton()
        {
            selenium.GoBack();
            return Pause(1000);
        }

        /// <summary>
        /// Clears the value of the specified input control.
        /// </summary>
        /// <param name="inputControlId">The ID of the input control.</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb ClearValue(string inputControlId)
        {
            return EnterValue(inputControlId, String.Empty);
        }

        /// <summary>
        /// Clicks the on the specified link.
        /// </summary>
        /// <param name="controlId">The ID of the control which is contained in the link.</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb ClickOnLink(string controlId)
        {
            selenium.Click(
                String.Format(
                    CultureInfo.InvariantCulture,
                    "xpath=//a[contains(@href,'{0}')]",
                    controlId));
            selenium.WaitForPageToLoad("10000");
            //Pause();
            return this;
        }

        /// <summary>
        /// Clicks the on the specified button.
        /// </summary>
        /// <param name="controlId">The ID of the control which is contained in the input field.</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb ClickOnButton(string controlId)
        {
            selenium.Click(
                String.Format(
                    CultureInfo.InvariantCulture,
                    "xpath=//input[contains(@id,'{0}')]",
                    controlId));
            selenium.WaitForPageToLoad("10000");
            //Pause();
            return this;
        }

        /// <summary>
        /// Clicks the on the specified link button.
        /// </summary>
        /// <param name="controlId">The ID of the control which is contained in the input field.</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb ClickOnLinkButton(string controlId)
        {
            selenium.Click(
                String.Format(
                    CultureInfo.InvariantCulture,
                    "xpath=//a[contains(@id,'{0}')]",
                    controlId));
            selenium.WaitForPageToLoad("10000");
            //Pause();
            return this;
        }

        /// <summary>
        /// Clicks the on the specified link and then expects a confirmation message box to appear.
        /// If confirmed then it goes to load a new page.
        /// </summary>
        /// <param name="controlId">The ID of the control which is contained in the link.</param>
        /// <param name="confirmationText">The expected confirmation text in the message box.</param>
        /// <param name="confirm">if set to <c>true</c>, the action will be confirmed. Otherwise it will be cancelled.</param>
        /// <param name="linkButton">if set to true the button is a LinkButton</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb ClickOnWithConfirmation(
            string controlId,
            string confirmationText,
            bool confirm,
            bool linkButton)
        {
            string controlName = "input";
            if (linkButton)
            {
                controlName = "a";
            }
            // first set up the non-default Selenium behaviour
            if (false == confirm)
                selenium.ChooseCancelOnNextConfirmation();

            selenium.Click(
                String.Format(
                    CultureInfo.InvariantCulture,
                    "xpath=//{0}[contains(@id,'{1}')]",
                    controlName,
                    controlId));

            Assert.IsTrue(selenium.IsConfirmationPresent());
            Assert.AreEqual(confirmationText, selenium.GetConfirmation());

            // only if confirmed will a new page be actually loaded
            if (confirm)
                selenium.WaitForPageToLoad("10000");

            return this;
        }

        /// <summary>
        /// Sets a Radio Button (which is passed in as the value property of the control)
        /// </summary>
        /// <param name="controlValue">the Value property of the radio button</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb SetRadioButtonByValue(string controlValue)
        {
            selenium.Check("xpath=//input[contains(@value,'" + controlValue + "')]");
            return this;
        }

        /// <summary>
        /// Sets a Radio Button containing string in id
        /// </summary>
        /// <param name="controlId">the Id property of the radio button</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb SetRadioButtonById(string controlId)
        {
            selenium.Check("xpath=//input[contains(@id,'" + controlId + "')]");
            return this;
        }

        /// <summary>
        /// Checks or unchecks a specific CheckBox
        /// </summary>
        /// <param name="controlId">Control ID of the control (can be only a part of it)</param>
        /// <param name="value">true = check it, false = uncheck it</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb SetCheckBox(string controlId, bool value)
        {
            if (value)
            {
                selenium.Check(
                    String.Format(
                        CultureInfo.InvariantCulture,
                        "xpath=//input[contains(@id,'{0}')]",
                        controlId));
            }
            else
            {
                selenium.Uncheck(
                    String.Format(
                        CultureInfo.InvariantCulture,
                        "xpath=//input[contains(@id,'{0}')]",
                        controlId));
            }

            return this;
        }

        /// <summary>
        /// Enters the text into the input field.
        /// </summary>
        /// <param name="inputControlId">The ID of the input control.</param>
        /// <param name="value">The text value to be entered.</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb EnterValue(string inputControlId, string value)
        {
            selenium.Type(
                String.Format(
                    CultureInfo.InvariantCulture,
                    "xpath=//input[contains(@id,'{0}') or contains(@name,'{0}')]",
                    inputControlId),
                value);
            return this;
        }

        /// <summary>
        /// Pauses the test execution for a short period. This method is sometimes useful when you have
        /// to wait for the page to load.
        /// </summary>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb Pause(int miliseconds)
        {
            Thread.Sleep(miliseconds);
            return this;
        }

        /// <summary>
        /// Selects a specified item from the specified drop down list (combo box).
        /// </summary>
        /// <param name="inputControlId">The ID of the input control.</param>
        /// <param name="value">The value to be selected.</param>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb SelectItem(string inputControlId, string value)
        {
            selenium.Select(
                String.Format(
                    CultureInfo.InvariantCulture,
                    "xpath=//select[contains(@id,'{0}')]",
                    inputControlId),
                value);
            return Pause(1000);
        }

        /// <summary>
        /// Stops the Selenium test session.
        /// </summary>
        /// <returns>
        /// The same <see cref="SeleniumTesterBase"/> object.
        /// </returns>
        public TestFixtureWeb Stop()
        {
            selenium.Stop();
            return this;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// click in button with value...
        /// </summary>
        /// <param name="controlvalue"></param>
        /// <returns></returns>
        public TestFixtureWeb ClickOnButtonWithValue(string controlvalue)
        {
            selenium.Click(
        String.Format(
            CultureInfo.InvariantCulture,
            "xpath=//input[@type='submit' and @value='{0}']",
            controlvalue));
            //selenium.WaitForPageToLoad("10000");
            //Pause();
            return this;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// return true for visible tegs
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        public TestFixtureWeb AssertIsVisibleTeg(string locator)
        {
            Assert.IsTrue(selenium.IsVisible(locator));
            return this;
        }
        
        /// <summary>
        /// return true for unvisible tegs
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        public TestFixtureWeb AssertIsNotVisibleTeg(string locator)
        {
            Assert.IsFalse(selenium.IsVisible(locator));
            return this;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// click on last button with value remove
        /// </summary>
        /// <returns></returns>
        public TestFixtureWeb ClickOnLastButtonRemove()
        {
            List<string> but = new List<string>(selenium.GetAllButtons());
            int index = 0;
            int len = but.Count;
            foreach (string a in but)
            {
                if (index == 3)
                {
                    selenium.Click(a);
                }
                index++;
            }
            selenium.WaitForPageToLoad("10000");
            return this;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public TestFixtureWeb ClickOnLinkButton11(string controlId)
        {
            selenium.Click(
                String.Format(
                    CultureInfo.InvariantCulture,
                    "xpath=//a[contains(@id,'{0}')]",
                    controlId));
            selenium.WaitForPageToLoad("30000");
            //Pause();
            return this;
        }

        public TestFixtureWeb AssertIsNotTextAvailable(string value)
        {
            Assert.IsFalse(
                selenium.IsElementPresent(
                    "xpath=//label[text()='" + value + "']"),
                "Value '{0}' is not available",
                value);
            return this;
        }
        private StringBuilder verificationErrors;
        
        private readonly ISelenium selenium;

        private readonly BrowserType browserType = BrowserType.InternetExplorer;

        private readonly string testMachine;

        private readonly int seleniumPort;

        private readonly string seleniumSpeed;

        private readonly string browserUrl;
    }
}