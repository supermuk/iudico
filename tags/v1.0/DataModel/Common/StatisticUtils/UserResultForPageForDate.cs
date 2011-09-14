using System;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Common.StatisticUtils
{
    public class UserResultForPageForDate : IComparable<UserResultForPageForDate>
    {
        public string ThemeName
        { 
            get
            {
                return StudentRecordFinder.GetTheme(_theme.ID).Name;
            }
        }

        public string PageName
        {
            get
            {
                return _item.Title;
            }
        }

        private readonly int _userId;

        private readonly TblThemes _theme;

        private readonly TblItems _item;
        //private readonly TblPages _page;

        public DateTime Date { get; set; }


        public UserResultForPageForDate(TblUserAnswers ua, int userId)
        {
            Date = (DateTime)ua.Date;

            //_page = StudentRecordFinder.GetPageForQuestion((int) ua.QuestionRef);

            _userId = userId;
        }

        public int CompareTo(UserResultForPageForDate other)
        {
            return other.Date.CompareTo(Date);
        }

        public bool Equals(UserResultForPageForDate other)
        {
            throw new NotImplementedException();
            /*
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._userId == _userId && Equals(other._page, _page) && other.Date.Equals(Date);
             * */
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
            /*
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (UserResultForPageForDate)) return false;
            return Equals((UserResultForPageForDate) obj);
             * */
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = _userId;
                result = (result*397) ^ (_item != null ? _item.GetHashCode() : 0);
                result = (result*397) ^ Date.GetHashCode();
                return result;
            }
        }

        public ResultStatus GetStatus()
        {
            var status = new UserResultForItem(_userId, _item, Date);
            status.Calc();

            return status.Status;
        }
    }
}