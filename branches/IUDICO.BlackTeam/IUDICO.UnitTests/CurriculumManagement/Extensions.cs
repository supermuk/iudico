using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IUDICO.Common.Models;

namespace IUDICO.UnitTests
{
    public static class AdvAssert
    {
        public static void AreEqual(Curriculum expected, Curriculum actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Created.ToString(), actual.Created.ToString());
            Assert.AreEqual(expected.Updated.ToString(), actual.Updated.ToString());
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

        public static void AreEqual(Stage expected, Stage actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.CurriculumRef, actual.CurriculumRef);
            Assert.AreEqual(expected.Created.ToString(), actual.Created.ToString());
            Assert.AreEqual(expected.Updated.ToString(), actual.Updated.ToString());
        }

        public static void AreEqual(IList<Stage> expected, IList<Stage> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (Stage expectedItem in expected)
            {
                Stage actualItem = actual.SingleOrDefault(item => item.Id == expectedItem.Id);
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

        public static void AreEqual(Theme expected, Theme actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.CourseRef, actual.CourseRef);
            Assert.AreEqual(expected.ThemeTypeRef, actual.ThemeTypeRef);
            Assert.AreEqual(expected.StageRef, actual.StageRef);
        }

        public static void AreEqual(IList<Theme> expected, IList<Theme> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (Theme expectedItem in expected)
            {
                Theme actualItem = actual.SingleOrDefault(item => item.Id == expectedItem.Id);
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

        public static void AreEqual(CurriculumAssignment actual, CurriculumAssignment expected)
        {
            Assert.AreEqual(expected.UserGroupRef, actual.UserGroupRef);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.CurriculumRef, actual.CurriculumRef);
        }

        public static void AreEqual(IList<CurriculumAssignment> expected, IList<CurriculumAssignment> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (CurriculumAssignment expectedItem in expected)
            {
                CurriculumAssignment actualItem = actual.SingleOrDefault(item => item.Id == expectedItem.Id);
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
            Assert.AreEqual(expected.CurriculumAssignmentRef, actual.CurriculumAssignmentRef);
            Assert.AreEqual(expected.StageRef, actual.StageRef);
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
                    //Assert.AreEqual(expectedItem.GroupUsers, actualItem.GroupUsers);
                }
                else
                {
                    Assert.Fail(String.Format("Item with id={0} doesn't exist in actual collection, but expected in expected collection",
                        expectedItem.Id));
                }
            }
        }

        public static void AreEqual(ThemeAssignment actual, ThemeAssignment expected)
        {
            Assert.AreEqual(expected.ThemeRef, actual.ThemeRef);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.CurriculumAssignmentRef, actual.CurriculumAssignmentRef);
            Assert.AreEqual(expected.MaxScore, actual.MaxScore);
        }

        public static void AreEqual(IList<ThemeAssignment> expected, IList<ThemeAssignment> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (ThemeAssignment expectedItem in expected)
            {
                ThemeAssignment actualItem = actual.SingleOrDefault(item => item.Id == expectedItem.Id);
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
