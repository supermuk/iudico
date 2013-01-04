using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.CurriculumManagement.Models.Enums {
   public enum ValidationErrorType {
      Group,
      Discipline,
      ChapterTimeline,
      TopicTimelineOutOfChapter,
      TopicTimelineOutOfCurriculum
   }
}