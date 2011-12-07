using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Search.Models.SearchResult
{
    public class NodeResult : ISearchResult
    {
        protected Node _Node;
        protected string _Text;
        protected string _Course;
        protected string _DateTime;

        public NodeResult(Node node, string course, string text, string datetime)
        {
            _Node = node;
            _Text = text;
            _Course = course;
            _DateTime = datetime;
        }

        public int GetId()
        {
            return _Node.Id;
        }

        public string GetName()
        {
            return _Node.Name;
        }

        public string GetText()
        {
            return !_Node.IsFolder ? _Text + Localization.getMessage("NodeName") + ": " + GetName() + "</br>" + Localization.getMessage("ParentCourse") + ": " + _Course + "</br>" + Localization.getMessage("Updated") + ": " + _DateTime + "</br>" + GetUrl() : Localization.getMessage("NodeName") + ": " + GetName() + "</br>" + Localization.getMessage("ParentCourse") + ": " + _Course + "</br>" + Localization.getMessage("Updated") + ": " + _DateTime + "</br>" + GetUrl();
        }

        public string GetUrl()
        {
            return "/Course/" + _Node.CourseId + "/Node/Index#" + _Node.Id;
        }
    }
}