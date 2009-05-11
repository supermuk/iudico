namespace IUDICO.DataModel.Common.TestingUtils
{
    class UserResult
    {
        private int _rank;

        private ResultStatus _status; 

        public UserResult()
        {
            _rank = 0;
            _status = ResultStatus.NoAnswer;
        }

        public void AddRank(int rank)
        {
            _rank += rank;
        }

        public void SetStatusPass()
        {
            _status = ResultStatus.Pass;
        }

        public void SetStatusFail()
        {
            _status = ResultStatus.Fail;
        }

        public void SetStatusEnqueued()
        {
            _status = ResultStatus.Enqueued;
            _rank = 0;
        }

        public void SetStatusNoAnswer()
        {
            _status = ResultStatus.NoAnswer;
            _rank = 0;
        }


        public int Rank
        {
            get { return _rank; }
        }

        public string Status
        {
            get { return _status.ToString(); }
        }
    }

    enum ResultStatus
    {
        Pass,
        Fail,
        Enqueued, 
        NoAnswer
    }
}
