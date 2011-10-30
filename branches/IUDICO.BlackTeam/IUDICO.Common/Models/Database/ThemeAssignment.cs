namespace IUDICO.Common.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ThemeAssignment
    {
        public int Id { get; set; }
        public int ThemeRef { get; set; }
        public int CurriculumAssignmentRef { get; set; }
        public int MaxScore { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual CurriculumAssignment CurriculumAssignment { get; set; }
        public virtual Theme Theme { get; set; }
    }
}
