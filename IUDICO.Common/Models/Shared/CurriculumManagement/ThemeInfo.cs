using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Shared.CurriculumManagement
{
    public class TopicDescription
    {
        public Topic Topic { get; set; }
        public Chapter Chapter { get; set; }
        public Discipline Discipline { get; set; }
        public List<Timeline> Timelines { get; set; }

        public override string ToString()
        {
            string result = String.Format("{0}/{1}/{2}", Discipline.Name, Chapter.Name, Topic.Name);
            result = result + string.Concat(Timelines.Select(timeline => String.Format("({0} - {1}),",
                String.Format("{0:g}", timeline.StartDate), String.Format("{0:g}", timeline.EndDate))));
            return result.Remove(result.Length - 1);
        }
    }
}
