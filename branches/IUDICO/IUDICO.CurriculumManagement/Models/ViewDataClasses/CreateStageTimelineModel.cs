using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class CreateChapterTimelineModel
    {
        public IEnumerable<SelectListItem> Chapters { get; set; }
        public int ChapterId { get; set; }
        public Timeline Timeline { get; set; }

        public CreateChapterTimelineModel()
        {
        }

        public CreateChapterTimelineModel(Timeline timeline, IEnumerable<Chapter> chapters, int chapterId)
        {
            Chapters = chapters
                    .Select(item => new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString(),
                        Selected = false
                    });
            Timeline = timeline;
            ChapterId = chapterId;
        }
    }
}