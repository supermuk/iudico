using System;

using IUDICO.Common.Models.Shared.DisciplineManagement;

namespace IUDICO.Common.Models.Shared.CurriculumManagement
{
    public class TopicDescription
    {
        public TopicTypeEnum TopicType { get; set; }
        public TopicPart TopicPart { get; set; }
        public int? CourseId { get; set; }
        public int? CurriculumChapterTopicId { get; set; }
        public Topic Topic { get; set; }
        public Chapter Chapter { get; set; }
        public Discipline Discipline { get; set; }
        public Curriculum Curriculum { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override string ToString()
        {
            string result = string.Format(
                "[{0}]:{1}/{2}/{3} (From {4} till {5})",
                this.TopicPart.ToString(),
                this.Discipline.Name,
                this.Chapter.Name,
                this.Topic.Name,
                this.StartDate.ToString(),
                this.EndDate.ToString());
            
            return result;
        }
    }
}
