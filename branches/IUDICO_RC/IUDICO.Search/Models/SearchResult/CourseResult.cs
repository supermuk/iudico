using IUDICO.Common;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Search.Models.SearchResult
{
    public class CourseResult : ISearchResult
    {
        protected Course course;
        protected string update;
        protected string owner;
        
        public CourseResult(Course course, string update, string owner)
        {
            this.course = course;
            this.update = update;
            this.owner = owner;
        }

        public int GetId()
        {
            return this.course.Id;
        }

        public string GetName()
        {
            return this.course.Name;
        }

        public string GetText()
        {
            return Localization.GetMessage("CourseName") + ": " + this.GetName() + "</br" + Localization.GetMessage("Owner") + ": " + this.owner + "</br>" + Localization.GetMessage("Updated") + ": " + this.update + "</br>" + this.GetUrl();
        }

        public string GetUrl()
        {
            
            return "/Course/" + this.course.Id + "/Node/Index";
        }
    }
}