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
    
    public partial class Course
    {
        public Course()
        {
            this.CourseUsers = new HashSet<CourseUser>();
            this.Nodes = new HashSet<Node>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Updated { get; set; }
        public bool Deleted { get; set; }
        public Nullable<bool> Locked { get; set; }
        public string Sequencing { get; set; }
    
        public virtual ICollection<CourseUser> CourseUsers { get; set; }
        public virtual ICollection<Node> Nodes { get; set; }
    }
}
