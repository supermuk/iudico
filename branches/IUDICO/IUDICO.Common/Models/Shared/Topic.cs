using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Shared
{
   partial class Topic
   {
      public TopicType TestTopicType
      {
         get { return this.TopicType; }
         set { this.TopicType = value; }
      }

      public TopicType TheoryTopicType
      {
         get { return this.TopicType1; }
         set { this.TopicType1 = value; }
      }
   }
}
