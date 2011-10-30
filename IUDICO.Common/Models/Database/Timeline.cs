namespace IUDICO.Common.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Timeline
    {
        public int Id { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int CurriculumAssignmentRef { get; set; }
        public Nullable<int> StageRef { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual CurriculumAssignment CurriculumAssignment { get; set; }
    }
}
