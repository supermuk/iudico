using SimpleLucene.Impl;

namespace IUDICO.Search.Models.Queries
{
    public class UserQuery : DefaultQuery
    {
        public UserQuery()
            : base(new[] { "Name", "Username" })
        {

        }
    }
}