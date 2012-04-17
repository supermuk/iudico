using System;
using IUDICO.Common.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using IUDICO.DisciplineManagement.Models;
using IUDICO.DisciplineManagement.Models.Storage;
using IUDICO.DisciplineManagement.Models.ViewDataClasses;

namespace IUDICO.DisciplineManagement.Controllers
{
    /// <summary>
    /// DisciplineBaseController.
    /// </summary>
    public class DisciplineBaseController : PluginController
    {
        protected IDisciplineStorage Storage { get; private set; }
        protected Validator Validator { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisciplineBaseController"/> class.
        /// </summary>
        /// <param name="disciplineStorage">The discipline storage.</param>
        public DisciplineBaseController(IDisciplineStorage disciplineStorage)
        {
            Storage = disciplineStorage;
            Validator = new Validator(Storage);
        }

        /// <summary>
        /// Adds errors to model state.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public void AddValidationErrorsToModelState(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(new Guid().ToString(), error);
            }
        }

        //TODO: FatTony: move to utils
        protected ViewTopicModel ToViewTopicModel(Topic topic)
        {
            return new ViewTopicModel
            {
                Id = topic.Id,
                ChapterId = topic.ChapterRef,
                Created = Converter.ToString(topic.Created),
                Updated = Converter.ToString(topic.Updated),
                TestCourseName =
                    topic.TestCourseRef.HasValue && topic.TestCourseRef != Constants.NoCourseId
                        ? Storage.GetCourse(topic.TestCourseRef.Value).Name
                        : String.Empty,
                TestTopicType = topic.TestTopicTypeRef.HasValue
                                    ? Converter.ToString(
                                        Storage.GetTopicType(topic.TestTopicTypeRef.Value))
                                    : String.Empty,
                TheoryCourseName = topic.TheoryCourseRef.HasValue
                                       ? Storage.GetCourse(topic.TheoryCourseRef.Value).Name
                                       : String.Empty,
                TheoryTopicType = topic.TheoryTopicTypeRef.HasValue
                                      ? Converter.ToString(
                                          Storage.GetTopicType(topic.TheoryTopicTypeRef.Value))
                                      : String.Empty,
                TopicName = topic.Name
            };
        }

        protected Topic ToTopic(CreateTopicModel model)
        {
            return new Topic
            {
                ChapterRef = model.ChapterId,
                Name = model.TopicName,
                TestCourseRef = model.BindTestCourse ? model.TestCourseId : (int?)null,
                TestTopicTypeRef = model.BindTestCourse ? model.TestTopicTypeId : (int?)null,
                TheoryCourseRef = model.BindTheoryCourse ? model.TheoryCourseId : (int?)null,
                TheoryTopicTypeRef = model.BindTheoryCourse ? model.TheoryTopicTypeId : (int?)null
            };
        }

        protected CreateTopicModel ToCreateTopicModel(Topic topic)
        {
            return new CreateTopicModel(topic.Name,
                                        topic.ChapterRef,
                                        Storage.GetCourses(),
                                        topic.TestCourseRef,
                                        Storage.GetTestTopicTypes(),
                                        topic.TestTopicTypeRef,
                                        topic.TheoryCourseRef,
                                        Storage.GetTheoryTopicTypes(),
                                        topic.TheoryTopicTypeRef);
        }
    }
}