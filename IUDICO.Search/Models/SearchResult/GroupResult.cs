using System;
using IUDICO.Common.Models;

namespace IUDICO.Search.Models.SearchResult
{
    public class GroupResult : ISearchResult
    {
        protected Group _Group;

        public GroupResult(Group group)
        {
            _Group = group;
        }

        public int GetId()
        {
            return _Group.Id;
        }

        public String GetName()
        {
            return _Group.Name;
        }

        public String GetText()
        {
            return "group";
        }

        public String GetUrl()
        {

            return "/Group/Edit?id=" + _Group.Id;
        }
    }
}