using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.Common.Models
{
    [MetadataType(typeof(Metadata))]
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

    [MetadataType(typeof(Metadata))]
    [Bind(Exclude = "Id")]
    public partial class Curriculum
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50.")]
            public string Name { get; set; }

            [DisplayName("Create Date")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [DisplayName("Update Date")]
            [ScaffoldColumn(false)]
            public DateTime Updated { get; set; }

            [ScaffoldColumn(false)]
            public bool IsDeleted { get; set; }
        }
    }

    [MetadataType(typeof(Metadata))]
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

            [DisplayName("Create Date")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [DisplayName("Update Date")]
            [ScaffoldColumn(false)]
            public DateTime Updated { get; set; }

            [ScaffoldColumn(false)]
            public int CurriculumRef { get; set; }

            [ScaffoldColumn(false)]
            public bool IsDeleted { get; set; }
        }
    }

    [MetadataType(typeof(Metadata))]
    [Bind(Exclude = "Id, StageRef")]
    public partial class Theme
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [DisplayName("Name")]
            [ScaffoldColumn(false)]
            public string Name { get; set; }

            [DisplayName("Create Date")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [DisplayName("Update Date")]
            [ScaffoldColumn(false)]
            public DateTime Updated { get; set; }

            [ScaffoldColumn(false)]
            public int StageRef { get; set; }

            [ScaffoldColumn(false)]
            public int CourseRef { get; set; }

            [ScaffoldColumn(false)]
            public int SortOrder { get; set; }

            [ScaffoldColumn(false)]
            public bool IsDeleted { get; set; }
        }
    }

    [MetadataType(typeof(Metadata))]
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

    [MetadataType(typeof(Metadata))]
    [Bind(Exclude = "Id")]
    public partial class User
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public Guid Id { get; set; }

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
            public string OpenId { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            [StringLength(200, ErrorMessage = "Name can not be longer than 200")]
            public string Name { get; set; }
        }
    }

    [MetadataType(typeof(Metadata))]
    [Bind(Exclude = "Id")]
    public partial class Role
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50")]
            public string Name { get; set; }
        }
    }

    [MetadataType(typeof(Metadata))]
    [Bind(Exclude = "Id")]
    public partial class Group
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50")]
            public string Name { get; set; }
        }
    }

    [MetadataType(typeof(Metadata))]
    public partial class GroupUser
    {
        public IEnumerable<SelectListItem> GroupList { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }

        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int GroupRef { get; set; }

            [ScaffoldColumn(false)]
            public int UserRef { get; set; }

            [DropDownList(OptionLabel = "Select Group", TargetProperty = "GroupRef")]
            [DisplayName("Group")]
            public IEnumerable<SelectListItem> GroupList { get; set; }

            [DropDownList(OptionLabel = "Select User", TargetProperty = "UserRef")]
            [DisplayName("User")]
            public IEnumerable<SelectListItem> UserList { get; set; }
        }
    }
}