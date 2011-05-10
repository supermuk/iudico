using System;
using IUDICO.Common.Models;


namespace IUDICO.Search.Models.SearchResult
{
    public class UserResult : ISearchResult
    {
        protected User _User;

        public UserResult(User user)
        {
            _User = user;
        }

        public int GetId()
        {
            return Convert.ToInt32(_User.Id);
        }

        public String GetName()
        {
            return _User.Name;
        }

        public String GetText()
        {
            return "user";
        }

        public String GetUrl()
        {
            return "/User/Details?id=" + _User.Id.ToString();
        }
    }
}