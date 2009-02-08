using System;

namespace IUDICO.DataModel.Common
{
    public struct DateTimeInterval
    {
        public static DateTimeInterval Full;

        public readonly DateTime? From;
        public readonly DateTime? To;

        public DateTimeInterval(DateTime? from, DateTime? to)
        {
            From = from;
            To = to;
        }

        public bool Contains(DateTime d)
        {
            return (From == null || From.Value <= d) && (To == null || To.Value >= d);
        }

        public bool IsFull { get { return From == null && To == null; } }
    }
}
