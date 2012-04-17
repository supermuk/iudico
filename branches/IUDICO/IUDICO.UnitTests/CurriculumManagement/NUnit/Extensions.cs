using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using NUnit.Framework;
using IUDICO.Common.Models;
using IUDICO.DisciplineManagement.Models.ViewDataClasses;
using IUDICO.CurriculumManagement.Models.ViewDataClasses;

namespace IUDICO.UnitTests.CurriculumManagement.NUnit
{
    public static class AdvAssert
    {
        public static void AreEqual(Discipline expected, Discipline actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.Owner, actual.Owner);
            Assert.AreEqual(expected.IsValid, actual.IsValid);
        }

        public static void AreEqual(IList<Discipline> expected, IList<Discipline> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            expected.ForEach((item, i) => AreEqual(expected[i], actual[i]));
        }

        public static void AreEqual(Chapter expected, Chapter actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.DisciplineRef, actual.DisciplineRef);
        }

        public static void AreEqual(IList<Chapter> expected, IList<Chapter> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            expected.ForEach((item, i) => AreEqual(expected[i], actual[i]));
        }

        public static void AreEqual(Topic expected, Topic actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.TestCourseRef, actual.TestCourseRef);
            Assert.AreEqual(expected.TestTopicType, actual.TestTopicType);
            Assert.AreEqual(expected.TheoryCourseRef, actual.TheoryCourseRef);
            Assert.AreEqual(expected.TheoryTopicType, actual.TheoryTopicType);
            Assert.AreEqual(expected.ChapterRef, actual.ChapterRef);
        }

        public static void AreEqual(IList<Topic> expected, IList<Topic> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            expected.ForEach((item, i) => AreEqual(expected[i], actual[i]));
        }

        public static void AreEqual(Curriculum expected, Curriculum actual)
        {
            Assert.AreEqual(expected.UserGroupRef, actual.UserGroupRef);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.DisciplineRef, actual.DisciplineRef);
            Assert.AreEqual(expected.StartDate, actual.StartDate);
            Assert.AreEqual(expected.EndDate, actual.EndDate);
            Assert.AreEqual(expected.IsValid, actual.IsValid);
        }

        public static void AreEqual(IList<Curriculum> expected, IList<Curriculum> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            expected.ForEach((item, i) => AreEqual(expected[i], actual[i]));
        }

        //public static void AreEqual(IList<Group> expected, IList<Group> actual)
        //{
        //    Assert.AreEqual(expected.Count, actual.Count);

        //    foreach (Group expectedItem in expected)
        //    {
        //        Group actualItem = actual.SingleOrDefault(item => item.Id == expectedItem.Id);
        //        if (actualItem != null)
        //        {
        //            Assert.AreEqual(expectedItem.Id, actualItem.Id);
        //            Assert.AreEqual(expectedItem.Name, actualItem.Name);
        //            Assert.AreEqual(expectedItem.Deleted, actualItem.Deleted);
        //            Assert.AreEqual(expectedItem.GroupUsers, actualItem.GroupUsers);
        //        }
        //        else
        //        {
        //            Assert.Fail(
        //                String.Format(
        //                    "Item with id={0} doesn't exist in actual collection, but expected in expected collection",
        //                    expectedItem.Id));
        //        }
        //    }
        //}

        public static void AreEqual(TopicDescription expected, TopicDescription actual)
        {
            AreEqual(expected.Topic, actual.Topic);
            AreEqual(expected.Chapter, actual.Chapter);
            AreEqual(expected.Discipline, actual.Discipline);
            Assert.AreEqual(expected.CourseId, actual.CourseId);
            AreEqual(expected.Curriculum, actual.Curriculum);
            Assert.AreEqual(expected.EndDate, actual.EndDate);
            Assert.AreEqual(expected.StartDate, actual.StartDate);
            Assert.AreEqual(expected.TopicPart, actual.TopicPart);
            Assert.AreEqual(expected.TopicType, actual.TopicType);
        }

        public static void AreEqual(IList<TopicDescription> expected, IList<TopicDescription> actual)
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
                    Assert.Fail("Expected topic description with topic={0} doesn't exists in actual collection",
                                exp.Topic);
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
            var result = new List<T>();
            itemNumbers
                .ToList()
                .ForEach(number => result.Add(items[number]));
            return result;
        }

        public static T ToModel<T>(this ActionResult actionResult)
        {
            return (T)((ViewResult)actionResult).ViewData.Model;
        }

        public static CreateTopicModel ToCreateModel(this Topic topic)
        {
            return new CreateTopicModel
            {
                BindTheoryCourse = topic.TheoryCourseRef.HasValue,
                TheoryCourseId = topic.TheoryCourseRef ?? 0,
                TheoryTopicTypeId = topic.TheoryTopicTypeRef ?? 0,
                BindTestCourse = topic.TestCourseRef.HasValue,
                TestCourseId = topic.TestCourseRef ?? 0,
                TestTopicTypeId = topic.TestTopicTypeRef ?? 0,
                TopicName = topic.Name,
                ChapterId = topic.ChapterRef
            };
        }

        public static CreateCurriculumModel ToCreateModel(this Curriculum curriculum)
        {
            return new CreateCurriculumModel
            {
                DisciplineId = curriculum.DisciplineRef,
                SetDate = curriculum.StartDate.HasValue && curriculum.EndDate.HasValue,
                StartDate = curriculum.StartDate ?? DateTime.Now,
                EndDate = curriculum.EndDate ?? DateTime.Now,
                GroupId = curriculum.UserGroupRef
            };
        }
    }
}