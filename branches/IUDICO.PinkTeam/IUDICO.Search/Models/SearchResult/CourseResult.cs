using System;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Search.Models.SearchResult
{
    public class CourseResult : ISearchResult
    {
        protected Course _Course;
        protected string _Update;
        protected string _Owner;
        
        public CourseResult(Course course, string update, string owner)
        {
            _Course = course;
            _Update = update;
            _Owner = owner;
        }

        public int GetId()
        {
            return _Course.Id;
        }

        public String GetName()
        {
            return _Course.Name;
        }

        public String GetText()
        {
            return Localization.getMessage("CourseName") + ": " + GetName() + "</br" + Localization.getMessage("Owner") + ": " + _Owner + "</br>" + Localization.getMessage("Updated") + ": " + _Update + "</br>" + GetUrl();
        }

        public String GetUrl()
        {
            
            return "/Course/" + _Course.Id + "/Node/Index";
        }
    }
}