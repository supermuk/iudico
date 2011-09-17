using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Shared.CurriculumManagement
{
    public class ThemeDescription
    {
        public Theme Theme { get; set; }
        public Stage Stage { get; set; }
        public Curriculum Curriculum { get; set; }
        public List<Timeline> Timelines { get; set; }

        public override string ToString()
        {
            string result = String.Format("{0}/{1}/{2}", Curriculum.Name, Stage.Name, Theme.Name);
            result = result + string.Concat(Timelines.Select(timeline => String.Format("({0} - {1}),",
                String.Format("{0:g}", timeline.StartDate), String.Format("{0:g}", timeline.EndDate))));
            return result.Remove(result.Length - 1);
        }
    }
}
