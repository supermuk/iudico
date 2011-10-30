namespace IUDICO.Common.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ThemeType
    {
        public ThemeType()
        {
            this.Themes = new HashSet<Theme>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Theme> Themes { get; set; }
    }
}
