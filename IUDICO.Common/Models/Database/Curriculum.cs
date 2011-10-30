//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IUDICO.Common.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Curriculum
    {
        public Curriculum()
        {
            this.CurriculumAssignments = new HashSet<CurriculumAssignment>();
            this.Stages = new HashSet<Stage>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Updated { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<CurriculumAssignment> CurriculumAssignments { get; set; }
        public virtual ICollection<Stage> Stages { get; set; }
    }
}
