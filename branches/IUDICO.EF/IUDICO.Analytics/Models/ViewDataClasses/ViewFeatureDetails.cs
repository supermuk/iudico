using System.Collections.Generic;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Analytics.Models.ViewDataClasses
{
    public class ViewFeatureDetails
    {
        public ViewFeatureDetails(Feature feature, IEnumerable<Topic> topics)
        {
            Feature = feature;
            Topics = topics;
        }

        public ViewFeatureDetails(Feature feature, IEnumerable<Topic> topics, IEnumerable<Topic> availableTopics) : this(feature, topics)
        {
            AvailableTopics = availableTopics;
        }

        public Feature Feature { get; set; }
        public IEnumerable<Topic> Topics { get; set; }
        public IEnumerable<Topic> AvailableTopics { get; set; }
    }
}