using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FireFly.CourseEditor.Course.Manifest;

namespace FireFly.UnitTest
{
    [TestFixture]
    [Description("Unit tests for FireFly.CourseEditor.Course.Manifest ItemType.")]
    public class ItemTypeTests
    {
        #region Fields

        protected ItemType item;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.item = ItemType.CreateNewItem("Test Item 1", "Test_Item_1_id", null, PageType.Chapter);
        }
        
        [Test]
        [Description("ItemType Title testing.")]
        public void TitleTest()
        {
            Assert.AreEqual("Test Item 1", this.item.Title);
            this.item.Title = "Test Item 2";
            Assert.AreEqual("Test Item 2", this.item.Title);
        }

        [Test]
        [Description("ItemType Identifier testing.")]
        public void IdentifierTest()
        {
            Assert.AreEqual("Test_Item_1_id", this.item.Identifier);
            this.item.Identifier = "Test_Item_2_id";
            Assert.AreEqual("Test_Item_2_id", this.item.Identifier);
        }

        [Test]
        [Description("IsLeaf property testing.")]
        public void IsLeafTest()
        {
            Assert.AreEqual(true, this.item.IsLeaf);
            
            //After adding subitems should be false.
            this.item.SubItems.Add(new ItemType());
            Assert.AreEqual(false, this.item.IsLeaf);
            
            //Again true after subItems clear.
            this.item.SubItems.Clear();
            Assert.AreEqual(true, this.item.IsLeaf);
        }

        [Test]
        [Description("Insert grouping chapter method testing. Check if SubItems are reordered properly.")]
        public void InsertGroupingChapter_SubItemsTest()
        {
            ItemType item1 = ItemType.CreateNewItem("Item1", "Item1", null, PageType.Chapter);
            ItemType item2 = ItemType.CreateNewItem("Item2", "Item2", null, PageType.ControlChapter);

            this.item.SubItems.Add(item1);
            this.item.SubItems.Add(item2);

            Assert.AreEqual(2, this.item.SubItems.Count);

            ItemType groupingItem = ItemType.CreateNewItem("ItemG", "ItemG", null, PageType.Chapter);
            groupingItem.Parent = groupingItem;

            this.item.InsertGroupingItem(groupingItem);

            Assert.AreEqual(1, this.item.SubItems.Count);
            Assert.AreSame(groupingItem, this.item.SubItems[0]);
            Assert.AreEqual(2, this.item.SubItems[0].SubItems.Count);

        }

        [Test]
        [Description("Insert grouping chapter method testing. Check for groupingItem invalid values used.")]
        public void InsertGroupingChapter_ExceptionsTest()
        {
            int excCounter = 0;
            try
            {
                this.item.InsertGroupingItem(null);
            }
            catch
            {
                excCounter++;
            }

            try
            {
                this.item.InsertGroupingItem(ItemType.CreateNewItem("", "", null, PageType.Question));
            }
            catch
            {
                excCounter++;
            }

            Assert.AreEqual(2, excCounter);
        }

        [Test]
        [Description("CheckForLeafChapter method testing. Includes Errors property.")]
        public void CheckForLeafChapterTest()
        {
            Assert.AreEqual(true, this.item.CheckForLeafChapter());
            Assert.Greater(this.item.Errors.Count, 0);

            ItemType item1 = ItemType.CreateNewItem("Item1", "Item1", null, PageType.Chapter);
            ItemType item2 = ItemType.CreateNewItem("Item2", "Item2", null, PageType.Theory);

            this.item.SubItems.Add(item1);
            Assert.AreEqual(false, this.item.CheckForLeafChapter());
            Assert.AreEqual(0, this.item.Errors.Count);

            this.item.RemoveChild(item1);
            
            this.item.SubItems.Add(item2);
            Assert.AreEqual(false, this.item.CheckForLeafChapter());
            Assert.AreEqual(0, this.item.Errors.Count);
        }

        [Test]
        [Description("Remove chapter and merge it's children with parent's children method test.")]
        public void RemoveAndMerge_SubItemsTest()
        {
            //Chapter1 to remove.
            ItemType chapter1 = ItemType.CreateNewItem("Chapter1", "Chapter1", null, PageType.Chapter);
            ItemType item0 = ItemType.CreateNewItem("Item0", "Item0", null, PageType.Theory);
            //Items to merge.
            ItemType item1 = ItemType.CreateNewItem("SubItem1", "SubItem1", null, PageType.Theory);
            ItemType item2 = ItemType.CreateNewItem("SubItem2", "SubItem2", null, PageType.Chapter);

            this.item.SubItems.Add(chapter1);
            this.item.SubItems.Add(item0);
            chapter1.SubItems.Add(item1);
            chapter1.SubItems.Add(item2);
            //Structure will look like this:
            //Test Item 1
            //  Chapter1
            //    SubItem1
            //    SubItem2
            //  Item0

            Assert.AreEqual(2, this.item.SubItems.Count);
            Assert.AreEqual(2, chapter1.SubItems.Count);
            Assert.IsTrue(this.item.SubItems[0].SubItems[0].IsLeaf);
            Assert.IsTrue(this.item.SubItems[0].SubItems[1].IsLeaf);
            Assert.IsTrue(this.item.SubItems[1].IsLeaf);

            Assert.AreSame(chapter1, this.item.SubItems[0]);

            this.item.SubItems[0].RemoveAndMerge();

            Assert.AreEqual(3, this.item.SubItems.Count);
            Assert.IsTrue(this.item.SubItems[0].IsLeaf);
            Assert.IsTrue(this.item.SubItems[1].IsLeaf);
            Assert.IsTrue(this.item.SubItems[2].IsLeaf);

            Assert.IsTrue(this.item.SubItems.Contains(item0));
            Assert.IsTrue(this.item.SubItems.Contains(item1));
            Assert.IsTrue(this.item.SubItems.Contains(item2));
        }

        [Test]
        [Description("RemoveAndMerge method testing with wrong situation.")]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void RemoveAndMerge_ExceptionTest()
        {
            this.item.Parent = new ObjectivesTypeObjective("Id123");

            ItemType item1 = ItemType.CreateNewItem("SubItem1", "SubItem1", null, PageType.Theory);
            ItemType item2 = ItemType.CreateNewItem("SubItem2", "SubItem2", null, PageType.Chapter);

            this.item.SubItems.Add(item1);
            this.item.SubItems.Add(item2);

            this.item.RemoveAndMerge();
        }
    }
}
