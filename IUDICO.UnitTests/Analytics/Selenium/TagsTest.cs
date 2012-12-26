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
        public void NavigateToTagsManagerPage(string username, string password)
        {
            // Log in with credentials
            this.DefaultLogin(username, password);

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
        public void NavigateToRecomenderPage(string username, string password)
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
            this.NavigateToTagsManagerPage("lex", "lex");

            // Save number of existing Tags (-1, because fist row contain headers)
            decimal numberOfExistingTags = this.selenium.GetXpathCount("//table/tbody/tr") - 1;

            // name of valid tag
            string tagName = "Test Valid Tag";

            // Click on 'Create new tag' link.
            this.selenium.Click("//a[contains(@href,'/Tags/Create')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Enter valid Tag name
            this.selenium.Type("//input[@id='Name']", tagName);

            // Click 'Create' button
            this.selenium.Click("//p/input[@type='submit']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            
            // Verify that new tag created
            Assert.AreEqual(numberOfExistingTags + 1, this.selenium.GetXpathCount("//table/tbody/tr") - 1);

            // Delete created tag
            this.selenium.Click("//table/tbody/tr[" + (numberOfExistingTags + 2).ToString() + "]/td[2]/a[contains(text(),'Delete')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
        }

        /// <summary>
        /// Author - Pohlod Yaroslav
        /// </summary>
        [Test]
        public void CreateBlankTagTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Tags' page.
            this.NavigateToTagsManagerPage("lex", "lex");

            // Click on 'Create new tag' link.
            this.selenium.Click("//a[contains(@href,'/Tags/Create')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Nothing typed - blank tag

            // Click 'Create' button
            this.selenium.Click("//p/input[@type='submit']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            bool isCreatePage = this.selenium.GetLocation().Contains("/Tags/Create");

            // Back to list with tags
            this.selenium.Click("//a[contains(@href,'/Tags')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Check if we stay on create page with error message or go to /Tags page
            Assert.IsTrue(isCreatePage); 
        }


        /// <summary>
        /// Author - Pohlod Yaroslav
        /// </summary>
        [Test]
        public void CreateNewTagInvalidTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Tags' page.
            this.NavigateToTagsManagerPage("lex", "lex");

            // Click on 'Create new tag' link.
            this.selenium.Click("//a[contains(@href,'/Tags/Create')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Enter duplicate Tag name
            this.selenium.Type("//input[@id='Name']", "8 is a number");

            // Click 'Create' button
            this.selenium.Click("//p/input[@type='submit']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            bool isCreatePage = this.selenium.GetLocation().Contains("/Tags/Create");

            // Check if we stay on create page with error message or go to /Tags page
            Assert.IsTrue(isCreatePage);
        }

        /// <summary>
        /// Author - Pohlod Yaroslav
        /// </summary>
        [Test]
        public void CreateDuplicateTagTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Tags' page.
            this.NavigateToTagsManagerPage("lex", "lex");

            // Save number of existing Tags (-1, because fist row contain headers)
            string nameOfDuplicateTag = "Test tag";

            // Click on 'Create new tag' link.
            this.selenium.Click("//a[contains(@href,'/Tags/Create')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Enter duplicate Tag name
            this.selenium.Type("//input[@id='Name']", nameOfDuplicateTag);

            // Click 'Create' button
            this.selenium.Click("//p/input[@type='submit']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // if we are not on the Tags page
            if (this.selenium.GetLocation().Contains("/Tags/Create"))
            {
                // Go back to page with tags
                this.selenium.Click("//a[contains(@href,'/Tags')]");
                this.selenium.WaitForPageToLoad(this.SeleniumWait);
            }

            // Click on 'Create new tag' link.
            this.selenium.Click("//a[contains(@href,'/Tags/Create')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Enter duplicate Tag name again
            this.selenium.Type("//input[@id='Name']", nameOfDuplicateTag);

            // Click 'Create' button
            this.selenium.Click("//p/input[@type='submit']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            bool isCreatePage = this.selenium.GetLocation().Contains("/Tags/Create");

            // Check if we stay on create page with error message or go to /Tags page
            Assert.IsTrue(isCreatePage);
        }

        [Test]
        public void EditTagTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Tags' page.
            this.NavigateToTagsManagerPage("lex", "lex");

            // Save number of existing Tags (-1, because fist row contain headers)
            decimal numberOfExistingTags = this.selenium.GetXpathCount("//table/tbody/tr") - 1;

            if (numberOfExistingTags == 0)
            {
                // name of valid tag
                string tagName = "New Test Tag ";

                // Click on 'Create new tag' link.
                this.selenium.Click("//a[contains(@href,'/Tags/Create')]");
                this.selenium.WaitForPageToLoad(this.SeleniumWait);

                // Enter valid Tag name
                this.selenium.Type("//input[@id='Name']", tagName);

                // Click 'Create' button
                this.selenium.Click("//p/input[@type='submit']");
                this.selenium.WaitForPageToLoad(this.SeleniumWait);
            }

            // Save old name of first Tag
            string oldTagName = this.selenium.GetText("//table/tbody/tr[2]/td[1]");
            string newTagName = "New " + oldTagName;

            // Click on 'Edit' link for first Tag.
            this.selenium.Click("//a[contains(@href,'/Tags/Edit?id=')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Enter new Tag name
            this.selenium.Type("//input[@id='Name']", "New " + oldTagName);

            // Click 'Edit' button
            this.selenium.Click("//p/input[@type='submit']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Verify that new Tag name was successfully saved
            string currentTagName = this.selenium.GetText("//table/tbody/tr[2]/td[1]");
            Assert.AreEqual(currentTagName, newTagName);
        }

        /// <summary>
        /// Author - Pohlod Yaroslav
        /// </summary>
        [Test]
        public void DeleteTagTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Tags' page.
            this.NavigateToTagsManagerPage("lex", "lex");

            // Save number of tags before deletion
            decimal numberOfExistingTags = this.selenium.GetXpathCount("//table/tbody/tr") - 1;
            if (numberOfExistingTags == 0)
            {
                // name of valid tag
                string tagName = "New Test Tag ";

                // Click on 'Create new tag' link.
                this.selenium.Click("//a[contains(@href,'/Tags/Create')]");
                this.selenium.WaitForPageToLoad(this.SeleniumWait);

                // Enter valid Tag name
                this.selenium.Type("//input[@id='Name']", tagName);

                // Click 'Create' button
                this.selenium.Click("//p/input[@type='submit']");
                this.selenium.WaitForPageToLoad(this.SeleniumWait);
            }

            // Click on 'Delete' link for first Tag.
            // this.selenium.Click("//a[contains(@href,'/Tags/Delete?id=1')]");
            this.selenium.Click("//a[contains(text(),'Delete')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Save number of tags after deletion
            decimal numberOfExistingTagsAfterDeletion = this.selenium.GetXpathCount("//table/tbody/tr") - 1;

            // Verify that number of existing tags are decrease
            Assert.AreEqual(numberOfExistingTags - 1, numberOfExistingTagsAfterDeletion);
        }

        [Test]
        public void EditTagTopicsTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Tags' page.
            this.NavigateToTagsManagerPage("lex", "lex");

            // Save number of existing Tags (-1, because fist row contain headers)
            decimal numberOfExistingTags = this.selenium.GetXpathCount("//table/tbody/tr") - 1;

            // Add new tag for test
            // name of valid tag
            string tagName = "A tag to test edit topics test";

            // Click on 'Create new tag' link.
            this.selenium.Click("//a[contains(@href,'/Tags/Create')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Enter valid Tag name
            this.selenium.Type("//input[@id='Name']", tagName);

            // Click 'Create' button
            this.selenium.Click("//p/input[@type='submit']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
            ++numberOfExistingTags;

            // Click on 'Edit Topics' link for first Tag.
            this.selenium.Click("//table/tbody/tr[" + (numberOfExistingTags + 1).ToString() + "]/td[2]/a[contains(@href,'/Tags/EditTopics?id=')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Add few Topics to this Tag
            this.selenium.Select("id=availableTopics", "value=1");
            this.selenium.Click("id=addTopic");

            // Click 'Save' button.
            this.selenium.Click("//input[@value='Save']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Click on 'Edit Topics' link for first Tag.
            this.selenium.Click("//table/tbody/tr[" + (numberOfExistingTags + 1).ToString() + "]/td[2]/a[contains(@href,'/Tags/EditTopics?id=')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Add few Topics to this Tag
            this.selenium.Select("id=selectedTopics", "value=1");
            this.selenium.Click("id=removeTopic");

            // Click 'Save' button.
            this.selenium.Click("//input[@value='Save']");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);


            // Delete created tag
            this.selenium.Click("//table/tbody/tr[" + (numberOfExistingTags + 1).ToString() + "]/td[2]/a[contains(text(),'Delete')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
        }

        [Test]
        public void UpdateOneTopicScoresTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Recomender' page.
            this.NavigateToRecomenderPage("lex", "lex");

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
            this.NavigateToRecomenderPage("lex", "lex");

            // Click on 'Topic Scores' link.
            this.selenium.Click("//a[contains(@href,'/Recommender/TopicScores')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);

            // Click on 'Update All Topic Scores' link for first Topic
            this.selenium.Click("//a[contains(@href,'/Recommender/UpdateAllTopics')]");
            this.selenium.WaitForPageToLoad(this.SeleniumWait);
        }

        /// <summary>
        /// Author - Pohlod Yaroslav
        /// </summary>
        [Test]
        public void UpdateOneUserScoresTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Recomender' page.
            this.NavigateToRecomenderPage("lex", "lex");

            try
            {
                // Click on 'User Scores' link.
                this.selenium.Click("//a[contains(@href,'/Recommender/UserScores')]");
                this.selenium.WaitForPageToLoad(this.SeleniumWait);

                // Click on 'Update User Scores' link for first Topic
                this.selenium.Click("//a[contains(text(),'Update User Scores')]");
                this.selenium.WaitForPageToLoad(this.SeleniumWait);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void UpdateAllUserScoresTest()
        {
            // Log In as 'lex' (with 'administrator' role permisions) and navigate to 'Recomender' page.
            this.NavigateToRecomenderPage("lex", "lex");

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

        /// <summary>
        /// Author - Pohlod Yaroslav
        /// </summary>
        [Test]
        public void ViewDisciplineQualityTest()
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
