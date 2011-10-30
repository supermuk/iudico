using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.Common.Models;

namespace IUDICO.UnitTests.CurriculumManagement
{
    public class CurriculumComparer : IEqualityComparer<Curriculum>
    {
        public bool Equals(Curriculum x, Curriculum y)
        {
            return x.Name == y.Name &&
                x.Id == y.Id &&
                x.IsDeleted == y.IsDeleted;
        }

        public int GetHashCode(Curriculum obj)
        {
            return (obj.Name + obj.Id.ToString()).GetHashCode();
        }
    }
}
