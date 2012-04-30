﻿using System;

using IUDICO.Common.Models.Shared;
using IUDICO.Search.Models.Queries;

using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace IUDICO.Search.Models.Definitions
{
    public class CourseDefinition : Definition<Course>
    {
        public CourseDefinition()
        {
            this.Query = new DefaultQuery(new[] { "Course" });
        }

        public override Document Convert(Course entity)
        {
            throw new NotImplementedException();
        }

        public override Term GetIndex(Course entity)
        {
            throw new NotImplementedException();
        }

        public override Course Convert(Document document)
        {
            throw new NotImplementedException();
        }
    }
}