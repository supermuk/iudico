using System;
using IUDICO.Common;
using IUDICO.Common.Models.Shared;


namespace IUDICO.Search.Models.SearchResult
{
    public class UserResult : ISearchResult
    {
        protected User user;
        protected string role;

        public UserResult(User user)
        {
            this.user = user;
        }

        public UserResult(User user, string role)
        {
            this.user = user;
            this.role = role;
        }

        public int GetId()
        {
            return Convert.ToInt32(this.user.Id);
        }

        public string GetName()
        {
            return this.user.Name;
        }

        public string GetText()
        {
            return Localization.GetMessage("User") + ": " + this.GetName() + "</br>" + Localization.GetMessage("Role") + ": " + this.role;
        }

        public string GetUrl()
        {
            return "/User/Details?id=" + this.user.Id.ToString();
        }
    }
}