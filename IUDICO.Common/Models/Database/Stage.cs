namespace IUDICO.Common.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stage
    {
        public Stage()
        {
            this.Themes = new HashSet<Theme>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Updated { get; set; }
        public int CurriculumRef { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Curriculum Curriculum { get; set; }
        public virtual ICollection<Theme> Themes { get; set; }
    }
}
