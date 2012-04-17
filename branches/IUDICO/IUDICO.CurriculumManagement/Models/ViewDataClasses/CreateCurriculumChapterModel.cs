using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class CreateCurriculumChapterModel
    {
        [LocalizedDisplayName("SetDate")]
        public bool SetDate { get; set; }

        [LocalizedDisplayName("StartDate")]
        [LocalizedRequired(ErrorMessage = "StartDateRequired")]
        [UIHint("DateTimeWithPicker")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [LocalizedDisplayName("EndDate")]
        [LocalizedRequired(ErrorMessage = "StartDateRequired")]
        [UIHint("DateTimeWithPicker")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public CreateCurriculumChapterModel()
        {
        }

        public CreateCurriculumChapterModel(DateTime? startDate, DateTime? endDate)
        {
            SetDate = startDate.HasValue;
            StartDate = startDate ?? DateTime.Now;
            EndDate = endDate ?? DateTime.Now;
        }
    }
}