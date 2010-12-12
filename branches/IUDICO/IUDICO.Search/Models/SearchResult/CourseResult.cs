using System;
using IUDICO.Common.Models;

namespace IUDICO.Search.Models.SearchResult
{
    public class CourseResult : ISearchResult
    {
        protected Course _Course;

        public CourseResult(Course course)
        {
            _Course = course;
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
            return "";
        }

        public String GetUrl()
        {
            return "/Course/" + _Course.Id + "/Node/Index";
        }
    }
}