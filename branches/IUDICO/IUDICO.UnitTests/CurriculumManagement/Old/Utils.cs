using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.UnitTests
{
    public static class Utils
    {
        public static Discipline GetDefaultDiscipline()
        {
            return new Discipline() { Name = "Discipline" };
        }

        public static Chapter GetDefaultChapter(int disciplineId)
        {
            return new Chapter() { DisciplineRef = disciplineId, Name = "Chapter" };
        }

        public static Topic GetDefaultTopic(int chapterId, int courseId)
        {
            return new Topic() { ChapterRef = chapterId, CourseRef = courseId, Name = "Topic", TopicTypeRef = 1 };
        }

        public static Curriculum GetDefaultCurriculum(int disciplineId, int groupId)
        {
            return new Curriculum() { UserGroupRef = groupId, DisciplineRef = disciplineId };
        }
    }
}
