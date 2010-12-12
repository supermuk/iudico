using IUDICO.Common.Models;

namespace IUDICO.Search.Models.SearchResult
{
    public class NodeResult : ISearchResult
    {
        protected Node _Node;
        protected string _Text;

        public NodeResult(Node node, string text)
        {
            _Node = node;
            _Text = text;
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
            return !_Node.IsFolder ? _Text : "";
        }

        public string GetUrl()
        {
            return "/Course/" + _Node.CourseId + "/Node/Index#" + _Node.Id;
        }
    }
}