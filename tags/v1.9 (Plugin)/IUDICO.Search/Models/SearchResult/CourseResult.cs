using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;

namespace IUDICO.Search.Models.SearchResult
{
    public class CourseResult : ISearchResult
    {
        protected Course course;

        public CourseResult(Course course)
        {
            this.course = course;
        }

        public int GetID()
        {
            return course.Id;
        }

        public String GetName()
        {
            return course.Name;
        }

        public String GetText()
        {
            return "";
        }

        public String GetUrl()
        {
            return "/Course/" + course.Id.ToString() + "/Node/Index";
        }
    }
}