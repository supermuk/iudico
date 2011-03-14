﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using IUDICO.Common.Models.Attributes;
using System.Data.Linq;
using System.Linq;

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
            [ScaffoldColumn(false)]
            public string Owner { get; set; }

            [DisplayName("Created Date")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [DisplayName("Last Updated")]
            [ScaffoldColumn(false)]
            public DateTime Updated { get; set; }

            [ScaffoldColumn(false)]
            public bool Deleted { get; set; }

            [ScaffoldColumn(false)]
            public bool Locked { get; set; }

            [ScaffoldColumn(false)]
            public int SequencingPattern { get; set; }

            [ScaffoldColumn(false)]
            public object CourseUsers { get; set; }

            [ScaffoldColumn(false)]
            public object Nodes { get; set; }
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

            [ScaffoldColumn(false)]
            public EntitySet<CurriculumAssignment> CurriculumAssignments { get; set; }

            [ScaffoldColumn(false)]
            public EntitySet<Stage> Stages { get; set; }
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

            [ScaffoldColumn(false)]
            public EntitySet<Theme> Themes { get; set; }

            [ScaffoldColumn(false)]
            public Curriculum Curriculum { get; set; }
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
            public int ThemeTypeRef { get; set; }

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
    public partial class User : IEquatable<User>
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public Guid Id { get; set; }

            [DisplayName("Username")]
            [Required(ErrorMessage = "Username is required")]
            [StringLength(100, ErrorMessage = "Username can not be longer than 100")]
            [Order(1)]
            public string Username { get; set; }

            [DisplayName("Password")]
            [Required(ErrorMessage = "Password is required")]
            [StringLength(50, ErrorMessage = "Password can not be longer than 50")]
            [Order(2)]
            public string Password { get; set; }

            [DisplayName("Email")]
            [Required(ErrorMessage = "Email is required")]
            [StringLength(100, ErrorMessage = "Email can not be longer than 100")]
            [EmailAddress(ErrorMessage = "Not a valid email")]
            [Order(3)]
            public string Email { get; set; }

            [DropDownList(OptionLabel = "Select Role", SourceProperty = "RolesList")]
            [DisplayName("Role")]
            [Order(6)]
            public int RoleId { get; set; }

            [DisplayName("OpenId")]
            [StringLength(200, ErrorMessage = "OpenId can not be longer than 200")]
            [Order(4)]
            public string OpenId { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            [StringLength(200, ErrorMessage = "Name can not be longer than 200")]
            [Order(5)]
            public string Name { get; set; }

            [ScaffoldColumn(false)]
            public bool Deleted { get; set; }

            [ScaffoldColumn(false)]
            public bool IsApproved { get; set; }

            [ScaffoldColumn(false)]
            public DateTime CreationDate { get; set; }

            [ScaffoldColumn(false)]
            public Guid? ApprovedBy { get; set; }
        }

        public IEnumerable<SelectListItem> RolesList { get; set; }

        [ScaffoldColumn(false)]
        public string GroupsLine
        {
            get
            {
                if (GroupUsers.Count > 1)
                    return GroupUsers.Select(g => g.Group.Name).Aggregate((prev, next) => prev + ", " + next);
                else if (GroupUsers.Count == 1)
                    return GroupUsers.Select(g => g.Group.Name).First();
                else
                    return string.Empty;
            }
        }

        [ScaffoldColumn(false)]
        public Role Role
        {
            get
            {
                return (Role)RoleId;
            }
            set
            {
                RoleId = (int)value;
            }
        }

        public bool Equals(User other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public enum Role
    {
        None = 0,
        Student = 1,
        Teacher = 2,
        Admin = 3
    }

    [MetadataType(typeof(Metadata))]
    [Bind(Exclude = "Id")]
    public partial class Group
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [ScaffoldColumn(false)]
            public EntitySet<GroupUser> GroupUsers { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50")]
            public string Name { get; set; }

            [ScaffoldColumn(false)]
            public bool Deleted { get; set; }
        }
    }
    
    [MetadataType(typeof(Metadata))]
    public partial class GroupUser
    {
        public IEnumerable<SelectListItem> UserList { get; set; }

        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int GroupRef { get; set; }

            [DropDownList(OptionLabel = "Select User", SourceProperty = "UserList")]
            [DisplayName("User")]
            public int UserRef { get; set; }
        }
    }

    [MetadataType(typeof(Metadata))]
    [Bind(Exclude = "Id")]
    public partial class Timeline
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [DisplayName("Start Date")]
            [Required(ErrorMessage = "Start Date is required")]
            [UIHint("DateTimeWithPicker")]
            public DateTime StartDate { get; set; }

            [DisplayName("End Date")]
            [Required(ErrorMessage = "End Date is required")]
            [UIHint("DateTimeWithPicker")]
            public DateTime EndDate { get; set; }

            [ScaffoldColumn(false)]
            public int CurriculumAssignmentRef { get; set; }

            [ScaffoldColumn(false)]
            public int? StageRef { get; set; }

            [ScaffoldColumn(false)]
            public bool IsDeleted { get; set; }

            [ScaffoldColumn(false)]
            public CurriculumAssignment CurriculumAssignment { get; set; }
        }
    }

    [MetadataType(typeof(Metadata))]
    [Bind(Exclude = "Id")]
    public partial class ThemeAssignment
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [ScaffoldColumn(false)]
            public int ThemeRef { get; set; }

            [ScaffoldColumn(false)]
            public int CurriculumAssignmentRef { get; set; }

            [DisplayName("Max Score")]
            [Required(ErrorMessage = "Max score is required")]
            public int MaxScore { get; set; }

            [ScaffoldColumn(false)]
            public CurriculumAssignment CurriculumAssignment { get; set; }

            [ScaffoldColumn(false)]
            public Theme Theme { get; set; }

            [ScaffoldColumn(false)]
            public bool IsDeleted { get; set; }
        }
    }
}