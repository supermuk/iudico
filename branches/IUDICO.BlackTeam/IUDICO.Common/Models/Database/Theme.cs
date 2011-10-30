namespace IUDICO.Common.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Theme
    {
        public Theme()
        {
            this.ThemeAssignments = new HashSet<ThemeAssignment>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Updated { get; set; }
        public int StageRef { get; set; }
        public int CourseRef { get; set; }
        public int SortOrder { get; set; }
        public int ThemeTypeRef { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Stage Stage { get; set; }
        public virtual ICollection<ThemeAssignment> ThemeAssignments { get; set; }
        public virtual ThemeType ThemeType { get; set; }
    }
}
