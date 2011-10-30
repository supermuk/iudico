namespace IUDICO.Common.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Node
    {
        public Node()
        {
            this.ChildNodes = new HashSet<Node>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public bool IsFolder { get; set; }
        public int Position { get; set; }
        public string Sequencing { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual ICollection<Node> ChildNodes { get; set; }
        public virtual Node ParentNode { get; set; }
    }
}
