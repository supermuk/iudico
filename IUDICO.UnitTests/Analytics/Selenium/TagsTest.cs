using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using IUDICO.UnitTests.Base;
using NUnit.Framework;

namespace IUDICO.UnitTests.Analytics.Selenium
{
    [TestFixture]
    public class TagsTest : SimpleWebTest
    {
        /*
         * Navigate to
         * Home -> Analytics -> Tags
         */
        public void NavigateToTagsManagerPage(String username, String password)
        {
            // Log in with credentials
            this.DefaultLogin(username,password);

            // Navigate to 'Analytics' plugin page. Click on 'Analytics' button in top menu.
            this.selenium.Click("//a[contains(@href,'/Analytics')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Navigate to Tags manager page. Click on 'Tags' link.
            this.selenium.Click("//a[contains(@href,'/Tags')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
        }

        /*
         * Navigate to
         * Home -> Analytics -> Recommender
         */
        public void NavigateToRecomenderPage(String username, String password)
        {
            // Log in with credentials
            this.DefaultLogin(username, password);

            // Navigate to 'Analytics' plugin page. Click on 'Analytics' button in top menu.
            this.selenium.Click("//a[contains(@href,'/Analytics')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Navigate to 'Recommender' page. Click on 'Recommender' link.
            this.selenium.Click("//a[contains(@href,'/Recommender')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
        }

        [Test]
        public void CreateNewValidTagTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Tags' page.
            NavigateToTagsManagerPage("lex", "lex");

            // Save number of existing Tags (-1, because fist row contain headers)
            decimal numberOfExistingTags = this.selenium.GetXpathCount("//table/tbody/tr") - 1;

            // Click on 'Create new tag' link.
            this.selenium.Click("//a[contains(@href,'/Tags/Create')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Enter valid Tag name
            this.selenium.Type("//input[@id='Name']", "New Test Tag");

            // Click 'Create' button
            this.selenium.Click("//p/input[@type='submit']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            
            // Verify that new tag created
            Assert.AreEqual(numberOfExistingTags + 1, this.selenium.GetXpathCount("//table/tbody/tr") - 1);
        }

        [Test]
        public void CreateNewInvalidTagTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Tags' page.
            NavigateToTagsManagerPage("lex", "lex");

            // Save number of existing Tags (-1, because fist row contain headers)
            String nameOfExistingTags = this.selenium.GetText("//table/tbody/tr[2]/td[1]");

            // Click on 'Create new tag' link.
            this.selenium.Click("//a[contains(@href,'/Tags/Create')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Enter valid Tag name
            this.selenium.Type("//input[@id='Name']", nameOfExistingTags);

            // Click 'Create' button
            this.selenium.Click("//p/input[@type='submit']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Verify that error message appear
            Assert.Fail(); 
            // This test should fail because there are not error messages at 'Create New Tag' page
        }

        [Test]
        public void EditTagTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Tags' page.
            NavigateToTagsManagerPage("lex", "lex");

            // Save old name of first Tag
            String oldTagName = this.selenium.GetText("//table/tbody/tr[2]/td[1]");
            String newTagName = "New " + oldTagName;

            // Click on 'Edit' link for first Tag.
            this.selenium.Click("//a[contains(@href,'/Tags/Edit?id=1')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Enter new Tag name
            this.selenium.Type("//input[@id='Name']", "New " + oldTagName);

            // Click 'Edit' button
            this.selenium.Click("//p/input[@type='submit']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Verify that new Tag name was successfully saved
            String currentTagName = this.selenium.GetText("//table/tbody/tr[2]/td[1]");
            Assert.AreEqual(currentTagName, newTagName);
        }

        [Test]
        public void EditTagTopicsTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Tags' page.
            NavigateToTagsManagerPage("lex", "lex");

            // Click on 'Edit Topics' link for first Tag.
            this.selenium.Click("//a[contains(@href,'/Tags/EditTopics?id=1')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Add few Topics to this Tag
            this.selenium.Select("id=availableTopics", "value=1");
            this.selenium.Click("id=addTopic");

            this.selenium.Select("id=availableTopics", "value=3");
            this.selenium.Click("id=addTopic");

            this.selenium.Select("id=availableTopics", "value=4");
            this.selenium.Click("id=addTopic");

            // Click 'Save' button.
            this.selenium.Click("//input[@type='submit']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
        }

        [Test]
        public void UpdateOneTopicScoresTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Recomender' page.
            NavigateToRecomenderPage("lex", "lex");

            // Click on 'Topic Scores' link.
            this.selenium.Click("//a[contains(@href,'/Recommender/TopicScores')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Click on 'Update Topic Scores' link for first Topic
            this.selenium.Click("//a[contains(@href,'/Recommender/UpdateTopic?id=1')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
        }

        [Test]
        public void UpdateAllTopicScoresTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Recomender' page.
            NavigateToRecomenderPage("lex", "lex");

            // Click on 'Topic Scores' link.
            this.selenium.Click("//a[contains(@href,'/Recommender/TopicScores')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Click on 'Update All Topic Scores' link for first Topic
            this.selenium.Click("//a[contains(@href,'/Recommender/UpdateAllTopics')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
        }

        [Test]
        public void UpdateOneUserScoresTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Recomender' page.
            NavigateToRecomenderPage("lex", "lex");

            // Click on 'User Scores' link.
            this.selenium.Click("//a[contains(@href,'/Recommender/UserScores')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Click on 'Update User Scores' link for first Topic
            this.selenium.Click("//a[contains(@href,'/Recommender/UpdateUser?id=152fd215-53fd-432c-a171-6ff53489311c')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
        }

        [Test]
        public void UpdateAllUserScoresTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Recomender' page.
            NavigateToRecomenderPage("lex", "lex");

            // Click on 'User Scores' link.
            this.selenium.Click("//a[contains(@href,'/Recommender/UserScores')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Click on 'Update All User Scores' link for first Topic
            this.selenium.Click("//a[contains(@href,'/Recommender/UpdateAllUsers')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
        }

        [Test]
        public void TopicQualityTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Discipline and Topic Quality' page.
            // Log in with credentials
            this.DefaultLogin("lex", "lex");

            // Navigate to 'Analytics' plugin page. Click on 'Analytics' button in top menu.
            this.selenium.Click("//a[contains(@href,'/Analytics')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Navigate to Tags manager page. Click on 'Tags' link.
            this.selenium.Click("//a[contains(@href,'/Quality')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Select Topic 'Pascal'.
            this.selenium.Click("xpath=(//input[@name='selectDisciplineId'])[2]");

            // Click 'Select' button.
            this.selenium.Click("//input[@type='submit']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
        }
    }
}
