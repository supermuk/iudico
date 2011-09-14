using System.Collections.Generic;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Common.StudentUtils
{
    class ControlInfo
    {
        private TblCurriculums _curriculumn;

        private readonly TblStages _stage;

        private readonly TblThemes _theme;

        private readonly IList<DatePeriod> _datePeriods;

        private readonly bool _isControlStartsNow;

        public ControlInfo()
        {
            _isControlStartsNow = false;
        }

        public ControlInfo(TblStages stage, TblThemes theme, IList<DatePeriod> datePeriods)
        {
            _stage = stage;
            _theme = theme;
            _datePeriods = datePeriods;
            _isControlStartsNow = true;
        }

        public bool IsControlStartsNow
        {
            get { return _isControlStartsNow; }
        }

        public void AddCurriculumnToInfo(TblCurriculums curriculumn)
        {
            if(_curriculumn == null)
                _curriculumn = curriculumn;
        }

        public TblCurriculums Curriculumn
        {
            get { return _curriculumn; }
        }

        public TblStages Stage
        {
            get { return _stage; }
        }

        public TblThemes Theme
        {
            get { return _theme; }
        }

        public IList<DatePeriod> DatePeriods
        {
            get { return _datePeriods; }
        }
    }
}
