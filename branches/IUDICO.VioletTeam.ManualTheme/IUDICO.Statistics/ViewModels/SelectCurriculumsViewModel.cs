using System.Collections.Generic;

namespace IUDICO.Statistics.ViewModels
{
    public class SelectCurriculumsViewModel
    {
        #region Constructors

        public SelectCurriculumsViewModel(string groupName, IEnumerable<CurriculumViewModel> curriculumViewModels)
        {
            GroupName = groupName;
            Curriculums = curriculumViewModels;
        }

        #endregion

        #region Public Properties

        public readonly string GroupName;

        public readonly IEnumerable<CurriculumViewModel> Curriculums;

        #endregion
    }
}