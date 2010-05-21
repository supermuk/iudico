using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FireFly.CourseEditor.Course.Manifest;
using FireFly.CourseEditor.Course;

namespace FireFly.UnitTests
{
    [TestFixture]
    [Description("Tests functionality of SequencingManager class.")]
    public class SequencingManagerTests
    {
        #region Fields

        protected ItemType chapterItem;
        protected ItemType controlChapterItem;
        protected ItemType theoryItem;
        protected ItemType questionItem;
        protected ItemType summaryItem;
        protected ItemType unknownItem;

        protected List<ItemType> items;

        protected SequencingType sequencing;

        protected OrganizationType organization;

        #endregion

        [SetUp]
        public void SettingUp()
        {             
                      
            this.chapterItem = ItemType.CreateNewItem("chapter item", "ci", null, PageType.Chapter);
            this.controlChapterItem = ItemType.CreateNewItem("controlChapter item", "cci", "ewrt", PageType.ControlChapter);
            this.theoryItem = ItemType.CreateNewItem("theory item", "ti", "dsfg", PageType.Theory);
           // this.questionItem = ItemType.CreateNewItem("question item", "qi", "rtwret", PageType.Question);
            this.summaryItem = ItemType.CreateNewItem("summary item", "si", "ywwdfg", PageType.Summary);
            this.unknownItem = ItemType.CreateNewItem("unknown item", "ui", "wettw", PageType.Unknown);

            this.items = new List<ItemType>();
            this.items.Add(chapterItem);
            this.items.Add(controlChapterItem);
            this.items.Add(theoryItem);
           // this.items.Add(questionItem);
            this.items.Add(summaryItem);
            this.items.Add(unknownItem);

            this.sequencing = new SequencingType();

            this.organization = new OrganizationType();
        }

        [Description("Tests Sequencing not null after item created.")]
        [Test]
        public void CreateNewSequencingTest()
        {
            foreach (ItemType item in this.items)
            {
                Assert.IsNotNull(item.Sequencing);
                SequencingType seq = SequencingManager.CreateNewSequencing(item);
                Assert.IsNotNull(seq);
            }
        }

        [Description("Tests Sequencing not null after organization sequencing creation.")]
        [Test]
        public void CreateOrganizationDefaultSequencingTest()
        {
            SequencingManager.CreateOrganizationDefaultSequencing(this.organization);
            Assert.AreEqual(1, this.organization.SequencingPatterns.Count);
        }

        [Description("Tests object after default constructor invoked.")]
        [Test]
        public void SequencingAfterInitializingTest()
        {
            Assert.IsNull(this.sequencing.auxiliaryResources);
            Assert.IsNull(this.sequencing.constrainedChoiceConsiderations);
            Assert.IsNull(this.sequencing.controlMode);
            Assert.IsNull(this.sequencing.deliveryControls);
            Assert.IsNull(this.sequencing.ID);
            Assert.IsNull(this.sequencing.IDRef);
            Assert.IsNull(this.sequencing.limitConditions);
            Assert.IsNull(this.sequencing.objectives);
            Assert.IsNull(this.sequencing.randomizationControls);
            Assert.IsNull(this.sequencing.rollupConsiderations);
            Assert.IsNull(this.sequencing.rollupRules);
            Assert.IsNull(this.sequencing.sequencingRules);
        }
        
        [Description("Tests ClearSequencing method and Constructor by default.")]
        [Test]
        public void ClearSequencingTest()
        {
            Assert.IsNotNull(this.chapterItem.Sequencing);

            SequencingType seq = SequencingManager.ClearSequencing(this.chapterItem);

            Assert.IsNotNull(seq);
            Assert.AreEqual(seq, this.chapterItem.Sequencing);
        }

        [Description("Tests CreatePreConditionRule method.")]
        [Test]
        public void CreateSimplePreConditionRuleTest()
        {
            PreConditionRuleType rule = SequencingManager.CreateSimplePreConditionRule(SequencingRuleConditionType.completed, PreConditionRuleActionType.skip);
            Assert.IsNotNull(rule);
            Assert.AreEqual(SequencingRuleConditionType.completed, rule.ruleConditions.ruleCondition[0].condition);
            Assert.AreEqual(PreConditionRuleActionType.skip, rule.ruleAction.action);
        }
    }

    [TestFixture]
    [Description("Tests functionality of SequencingPattern class.")]
    public class SequencingPatternTests
    {
        SequencingPattern pattern;
        OrganizationType organization;
        ItemType chapter;
        ItemType controlChapter;

        [SetUp]
        public void SetUp()
        {
            this.pattern = new SequencingPattern();
            this.organization = new OrganizationType();
            this.chapter = ItemType.CreateNewItem("Chapter", "chapter_base", null, PageType.Chapter);
            this.controlChapter = ItemType.CreateNewItem("Control Chapter", "control chapter", null, PageType.ControlChapter);
        }

        [Description("Tests CanApplyPattern method.")]
        [Test]
        public void CanApplyPatternTest()
        {             
            int excCount = 0;
            
            try
            {
                pattern.CanApplyPattern(null); 
            }
            catch(ArgumentNullException)
            {
                excCount++;
            }

            Assert.IsFalse(pattern.CanApplyPattern(excCount));
            Assert.IsFalse(pattern.CanApplyPattern(organization));
            Assert.IsFalse(pattern.CanApplyPattern(chapter));

            ItemType chapter1 = ItemType.CreateNewItem("chapter1", "chapter1_id", null, PageType.Chapter);
            ItemType controlChapter1 = ItemType.CreateNewItem("controlChapter1", "controlChapter1_id", null, PageType.ControlChapter);
            ItemType theory = ItemType.CreateNewItem("theory1", "theory_id", null, PageType.Theory);

            this.controlChapter.SubItems.Add(theory);
            Assert.IsTrue(pattern.CanApplyPattern(controlChapter));

            this.organization.SubItems.Add(theory);
            this.organization.SubItems.Add(controlChapter1);
            Assert.IsTrue(pattern.CanApplyPattern(this.organization));

            theory.SubItems.Add(chapter1);
            Assert.IsFalse(pattern.CanApplyPattern(theory));
        }

        [Description("Tests ApplyPattern method.")]
        [Test]
        public void ApplyPatternTest()
        {
            bool catched = false;
            try
            {
            this.pattern.ApplyPattern(this.organization);
            }
            catch(InvalidOperationException)
            {
                catched = true;
            }
            Assert.AreEqual(true, catched);
            this.organization.SubItems.Add(this.controlChapter);
            this.pattern.ApplyPattern(this.organization);
        }

        [Description("Tests are all chapters method.")]
        [Test]
        public void AreAllChaptersTest()
        {
            Assert.IsTrue(SequencingPattern.AreAllChapters(this.organization));

            this.organization.SubItems.Add(chapter);
            this.organization.SubItems.Add(controlChapter);

            Assert.IsTrue(SequencingPattern.AreAllChapters(organization));
            
            ItemType theory = ItemType.CreateNewItem("theory1", "theory_id", null, PageType.Theory);
            
            this.organization.SubItems.Add(theory);

            Assert.IsFalse(SequencingPattern.AreAllChapters(organization));
        }

        [Description("Tests are all pages(question or theory) method.")]
        [Test]
        public void AreAllPagesTest()
        {
            Assert.IsTrue(SequencingPattern.AreAllPages(this.chapter));

            ItemType theory = ItemType.CreateNewItem("theory1", "theory_id", null, PageType.Theory);
            ItemType summary = ItemType.CreateNewItem("summary2", "theory2_id", null, PageType.Summary);

            this.chapter.SubItems.Add(theory);
            this.chapter.SubItems.Add(summary);

            Assert.IsTrue(SequencingPattern.AreAllPages(this.chapter));

            this.chapter.SubItems.Add(this.controlChapter);

            Assert.IsFalse(SequencingPattern.AreAllPages(this.chapter));
        }
    }

}
