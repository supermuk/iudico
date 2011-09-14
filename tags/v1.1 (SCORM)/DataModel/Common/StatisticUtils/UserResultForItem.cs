using System;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Common.StatisticUtils
{
    public class UserResultForItem
    {
        private readonly int _userId;

        private readonly TblItems _item;

        private ResultStatus _status;

        private int _pageRank;

        private int _userRank;

        private readonly DateTime? _date;

        public UserResultForItem(int userId, TblItems item, DateTime? date)
        {
            _date = date;
            _userId = userId;
            _item = item;

            //_pageRank = (int)_page.PageRank;
        }

        public void Calc()
        {

            /*
            var questions = StudentRecordFinder.GetQuestionsForPage(_item.ID);

            foreach (var q in questions)
            {
                var status = (new UserResultForQuestion(_userId, q, _date)).Calc();

                if(status == ResultStatus.NotIncluded)
                {
                    SetNotIncluded();
                    return;
                }

                if (status == ResultStatus.Enqueued)
                {
                    SetEnqueued();
                    return;
                }

                if (status == ResultStatus.Empty)
                {
                    SetEmpty();
                    return;
                }

                if (status == ResultStatus.Pass)
                    _userRank += (int)q.Rank;
            }

            if (_userRank >= _pageRank)
                _status = ResultStatus.Pass;
            else
                _status = ResultStatus.Fail;
            */
        }

        private void SetEnqueued()
        {
            _userRank = 0;
            _status = ResultStatus.Enqueued;
        }

        private void SetEmpty()
        {
            _userRank = 0;
            _status = ResultStatus.Empty;
        }

        private void SetNotIncluded()
        {
            _pageRank = 0;
            _userRank = 0;
            _status = ResultStatus.NotIncluded;
        }

        public ResultStatus Status
        {
            get
            {
                return _status;
            }
        }

        public int UserRank
        {
            get { return _userRank; }
        }

        public int PageRank
        {
            get { return _pageRank; }
        }

        public TblItems Item
        {
            get { return _item; }
        }
    }
}
