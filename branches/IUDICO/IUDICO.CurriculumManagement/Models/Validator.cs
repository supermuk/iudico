using System;
using System.Linq;
using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement.Models.Storage;

namespace IUDICO.CurriculumManagement.Models
{
    /// <summary>
    /// Validator class.
    /// </summary>
    /// <remarks></remarks>
    public class Validator
    {
        private ICurriculumStorage Storage { get; set; }

        public Validator(ICurriculumStorage storage)
        {
            this.Storage = storage;
        }

        /// <summary>
        /// Validates the curriculum chapter.
        /// </summary>
        /// <param name="data">The curriculum chapter.</param>
        /// <returns></returns>
        public ValidationStatus ValidateCurriculumChapter(CurriculumChapter data)
        {
            var validationStatus = new ValidationStatus();

            ValidateDate(data.StartDate, data.EndDate, validationStatus);

            return validationStatus;
        }

        /// <summary>
        /// Validates the curriculum chapter topic.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public ValidationStatus ValidateCurriculumChapterTopic(CurriculumChapterTopic data)
        {
            var validationStatus = new ValidationStatus();

            ValidateDate(data.TestStartDate, data.TestEndDate, validationStatus);
            ValidateDate(data.TheoryStartDate, data.TheoryEndDate, validationStatus);

            if (data.MaxScore <= 0)
            {
                validationStatus.AddLocalizedError("MaxScoreMustBeGreaterThanZero");
            }

            return validationStatus;
        }

        /// <summary>
        /// Validates the curriculum.
        /// </summary>
        /// <param name="curriculum">The curriculum.</param>
        /// <returns></returns>
        public ValidationStatus ValidateCurriculum(Curriculum curriculum)
        {
            var validationStatus = new ValidationStatus();
            var currentGroupId = -1;
            // If update old curriculum
            if (curriculum.Id > 0)
            {
                var oldCurriculum = this.Storage.GetCurriculum(curriculum.Id);
                currentGroupId = oldCurriculum.UserGroupRef;
                curriculum.DisciplineRef = oldCurriculum.DisciplineRef;
            }

            if (curriculum.UserGroupRef > 0 && curriculum.DisciplineRef > 0)
            {
                var curriculums = this.Storage.GetCurriculums(c => c.DisciplineRef == curriculum.DisciplineRef).ToList();
                if (curriculums.Exists(item => item.UserGroupRef == curriculum.UserGroupRef) && curriculum.UserGroupRef != currentGroupId)
                {
                    validationStatus.AddLocalizedError("ChooseAnotherGroupForThisCurriculum");
                }
            }
            if (curriculum.UserGroupRef <= 0)
            {
                validationStatus.AddLocalizedError("ChooseGroup");
            }
            if (curriculum.DisciplineRef <= 0)
            {
                validationStatus.AddLocalizedError("ChooseDiscipline");
            }

            ValidateDate(curriculum.StartDate, curriculum.EndDate, validationStatus);

            return validationStatus;
        }

        private static void ValidateDate(DateTime? startDate, DateTime? endDate, ValidationStatus validationStatus)
        {
            DateTime minAllowedDate = Constants.MinAllowedDateTime;
            DateTime maxAllowedDate = Constants.MaxAllowedDateTime;
            if (endDate.HasValue ^ startDate.HasValue)
            {
                validationStatus.AddLocalizedError("ChooseStartAndEndDate");
            }
            else if (endDate.HasValue)
            {
                if (startDate > endDate)
                {
                    validationStatus.AddLocalizedError("StartDateMustLessThanEndDate");
                }
                if (startDate < minAllowedDate || startDate > maxAllowedDate)
                {
                    validationStatus.AddLocalizedError("StartDateMustBeBetween", minAllowedDate.ToString(), maxAllowedDate.ToString());
                }
                if (endDate < minAllowedDate || endDate > maxAllowedDate)
                {
                    validationStatus.AddLocalizedError("EndDateMustBeBetween", minAllowedDate.ToString(), maxAllowedDate.ToString());
                }
            }
        }
    }
}