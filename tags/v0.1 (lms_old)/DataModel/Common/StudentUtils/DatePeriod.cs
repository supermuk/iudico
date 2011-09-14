using System;
using System.Collections.Generic;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Common.StudentUtils
{
    class DatePeriod
    {
        private readonly DateTime? _start;

        private readonly DateTime? _end;

        public DatePeriod(TblPermissions permission)
        {
            _start = permission.DateSince;
            _end = permission.DateTill;
        }

        public DateTime? Start
        {
            get { return _start; }
        }

        public DateTime? End
        {
            get { return _end; }
        }

        public static IList<DatePeriod> ExtractPeriodsFromPermissions(IList<TblPermissions> permissions)
        {
            var periods = new List<DatePeriod>();

            foreach (var p in permissions)
                periods.Add(new DatePeriod(p));

            return periods;
        }
    }
}
