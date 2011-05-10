using System;
using IUDICO.Common.Models;

namespace IUDICO.Search.Models.SearchResult
{
    public class ThemeResult : ISearchResult
    {
        protected Theme _Theme;

        public ThemeResult(Theme theme)
        {
            _Theme = theme;
        }

        public int GetId()
        {
            return _Theme.Id;
        }

        public String GetName()
        {
            return _Theme.Name;
        }

        public String GetText()
        {
            return "theme";
        }

        public String GetUrl()
        {

            return "/Theme/" + _Theme.Id + "/Edit";
        }
    }
}