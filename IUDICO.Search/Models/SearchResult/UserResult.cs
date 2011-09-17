using System;
using IUDICO.Common.Models;


namespace IUDICO.Search.Models.SearchResult
{
    public class UserResult : ISearchResult
    {
        protected User _User;
        protected string _Role;

        public UserResult(User user, string role)
        {
            _User = user;
            _Role = role;
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
            return  Localization.getMessage("User") + ": " + GetName() + "</br>" + Localization.getMessage("Role") + ": " + _Role;
        }

        public String GetUrl()
        {
            return "/User/Details?id=" + _User.Id.ToString();
        }
    }
}