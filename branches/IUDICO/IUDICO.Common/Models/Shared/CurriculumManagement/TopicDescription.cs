using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Shared.CurriculumManagement
{
    public class TopicDescription
    {
        public TopicTypeEnum TopicType { get; set; }
        public TopicPart TopicPart { get; set; }
        public Topic Topic { get; set; }
        public Chapter Chapter { get; set; }
        public Discipline Discipline { get; set; }
        public Curriculum Curriculum { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Rating { get; set; }

        public override string ToString()
        {
            string result = String.Format("[{0}]:{1}/{2}/{3} (From {4} till {5})", 
                TopicPart.ToString(), Discipline.Name, Chapter.Name, Topic.Name, StartDate.ToString(), EndDate.ToString());
            return result;
        }
    }
}
