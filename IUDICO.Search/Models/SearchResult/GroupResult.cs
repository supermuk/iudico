using System;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Search.Models.SearchResult
{
    public class GroupResult : ISearchResult
    {
        protected Group group;

        public GroupResult(Group group)
        {
            this.group = group;
        }

        public int GetId()
        {
            return this.group.Id;
        }

        public string GetName()
        {
            return this.group.Name;
        }

        public string GetText()
        {
            return Localization.GetMessage("Group");
        }

        public string GetUrl()
        {

            return "/Group/Edit?id=" + this.group.Id;
        }
    }
}