using IUDICO.Common.Models.Shared;
using IUDICO.Common;

namespace IUDICO.Search.Models.SearchResult
{
    public class NodeResult : ISearchResult
    {
        protected Node node;
        protected string text;
        protected string course;
        protected string dateTime;

        public NodeResult(Node node, string course, string text, string datetime)
        {
            this.node = node;
            this.text = text;
            this.course = course;
            this.dateTime = datetime;
        }

        public int GetId()
        {
            return this.node.Id;
        }

        public string GetName()
        {
            return this.node.Name;
        }

        public string GetText()
        {
            return !this.node.IsFolder ? this.text + Localization.GetMessage("NodeName") + ": " + this.GetName() + "</br>" + Localization.GetMessage("ParentCourse") + ": " + this.course + "</br>" + Localization.GetMessage("Updated") + ": " + this.dateTime + "</br>" + this.GetUrl() : Localization.GetMessage("NodeName") + ": " + this.GetName() + "</br>" + Localization.GetMessage("ParentCourse") + ": " + this.course + "</br>" + Localization.GetMessage("Updated") + ": " + this.dateTime + "</br>" + this.GetUrl();
        }

        public string GetUrl()
        {
            return "/Course/" + this.node.CourseId + "/Node/Index#" + this.node.Id;
        }
    }
}