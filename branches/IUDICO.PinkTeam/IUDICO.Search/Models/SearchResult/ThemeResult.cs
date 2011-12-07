using System;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Search.Models.SearchResult
{
    public class ThemeResult : ISearchResult
    {
        protected Theme _Theme;
        protected string _Course;
        public ThemeResult(Theme theme, string course)
        {
            _Theme = theme;
            _Course = course;
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
            return Localization.getMessage("ThemeName") + ": " + GetName() + "</br>" + Localization.getMessage("Course") + ": " + _Course + "</br>" + GetUrl();
        }

        public String GetUrl()
        {

            return "/Theme/" + _Theme.Id + "/Edit";
        }
    }
}