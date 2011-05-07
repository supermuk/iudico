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

            [LocalizedDisplayName("Name", NameResourceType = "IUDICO.Common.Resource")]
            [LocalizedRequired(ErrorMessage = "NameRequired")]
            public string Name { get; set; }

            [LocalizedDisplayName("Owner", NameResourceType = "IUDICO.Common.Resource")]
            [ScaffoldColumn(false)]
            public string Owner { get; set; }

            [LocalizedDisplayName("CreatedDate", NameResourceType = "IUDICO.Common.Resource")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [LocalizedDisplayName("LastUpdated", NameResourceType = "IUDICO.Common.Resource")]
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

            [LocalizedDisplayName("Name", NameResourceType = "IUDICO.Common.Resource")]
            [LocalizedRequired(ErrorMessage = "NameRequired")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50.")]
            public string Name { get; set; }

            [LocalizedDisplayName("CreateDate", NameResourceType = "IUDICO.Common.Resource")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [LocalizedDisplayName("UpdateDate", NameResourceType = "IUDICO.Common.Resource")]
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

            [LocalizedDisplayName("Name", NameResourceType = "IUDICO.Common.Resource")]
            [LocalizedRequired(ErrorMessage = "NameRequired")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50.")]
            public string Name { get; set; }

            [LocalizedDisplayName("CreateDate", NameResourceType = "IUDICO.Common.Resource")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [LocalizedDisplayName("UpdateDate", NameResourceType = "IUDICO.Common.Resource")]
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

            [LocalizedDisplayName("Name", NameResourceType = "IUDICO.Common.Resource")]
            [LocalizedRequired(ErrorMessage = "NameRequired")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50.")]
            public string Name { get; set; }

            [LocalizedDisplayName("CreateDate", NameResourceType = "IUDICO.Common.Resource")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [LocalizedDisplayName("UpdateDate", NameResourceType = "IUDICO.Common.Resource")]
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

            [LocalizedDisplayName("Name", NameResourceType = "IUDICO.Common.Resource")]
            [LocalizedRequired(ErrorMessage = "NameRequired")]
            public string Name { get; set; }

            [ScaffoldColumn(false)]
            public int? ParentId { get; set; }

            [ScaffoldColumn(false)]
            public int CourseId { get; set; }

            [ScaffoldColumn(false)]
            [LocalizedDisplayName("IsFolder", NameResourceType = "IUDICO.Common.Resource")]
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

            [LocalizedDisplayName("Username", NameResourceType = "IUDICO.Common.Resource")]
            [LocalizedRequired(ErrorMessage = "UsernameRequired")]
            [StringLength(100, ErrorMessage = "Username can not be longer than 100")]
            [Order(1)]
            public string Username { get; set; }

            [LocalizedDisplayName("Password", NameResourceType = "IUDICO.Common.Resource")]
            [LocalizedRequired(ErrorMessage = "PasswordRequired")]
            [StringLength(50, ErrorMessage = "Password can not be longer than 50")]
            [DataType(DataType.Password)]
            [Order(2)]
            public string Password { get; set; }

            [LocalizedDisplayName("Email", NameResourceType = "IUDICO.Common.Resource")]
            [LocalizedRequired(ErrorMessage = "EmailRequired")]
            [StringLength(100, ErrorMessage = "Email can not be longer than 100")]
            [EmailAddress(ErrorMessage = "Not a valid email")]
            [Order(3)]
            public string Email { get; set; }

            [DropDownList(OptionLabel = "Select Role", SourceProperty = "RolesList")]
            [LocalizedDisplayName("Role", NameResourceType = "IUDICO.Common.Resource")]
            [Order(6)]
            public int RoleId { get; set; }

            [DisplayName("OpenId")]
            [StringLength(200, ErrorMessage = "OpenId can not be longer than 200")]
            [Order(4)]
            public string OpenId { get; set; }

            [LocalizedDisplayName("Name", NameResourceType = "IUDICO.Common.Resource")]
            [LocalizedRequired(ErrorMessage = "NameRequired")]
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

            [LocalizedDisplayName("Name", NameResourceType = "IUDICO.Common.Resource")]
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

            [DropDownList(OptionLabel = "Select User", SourceProperty = "UserList")]
            [LocalizedDisplayName("User", NameResourceType = "IUDICO.Common.Resource")]
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

            [LocalizedDisplayName("StartDate", NameResourceType = "IUDICO.Common.Resource")]
            [LocalizedRequired(ErrorMessage = "StartDateRequired")]
            [UIHint("DateTimeWithPicker")]
            [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime StartDate { get; set; }

            [LocalizedDisplayName("EndDate", NameResourceType = "IUDICO.Common.Resource")]
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

            [LocalizedDisplayName("MaxScore", NameResourceType = "IUDICO.Common.Resource")]
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
        private PropertyInfo _nameProperty;
        private string _resource;

        private static System.Resources.ResourceManager ManagerEN;
        private static System.Resources.ResourceManager ManagerUK;

        public LocalizedDisplayNameAttribute(string displayNameKey)
            : base(displayNameKey)
        {
            ManagerEN = new System.Resources.ResourceManager("IUDICO.Common.Resource", Assembly.GetExecutingAssembly());
            ManagerUK = new System.Resources.ResourceManager("IUDICO.Common.Resourceuk", Assembly.GetExecutingAssembly());
        }

        public string NameResourceType
        {
            get
            {
                return _resource;
            }
            set
            {
                _resource = value;

                //_nameProperty = _resourceType.GetProperty(base.DisplayName, BindingFlags.Static | BindingFlags.Public);
                /*if (Thread.CurrentThread.CurrentUICulture.Name == "en")
                {
                    _nameProperty = _resourceType.GetProperty("Resource", BindingFlags.Static | BindingFlags.Public);
                }
                else
                {
                    _nameProperty = _resourceType.GetProperty("Resourceuk", BindingFlags.Static | BindingFlags.Public);
                }*/
            }
        }

        public override string DisplayName
        {
            get
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "en")
                {
                    return ManagerEN.GetString(base.DisplayName, Thread.CurrentThread.CurrentUICulture);
                }
                else
                {
                    return ManagerUK.GetString(base.DisplayName, Thread.CurrentThread.CurrentUICulture);
                }
                if (_nameProperty == null)
                {
                    return base.DisplayName;
                }

                //return (string)_nameProperty.GetValue(_nameProperty.DeclaringType, null);
            }
        }
    }
    public class LocalizedRequiredAttribute : RequiredAttribute
    {
        private PropertyInfo _nameProperty;
        private string _resource;

        private static System.Resources.ResourceManager ManagerEN;
        private static System.Resources.ResourceManager ManagerUK;

        public LocalizedRequiredAttribute()
            : base()
        {

            ManagerEN = new System.Resources.ResourceManager("IUDICO.Common.Resource", Assembly.GetExecutingAssembly());
            ManagerUK = new System.Resources.ResourceManager("IUDICO.Common.Resourceuk", Assembly.GetExecutingAssembly());
            //base.ErrorMessage = ManagerUK.GetString(base.ErrorMessage, Thread.CurrentThread.CurrentUICulture);
        }

        public string NameResourceType
        {
            get
            {
                return _resource;
            }
            set
            {
                _resource = value;

                //_nameProperty = _resourceType.GetProperty(base.DisplayName, BindingFlags.Static | BindingFlags.Public);
                /*if (Thread.CurrentThread.CurrentUICulture.Name == "en")
                {
                    _nameProperty = _resourceType.GetProperty("Resource", BindingFlags.Static | BindingFlags.Public);
                }
                else
                {
                    _nameProperty = _resourceType.GetProperty("Resourceuk", BindingFlags.Static | BindingFlags.Public);
                }*/
            }
        }

        public override string FormatErrorMessage(string name)
        {
            //get
            //{
            if (Thread.CurrentThread.CurrentUICulture.Name == "en")
            {
                return ManagerEN.GetString(base.ErrorMessage, Thread.CurrentThread.CurrentUICulture);
            }
            else
            {
                return ManagerUK.GetString(base.ErrorMessage, Thread.CurrentThread.CurrentUICulture);
            }
            if (_nameProperty == null)
            {
                return base.ErrorMessage;
            }

            //return (string)_nameProperty.GetValue(_nameProperty.DeclaringType, null);
            //}
        }
    }
}