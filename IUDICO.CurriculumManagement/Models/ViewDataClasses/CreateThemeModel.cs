using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using System.Reflection;
using IUDICO.Common.Models.Shared;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class CreateTopicModel
    {
        public List<SelectListItem> Courses { get; set; }
        public List<SelectListItem> TopicTypes { get; set; }
        public int CourseId { get; set; }
        public int ChapterId { get; set; }
        public int TopicTypeId { get; set; }
        public string TopicName { get; set; }

        public CreateTopicModel()
        {
        }

        public CreateTopicModel(int chapterId, IEnumerable<Course> courses, int? courseId, IEnumerable<TopicType> topicTypes, int topicTypeId, string topicName)
        {
            ChapterId = chapterId;
            Courses = courses
                    .Select(item => new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = false
                    })
                    .ToList();
            Courses.Insert(0, new SelectListItem()
            {
                Text = Localization.getMessage("No course"),
                Value = Constants.NoCourseId.ToString(),
                Selected = false
            });

            TopicTypes = topicTypes
                        .Select(item => new SelectListItem
                        {
                            Text = Converters.ConvertToString(item),
                            Value = item.Id.ToString(),
                            Selected = false
                        })
                        .ToList();
            CourseId = courseId ?? Constants.NoCourseId;
            TopicTypeId = topicTypeId;
            TopicName = topicName;
        }
    }
}