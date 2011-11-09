using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using IUDICO.Common.Models.Attributes;
using System.Data.Linq;
using System.Linq;
using System.Globalization;
using System.Threading;
using System.Reflection;

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

            [LocalizedDisplayName("Name")]
            [LocalizedRequired(ErrorMessage = "NameRequired")]
            public string Name { get; set; }

            [LocalizedDisplayName("Owner")]
            [ScaffoldColumn(false)]
            public string Owner { get; set; }

            [LocalizedDisplayName("CreatedDate")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [LocalizedDisplayName("LastUpdated")]
            [ScaffoldColumn(false)]
            public DateTime Updated { get; set; }

            [ScaffoldColumn(false)]
            public bool Deleted { get; set; }

            [ScaffoldColumn(false)]
            public bool Locked { get; set; }

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

            [LocalizedDisplayName("Name")]
            [LocalizedRequired(ErrorMessage = "NameRequired")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50.")]
            public string Name { get; set; }

            [LocalizedDisplayName("CreateDate")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [LocalizedDisplayName("UpdateDate")]
            [ScaffoldColumn(false)]
            public DateTime Updated { get; set; }

            [ScaffoldColumn(false)]
            public string Owner { get; set; }

            [ScaffoldColumn(false)]
            public bool IsDeleted { get; set; }

            [ScaffoldColumn(false)]
            public EntitySet<CurriculumAssignment> CurriculumAssignments { get; set; }

            [ScaffoldColumn(false)]
            public EntitySet<Stage> Stages { get; set; }

            [ScaffoldColumn(false)]
            public bool IsValid { get; set; }
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

            [LocalizedDisplayName("Name")]
            [LocalizedRequired(ErrorMessage = "NameRequired")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50.")]
            public string Name { get; set; }

            [LocalizedDisplayName("CreateDate")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [LocalizedDisplayName("UpdateDate")]
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

            [LocalizedDisplayName("Name")]
            [LocalizedRequired(ErrorMessage = "NameRequired")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50.")]
            public string Name { get; set; }

            [LocalizedDisplayName("CreateDate")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [LocalizedDisplayName("UpdateDate")]
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

            [LocalizedDisplayName("Name")]
            [LocalizedRequired(ErrorMessage = "NameRequired")]
            public string Name { get; set; }

            [ScaffoldColumn(false)]
            public int? ParentId { get; set; }

            [ScaffoldColumn(false)]
            public int CourseId { get; set; }

            [ScaffoldColumn(false)]
            [LocalizedDisplayName("IsFolder")]
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

            [LocalizedDisplayName("Login")]
            [LocalizedRequired(ErrorMessage = "LoginRequired")]
            [StringLength(100, ErrorMessage = "Login can not be longer than 100")]
            [Order(1)]
            public string Username { get; set; }

            [LocalizedDisplayName("Password")]
            [LocalizedRequired(ErrorMessage = "PasswordRequired")]
            [StringLength(50, ErrorMessage = "Password can not be longer than 50")]
            [DataType(DataType.Password)]
            [Order(2)]
            public string Password { get; set; }

            [LocalizedDisplayName("Email")]
            [LocalizedRequired(ErrorMessage = "EmailRequired")]
            [StringLength(100, ErrorMessage = "Email can not be longer than 100")]
            [EmailAddress(ErrorMessage = "Not a valid email")]
            [Order(3)]
            public string Email { get; set; }

            [LocalizedDropDownList("SelectRole", SourceProperty = "RolesList")]
            [LocalizedDisplayName("Role")]
            [Order(6)]
            public int RoleId { get; set; }

            [DisplayName("OpenId")]
            [StringLength(200, ErrorMessage = "OpenId can not be longer than 200")]
            [Order(4)]
            public string OpenId { get; set; }

            [LocalizedDisplayName("FullName")]
            [LocalizedRequired(ErrorMessage = "FullNameRequired")]
            [StringLength(200, ErrorMessage = "FullName can not be longer than 200")]
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

            [LocalizedDisplayName("Name")]
            [LocalizedRequired(ErrorMessage = "NameRequired")]
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

            [LocalizedDropDownList("SelectUser", SourceProperty = "UserList")]
            [LocalizedDisplayName("User")]
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

            [LocalizedDisplayName("StartDate")]
            [LocalizedRequired(ErrorMessage = "StartDateRequired")]
            [UIHint("DateTimeWithPicker")]
            [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime StartDate { get; set; }

            [LocalizedDisplayName("EndDate")]
            [LocalizedRequired(ErrorMessage = "EndDateRequired")]
            [UIHint("DateTimeWithPicker")]
            [DisplayFormat(DataFormatString = /*"dd.MM.yy HH:mm:ss"*/"{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
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

            [LocalizedDisplayName("MaxScore")]
            [LocalizedRequired(ErrorMessage = "MaxScoreRequired")]
            public int MaxScore { get; set; }

            [ScaffoldColumn(false)]
            public CurriculumAssignment CurriculumAssignment { get; set; }

            [ScaffoldColumn(false)]
            public Theme Theme { get; set; }

            [ScaffoldColumn(false)]
            public bool IsDeleted { get; set; }
        }
    }
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {

        public LocalizedDisplayNameAttribute(string displayNameKey)
            : base(displayNameKey)
        {
        }

        public override string DisplayName
        {
            get
            {
                return Localization.getMessage(base.DisplayName);
            }
        }
    }
    public class LocalizedRequiredAttribute : RequiredAttribute
    {

        public LocalizedRequiredAttribute()
            : base()
        {

        }

        public override string FormatErrorMessage(string name)
        {
            return Localization.getMessage(base.ErrorMessage);
        }
    }
    public class LocalizedDropDownListAttribute : DropDownListAttribute
    {

        public LocalizedDropDownListAttribute(string DropDownListKey)
            : base()
        {
            OptionLabel = Localization.getMessage(DropDownListKey);
        }
    }
}