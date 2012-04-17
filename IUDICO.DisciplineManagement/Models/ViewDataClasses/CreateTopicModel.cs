using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Attributes;
using System.ComponentModel.DataAnnotations;

namespace IUDICO.DisciplineManagement.Models.ViewDataClasses
{
    public class CreateTopicModel
    {
        #region Properties

        public List<SelectListItem> TestCourses { get; set; }
        public List<SelectListItem> TestTopicTypes { get; set; }
        public List<SelectListItem> TheoryCourses { get; set; }
        public List<SelectListItem> TheoryTopicTypes { get; set; }

        [LocalizedDisplayName("Name")]
        public string TopicName { get; set; }

        [LocalizedDisplayName("BindTestCourse")]
        public bool BindTestCourse { get; set; }

        [DropDownList(SourceProperty = "TestCourses")]
        [LocalizedDisplayName("ChooseCourseForTestTopic")]
        public int TestCourseId { get; set; }

        [DropDownList(SourceProperty = "TestTopicTypes")]
        [LocalizedDisplayName("ChooseTopicTypeForTestTopic")]
        public int TestTopicTypeId { get; set; }

        [LocalizedDisplayName("BindTheoryCourse")]
        public bool BindTheoryCourse { get; set; }

        [DropDownList(SourceProperty = "TheoryCourses")]
        [LocalizedDisplayName("ChooseCourseForTheoryTopic")]
        public int TheoryCourseId { get; set; }

        [DropDownList(SourceProperty = "TheoryTopicTypes")]
        [LocalizedDisplayName("ChooseTopicTypeForTheoryTopic")]
        public int TheoryTopicTypeId { get; set; }

        [ScaffoldColumn(false)]
        public int ChapterId { get; set; }

        #endregion

        public CreateTopicModel()
        {
        }

        public CreateTopicModel(string topicName, int chapterId, IEnumerable<Course> courses,
            int? testCourseId, IEnumerable<TopicType> testTopicTypes, int? testTopicTypeId,
            int? theoryCourseId, IEnumerable<TopicType> theoryTopicTypes, int? theoryTopicTypeId)
        {
            //Test course
            TestCourses = courses
                    .Select(item => new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = false
                    })
                    .ToList();
            TestCourses.Insert(0, new SelectListItem()
            {
                Text = Localization.getMessage("NoCourse"),
                Value = Constants.NoCourseId.ToString(),
                Selected = false
            });

            TestTopicTypes = testTopicTypes
                        .Select(item => new SelectListItem
                        {
                            Text = Converter.ToString(item),
                            Value = item.Id.ToString(),
                            Selected = false
                        })
                        .ToList();
            BindTestCourse = testCourseId.HasValue;
            TestCourseId = testCourseId ?? 0;
            TestTopicTypeId = testTopicTypeId ?? 0;

            //Theory course
            TheoryCourses = courses
                    .Select(item => new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = false
                    })
                    .ToList();

            TheoryTopicTypes = theoryTopicTypes
                        .Select(item => new SelectListItem
                        {
                            Text = Converter.ToString(item),
                            Value = item.Id.ToString(),
                            Selected = false
                        })
                        .ToList();
            BindTheoryCourse = theoryCourseId.HasValue;
            TheoryCourseId = theoryCourseId ?? 0;
            TheoryTopicTypeId = theoryTopicTypeId ?? 0;

            TopicName = topicName;
            ChapterId = chapterId;
        }
    }
}