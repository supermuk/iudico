using System;
using System.Collections.Generic;

namespace IUDICO.Analytics.Models
{
    public class UserTags
    {
        public Guid Id { get; set; }
        public Dictionary<int, double> Tags { get; set; }
    }
}