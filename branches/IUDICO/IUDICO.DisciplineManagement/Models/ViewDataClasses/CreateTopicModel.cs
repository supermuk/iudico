using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Attributes;
using System.ComponentModel.DataAnnotations;
using IUDICO.Common.Models.Shared.DisciplineManagement;

namespace IUDICO.DisciplineManagement.Models.ViewDataClasses
{
    public class CreateTopicModel
    {
        #region Properties

        public List<SelectListItem> TestCourses { get; set; }
        public List<SelectListItem> TheoryCourses { get; set; }

        [LocalizedDisplayName("Name")]
        [Required(ErrorMessage = "*")]
        public string TopicName { get; set; }

        [DropDownList(SourceProperty = "TestCourses")]
        [LocalizedDisplayName("ChooseCourseForTestTopic")]
        public int TestCourseId { get; set; }

        [DropDownList(SourceProperty = "TheoryCourses")]
        [LocalizedDisplayName("ChooseCourseForTheoryTopic")]
        public int TheoryCourseId { get; set; }

        [ScaffoldColumn(false)]
        public int ChapterId { get; set; }

        #endregion

        public CreateTopicModel()
        {
        }

        public CreateTopicModel(IList<Course> courses, Topic topic)
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
            TestCourses.Insert(0, new SelectListItem()
            {
                Text = Converter.ToString(TopicTypeEnum.TestWithoutCourse),
                Value = Constants.TestWithoutCourseId.ToString(),
                Selected = false
            });

            switch (topic.TestTopicTypeRef)
            {
                case ((int) TopicTypeEnum.TestWithoutCourse):
                    TestCourseId = Constants.TestWithoutCourseId;
                    break;
                case (null):
                    TestCourseId = Constants.NoCourseId;
                    break;
                default:
                    TestCourseId = topic.TestCourseRef.Value;
                    break;
            }

            //Theory course
            TheoryCourses = courses
                    .Select(item => new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = false
                    })
                    .ToList();
            TheoryCourses.Insert(0, new SelectListItem()
            {
                Text = Localization.getMessage("NoCourse"),
                Value = Constants.NoCourseId.ToString(),
                Selected = false
            });

            TheoryCourseId = topic.TheoryTopicTypeRef == null
                                 ? Constants.NoCourseId
                                 : topic.TheoryCourseRef.Value;

            TopicName = topic.Name;
            ChapterId = topic.ChapterRef;
        }
    }
}