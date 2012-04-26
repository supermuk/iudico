using System;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Search.Models.SearchResult
{
    public class TopicResult : ISearchResult
    {
        protected Topic topic;
        protected string course;
        public TopicResult(Topic topic, string course)
        {
            this.topic = topic;
            this.course = course;
        }

        public int GetId()
        {
            return this.topic.Id;
        }

        public string GetName()
        {
            return this.topic.Name;
        }

        public string GetText()
        {
            return Localization.GetMessage("TopicName") + ": " + this.GetName() + "</br>" + Localization.GetMessage("Course") + ": " + this.course + "</br>" + this.GetUrl();
        }

        public string GetUrl()
        {

            return "/Topic/" + this.topic.Id + "/Edit";
        }
    }
}