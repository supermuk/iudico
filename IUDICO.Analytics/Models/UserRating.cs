using System;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Analytics.Models
{
    public class UserRating : IComparable<UserRating>
    {
        public UserRating(User user, double score)
        {
            User = user;
            Score = score;
        }

        public User User { get; protected set; }
        public double Score { get; protected set; }

        #region Implementation of IComparable<in UserRating>

        public int CompareTo(UserRating other)
        {
            return other.Score.CompareTo(Score);
        }

        #endregion
    }
}