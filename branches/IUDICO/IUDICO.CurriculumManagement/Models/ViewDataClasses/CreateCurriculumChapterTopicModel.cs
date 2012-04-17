using System.Collections.Generic;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class CreateCurriculumChapterTopicModel
    {
        [LocalizedDisplayName("MaxScore")]
        public int MaxScore { get; set; }

        [LocalizedDisplayName("BlockTopicAtTesting")]
        public bool BlockTopicAtTesting { get; set; }

        [LocalizedDisplayName("BlockCurriculumAtTesting")]
        public bool BlockCurriculumAtTesting { get; set; }

        [LocalizedDisplayName("SetTestDate")]
        public bool SetTestDate { get; set; }

        [LocalizedDisplayName("StartDate")]
        [LocalizedRequired(ErrorMessage = "StartDateRequired")]
        [UIHint("DateTimeWithPicker")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TestStartDate { get; set; }

        [LocalizedDisplayName("EndDate")]
        [LocalizedRequired(ErrorMessage = "StartDateRequired")]
        [UIHint("DateTimeWithPicker")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TestEndDate { get; set; }

        [LocalizedDisplayName("SetTheoryDate")]
        public bool SetTheoryDate { get; set; }

        [LocalizedDisplayName("StartDate")]
        [LocalizedRequired(ErrorMessage = "StartDateRequired")]
        [UIHint("DateTimeWithPicker")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TheoryStartDate { get; set; }

        [LocalizedDisplayName("EndDate")]
        [LocalizedRequired(ErrorMessage = "StartDateRequired")]
        [UIHint("DateTimeWithPicker")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TheoryEndDate { get; set; }

        public CreateCurriculumChapterTopicModel()
        {
        }

        public CreateCurriculumChapterTopicModel(int maxScore, bool blockTopicAtTesting, bool blockCurriculumAtTesting,
            DateTime? testStartDate, DateTime? testEndDate, DateTime? theoryStartDate, DateTime? theoryEndDate)
        {
            MaxScore = maxScore;
            BlockTopicAtTesting = blockTopicAtTesting;
            BlockCurriculumAtTesting = blockCurriculumAtTesting;

            SetTestDate = testStartDate.HasValue;
            TestStartDate = testStartDate ?? DateTime.Now;
            TestEndDate = testEndDate ?? DateTime.Now;

            SetTheoryDate = theoryStartDate.HasValue;
            TheoryStartDate = theoryStartDate ?? DateTime.Now;
            TheoryEndDate = theoryEndDate ?? DateTime.Now;
        }
    }
}