using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IUDICO.Common.Models
{
    [MetadataType(typeof(Course.Metadata))]
    [Bind(Exclude="Id")]
    public partial class Course
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            public string Name { get; set; }

            [DisplayName("Owner")]
            public string Owner { get; set; }

            [DisplayName("Created Date")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [DisplayName("Last Updated")]
            [ScaffoldColumn(false)]
            public DateTime Updated { get; set; }

            [ScaffoldColumn(false)]
            public bool Deleted { get; set; }
        }
    }

    [MetadataType(typeof(Curriculum.Metadata))]
    [Bind(Exclude = "Id")]
    public partial class Curriculum
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            public string Name { get; set; }

            [DisplayName("Created Date")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [DisplayName("Last Updated")]
            [ScaffoldColumn(false)]
            public DateTime Updated { get; set; }
        }
    }

    [MetadataType(typeof(Theme.Metadata))]
    [Bind(Exclude = "Id")]
    public partial class Theme
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            public string Name { get; set; }

            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [ScaffoldColumn(false)]
            public DateTime Updated { get; set; }

            [ScaffoldColumn(false)]
            public int StageRef { get; set; }

            [ScaffoldColumn(false)]
            public int CourseRef { get; set; }

            [ScaffoldColumn(false)]
            public int SortOrder { get; set; }
        }
    }
    
    [MetadataType(typeof(Stage.Metadata))]
    [Bind(Exclude = "Id, CurriculumRef")]
    public partial class Stage
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50.")]
            public string Name { get; set; }

            [DisplayName("Created Date")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [DisplayName("Last Updated")]
            [ScaffoldColumn(false)]
            public DateTime Updated { get; set; }

            [ScaffoldColumn(false)]
            public int CurriculumRef { get; set; }
        }
    }

    [MetadataType(typeof(Node.Metadata))]
    [Bind(Exclude = "Id, CourseId")]
    public partial class Node
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            public string Name { get; set; }

            [ScaffoldColumn(false)]
            public int? ParentId { get; set; }

            [ScaffoldColumn(false)]
            public int CourseId { get; set; }

            [DisplayName("Is Folder")]
            public bool IsFolder { get; set; }

            [ScaffoldColumn(false)]
            public int Position { get; set; }
        }
    }

    [MetadataType(typeof(User.Metadata))]
    [Bind(Exclude = "ID")]
    public partial class User
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public Guid ID { get; set; }

            [DisplayName("Username")]
            [Required(ErrorMessage = "Username is required")]
            [StringLength(100, ErrorMessage = "Username can not be longer than 100")]
            public string Username { get; set; }

            [DisplayName("Password")]
            [Required(ErrorMessage = "Password is required")]
            [StringLength(50, ErrorMessage = "Password can not be longer than 50")]
            public string Password { get; set; }

            [DisplayName("Email")]
            [Required(ErrorMessage = "Email is required")]
            [StringLength(100, ErrorMessage = "Email can not be longer than 100")]
            public string Email { get; set; }

            [DisplayName("OpenID")]
            [Required(ErrorMessage = "OpenID is required")]
            [StringLength(200, ErrorMessage = "OpenID can not be longer than 200")]
            public string OpenID { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            [StringLength(200, ErrorMessage = "Name can not be longer than 200")]
            public string Name { get; set; }

            [ScaffoldColumn(false)]
            public int RoleID { get; set; }
        }
    }

    [MetadataType(typeof(Role.Metadata))]
    [Bind(Exclude = "ID")]
    public partial class Role
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int ID { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50")]
            public string Name { get; set; }
        }
    }

    [MetadataType(typeof(Group.Metadata))]
    [Bind(Exclude = "ID")]
    public partial class Group
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int ID { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50")]
            public string Name { get; set; }
        }
    }
}