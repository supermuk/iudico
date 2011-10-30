namespace IUDICO.Common.Models
{
    using System;
    using System.Collections.Generic;

    public partial class User
    {
        public User()
        {
            //this.User1 = new HashSet<User>();
            this.Groups = new HashSet<Group>();
        }
    
        public System.Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string OpenId { get; set; }
        public string Name { get; set; }
        public bool IsApproved { get; set; }
        public int RoleId { get; set; }
        public bool Deleted { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.Guid> ApprovedBy { get; set; }
    
        /*public virtual ICollection<User> User1 { get; set; }
        public virtual User User2 { get; set; }*/
        public virtual ICollection<Group> Groups { get; set; }
    }
}
