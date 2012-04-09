using System.Collections.Generic;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Analytics.Models.ViewDataClasses
{
    public class ViewTagDetails
    {
        public ViewTagDetails(Tag tag, IEnumerable<Topic> topics)
        {
            Tag = tag;
            Topics = topics;
        }

        public ViewTagDetails(Tag tag, IEnumerable<Topic> topics, IEnumerable<Topic> availableTopics)
            : this(tag, topics)
        {
            AvailableTopics = availableTopics;
        }

        public Tag Tag { get; set; }
        public IEnumerable<Topic> Topics { get; set; }
        public IEnumerable<Topic> AvailableTopics { get; set; }
    }
}