using System.ComponentModel;
using IUDICO.Common.Models.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class CreateCurriculumChapterTopicModel
    {
        [LocalizedDisplayName("ThresholdOfSuccess")]
        public int ThresholdOfSuccess { get; set; }

        [LocalizedDisplayName("BlockTopicAtTesting")]
        public bool BlockTopicAtTesting { get; set; }

        [LocalizedDisplayName("BlockCurriculumAtTesting")]
        public bool BlockCurriculumAtTesting { get; set; }

        [LocalizedDisplayName("SetTestTimeline")]
        public bool SetTestTimeline { get; set; }

        [LocalizedDisplayName("TestStartDate")]
        [LocalizedRequired(ErrorMessage = "StartDateRequired")]
        [UIHint("DateTimeWithPicker")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TestStartDate { get; set; }

        [LocalizedDisplayName("TestEndDate")]
        [LocalizedRequired(ErrorMessage = "StartDateRequired")]
        [UIHint("DateTimeWithPicker")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TestEndDate { get; set; }

        [LocalizedDisplayName("SetTheoryTimeline")]
        public bool SetTheoryTimeline { get; set; }

        [LocalizedDisplayName("TheoryStartDate")]
        [LocalizedRequired(ErrorMessage = "StartDateRequired")]
        [UIHint("DateTimeWithPicker")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TheoryStartDate { get; set; }

        [LocalizedDisplayName("TheoryEndDate")]
        [LocalizedRequired(ErrorMessage = "StartDateRequired")]
        [UIHint("DateTimeWithPicker")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TheoryEndDate { get; set; }

        public CreateCurriculumChapterTopicModel()
        {
        }

        public CreateCurriculumChapterTopicModel(
            int thresholdOfSuccess,
            bool blockTopicAtTesting,
            bool blockCurriculumAtTesting,
            DateTime? testStartDate,
            DateTime? testEndDate,
            DateTime? theoryStartDate,
            DateTime? theoryEndDate)
        {
            this.ThresholdOfSuccess = thresholdOfSuccess;
            this.BlockTopicAtTesting = blockTopicAtTesting;
            this.BlockCurriculumAtTesting = blockCurriculumAtTesting;

            this.SetTestTimeline = testStartDate.HasValue;
            this.TestStartDate = testStartDate ?? DateTime.Now;
            this.TestEndDate = testEndDate ?? DateTime.Now;

            this.SetTheoryTimeline = theoryStartDate.HasValue;
            this.TheoryStartDate = theoryStartDate ?? DateTime.Now;
            this.TheoryEndDate = theoryEndDate ?? DateTime.Now;
        }
    }
}