using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;

namespace IUDICO.UnitTests.CurriculumManagement.NUnit
{
    public static class AdvAssert
    {
        public static void AreEqual(Discipline expected, Discipline actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Owner, actual.Owner);
            //Assert.AreEqual(expected.Created.ToString(), actual.Created.ToString());
            //Assert.AreEqual(expected.Updated.ToString(), actual.Updated.ToString());
        }

        public static void AreEqual(IList<Discipline> expected, IList<Discipline> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (Discipline expectedItem in expected)
            {
                Discipline actualItem = actual.SingleOrDefault(item => item.Id == expectedItem.Id);
                if (actualItem != null)
                {
                    AreEqual(expectedItem, actualItem);
                }
                else
                {
                    Assert.Fail(String.Format("Item with id={0} doesn't exist in actual collection, but expected in expected collection", 
                        expectedItem.Id));
                }
            }
        }

        public static void AreEqual(Chapter expected, Chapter actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.DisciplineRef, actual.DisciplineRef);
            //Assert.AreEqual(expected.Created.ToString(), actual.Created.ToString());
            //Assert.AreEqual(expected.Updated.ToString(), actual.Updated.ToString());
        }

        public static void AreEqual(IList<Chapter> expected, IList<Chapter> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (Chapter expectedItem in expected)
            {
                Chapter actualItem = actual.SingleOrDefault(item => item.Id == expectedItem.Id);
                if (actualItem != null)
                {
                    AreEqual(expectedItem, actualItem);
                }
                else
                {
                    Assert.Fail(String.Format("Item with id={0} doesn't exist in actual collection, but expected in expected collection",
                        expectedItem.Id));
                }
            }
        }

        public static void AreEqual(Topic expected, Topic actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.CourseRef, actual.CourseRef);
            Assert.AreEqual(expected.TopicTypeRef, actual.TopicTypeRef);
            Assert.AreEqual(expected.ChapterRef, actual.ChapterRef);
        }

        public static void AreEqual(IList<Topic> expected, IList<Topic> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (Topic expectedItem in expected)
            {
                Topic actualItem = actual.SingleOrDefault(item => item.Id == expectedItem.Id);
                if (actualItem != null)
                {
                    AreEqual(expectedItem, actualItem);
                }
                else
                {
                    Assert.Fail(String.Format("Item with id={0} doesn't exist in actual collection, but expected in expected collection",
                        expectedItem.Id));
                }
            }
        }

        public static void AreEqual(Curriculum actual, Curriculum expected)
        {
            Assert.AreEqual(expected.UserGroupRef, actual.UserGroupRef);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.DisciplineRef, actual.DisciplineRef);
        }

        public static void AreEqual(IList<Curriculum> expected, IList<Curriculum> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (Curriculum expectedItem in expected)
            {
                Curriculum actualItem = actual.SingleOrDefault(item => item.Id == expectedItem.Id);
                if (actualItem != null)
                {
                    AreEqual(expectedItem, actualItem);
                }
                else
                {
                    Assert.Fail(String.Format("Item with id={0} doesn't exist in actual collection, but expected in expected collection",
                        expectedItem.Id));
                }
            }
        }

        public static void AreEqual(Timeline actual, Timeline expected)
        {
            Assert.AreEqual(expected.EndDate.ToString(), actual.EndDate.ToString());
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.CurriculumRef, actual.CurriculumRef);
            Assert.AreEqual(expected.ChapterRef, actual.ChapterRef);
            Assert.AreEqual(expected.StartDate.ToString(), actual.StartDate.ToString());
        }

        public static void AreEqual(IList<Timeline> expected, IList<Timeline> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (Timeline expectedItem in expected)
            {
                Timeline actualItem = actual.SingleOrDefault(item => item.Id == expectedItem.Id);
                if (actualItem != null)
                {
                    AreEqual(expectedItem, actualItem);
                }
                else
                {
                    Assert.Fail(String.Format("Item with id={0} doesn't exist in actual collection, but expected in expected collection",
                        expectedItem.Id));
                }
            }
        }

        public static void AreEqual(IList<Group> expected, IList<Group> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (Group expectedItem in expected)
            {
                Group actualItem = actual.SingleOrDefault(item => item.Id == expectedItem.Id);
                if (actualItem != null)
                {
                    Assert.AreEqual(expectedItem.Id, actualItem.Id);
                    Assert.AreEqual(expectedItem.Name, actualItem.Name);
                    Assert.AreEqual(expectedItem.Deleted, actualItem.Deleted);
                    Assert.AreEqual(expectedItem.GroupUsers, actualItem.GroupUsers);
                }
                else
                {
                    Assert.Fail(String.Format("Item with id={0} doesn't exist in actual collection, but expected in expected collection",
                        expectedItem.Id));
                }
            }
        }

        public static void AreEqual(TopicAssignment actual, TopicAssignment expected)
        {
            Assert.AreEqual(expected.TopicRef, actual.TopicRef);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.CurriculumRef, actual.CurriculumRef);
            Assert.AreEqual(expected.MaxScore, actual.MaxScore);
        }

        public static void AreEqual(IList<TopicAssignment> expected, IList<TopicAssignment> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (TopicAssignment expectedItem in expected)
            {
                TopicAssignment actualItem = actual.SingleOrDefault(item => item.Id == expectedItem.Id);
                if (actualItem != null)
                {
                    AreEqual(expectedItem, actualItem);
                }
                else
                {
                    Assert.Fail(String.Format("Item with id={0} doesn't exist in actual collection, but expected in expected collection",
                        expectedItem.Id));
                }
            }
        }
        public static void AreEqual(TopicDescription expected, TopicDescription actual)
        {
            Assert.AreEqual(expected.Topic, actual.Topic);
            Assert.AreEqual(expected.Chapter, actual.Chapter);
            Assert.AreEqual(expected.Discipline, actual.Discipline);
            AdvAssert.AreEqual(expected.Timelines, actual.Timelines);
        }
        public static void AreEqual(IEnumerable<TopicDescription> expected, IEnumerable<TopicDescription> actual)
        {
            Assert.AreEqual(expected.ToList().Count, actual.ToList().Count);
            foreach (TopicDescription exp in expected)
            {
                TopicDescription act = actual.SingleOrDefault(item => item.Topic == exp.Topic);
                if (act != null)
                {
                    AreEqual(exp, act);
                }
                else
                {
                    Assert.Fail("Expected topic description with topic={0} doesn't exists in actual collection", exp.Topic);
                }
            }
        }
    }

    public static class Extensions
    {
        /// <summary>
        /// Gets elements specified by itemNumbers from items collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="itemNumbers">The item numbers.</param>
        /// <returns></returns>
        public static List<T> GetSpecificItems<T>(this List<T> items, params int[] itemNumbers)
        {
            List<T> result = new List<T>();
            itemNumbers
                .ToList()
                .ForEach(number => result.Add(items[number]));
            return result;
        }
    }
}
