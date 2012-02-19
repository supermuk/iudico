using System;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Search.Models.SearchResult
{
    public class TopicResult : ISearchResult
    {
        protected Topic _Topic;
        protected string _Course;
        public TopicResult(Topic topic, string course)
        {
            _Topic = topic;
            _Course = course;
        }

        public int GetId()
        {
            return _Topic.Id;
        }

        public String GetName()
        {
            return _Topic.Name;
        }

        public String GetText()
        {
            return Localization.getMessage("TopicName") + ": " + GetName() + "</br>" + Localization.getMessage("Course") + ": " + _Course + "</br>" + GetUrl();
        }

        public String GetUrl()
        {

            return "/Topic/" + _Topic.Id + "/Edit";
        }
    }
}