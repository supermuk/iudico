﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using IUDICO.Common.Models.Attributes;
using System.Data.Linq;
using System.Linq;

namespace IUDICO.Common.Models.Shared
{
    [MetadataType(typeof(Metadata))]
    [Bind(Exclude = "Id")]
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

            [LocalizedDisplayName("UpdatedBy")]
            [ScaffoldColumn(false)]
            public string UpdatedBy { get; set; }

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
    public partial class Discipline
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [LocalizedDisplayName("Name")]
            [Required(ErrorMessage = "*")]
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
            public EntitySet<Curriculum> Curriculums { get; set; }

            [ScaffoldColumn(false)]
            public EntitySet<Chapter> Chapters { get; set; }

            [ScaffoldColumn(false)]
            public bool IsValid { get; set; }
        }
    }

    [MetadataType(typeof(Metadata))]
    [Bind(Exclude = "Id, DisciplineRef")]
    public partial class Chapter
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [LocalizedDisplayName("Name")]
            [Required(ErrorMessage = "*")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50.")]
            public string Name { get; set; }

            [LocalizedDisplayName("CreateDate")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [LocalizedDisplayName("UpdateDate")]
            [ScaffoldColumn(false)]
            public DateTime Updated { get; set; }

            [ScaffoldColumn(false)]
            public int DisciplineRef { get; set; }

            [ScaffoldColumn(false)]
            public bool IsDeleted { get; set; }

            [ScaffoldColumn(false)]
            public EntitySet<Topic> Topics { get; set; }

            [ScaffoldColumn(false)]
            public EntitySet<CurriculumChapter> CurriculumChapters { get; set; }

            [ScaffoldColumn(false)]
            public Discipline Discipline { get; set; }
        }
    }

    [MetadataType(typeof(Metadata))]
    [Bind(Exclude = "Id, ChapterRef")]
    public partial class Topic
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [LocalizedDisplayName("Name")]
            [Required(ErrorMessage = "*")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50.")]
            public string Name { get; set; }

            [LocalizedDisplayName("CreateDate")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [LocalizedDisplayName("UpdateDate")]
            [ScaffoldColumn(false)]
            public DateTime Updated { get; set; }

            [ScaffoldColumn(false)]
            public int ChapterRef { get; set; }

            [ScaffoldColumn(false)]
            public int TestCourseRef { get; set; }

            [ScaffoldColumn(false)]
            public int TestTopicTypeRef { get; set; }

            [ScaffoldColumn(false)]
            public int TheoryCourseRef { get; set; }

            [ScaffoldColumn(false)]
            public int TheoryTopicTypeRef { get; set; }

            [ScaffoldColumn(false)]
            public int SortOrder { get; set; }

            [ScaffoldColumn(false)]
            public bool IsDeleted { get; set; }

            [ScaffoldColumn(false)]
            public bool BlockTopicAtTesting { get; set; }

            [ScaffoldColumn(false)]
            public bool BlockCurriculumAtTesting { get; set; }
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
    public partial class UserRole
    {
        private sealed class Metadata
        {
            [LocalizedDropDownList("SelectUser", SourceProperty = "UsersList")]
            public Guid UserId { get; set; }

            [LocalizedDropDownList("SelectRole", SourceProperty = "RolesList")]
            public int RoleId { get; set; }

            public IEnumerable<SelectListItem> RolesList { get; set; }

            public IEnumerable<SelectListItem> UsersList { get; set; }
        }
    }

    [MetadataType(typeof(Metadata))]
    [Bind(Exclude = "Id")]
    public partial class User
    {
        public IEnumerable<Role> Roles
        {
            get { return this.UserRoles.Select(u => (Role)u.RoleRef); }
        }

        public string GroupsLine
        {
            get
            {
                if (this.GroupUsers.Count > 0)
                    return this.GroupUsers.Select(g => g.Group.Name).Aggregate((a, b) => a + ", " + b);

                return string.Empty;
            }
        }

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
            [EmailAddress(ErrorMessage = "NotValidEmail")]
            [Order(3)]
            public string Email { get; set; }

            /*[LocalizedDropDownList("SelectRole", SourceProperty = "RolesList")]
            [LocalizedDisplayName("Role")]
            [Order(6)]
            public int RoleId { get; set; }*/

            [DisplayName("OpenId")]
            [StringLength(200, ErrorMessage = "OpenId can not be longer than 200")]
            [Order(4)]
            public string OpenId { get; set; }

            [LocalizedDisplayName("FullName")]
            [LocalizedRequired(ErrorMessage = "FullNameRequired")]
            [StringLength(200, ErrorMessage = "FullName can not be longer than 200")]
            [Order(5)]
            public string Name { get; set; }

            [LocalizedDisplayName("UserID")]
            [LocalizedRequired(ErrorMessage = "UserIDRequired")]
            [StringLength(100, ErrorMessage = "ID can not be longer than 100")]
            [Order(7)]
            public string UserId { get; set; }

            [ScaffoldColumn(false)]
            public bool Deleted { get; set; }

            [ScaffoldColumn(false)]
            public bool IsApproved { get; set; }

            [ScaffoldColumn(false)]
            public DateTime CreationDate { get; set; }

            [ScaffoldColumn(false)]
            public Guid? ApprovedBy { get; set; }

            [ScaffoldColumn(false)]
            public string GroupsLine { get; set; }

            [ScaffoldColumn(false)]
            public int TestsSum { get; set; }

            [ScaffoldColumn(false)]
            public int TestsTotal { get; set; }
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
    public partial class ForecastingTree
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [LocalizedDisplayName("Name")]
            [LocalizedRequired(ErrorMessage = "NameRequired")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50.")]
            public string Name { get; set; }

            [LocalizedDisplayName("UserRef")]
            [ScaffoldColumn(false)]
            public Guid UserRef { get; set; }

            [LocalizedDisplayName("CreateDate")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [LocalizedDisplayName("UpdateDate")]
            [ScaffoldColumn(false)]
            public DateTime Updated { get; set; }

            [ScaffoldColumn(false)]
            public bool IsDeleted { get; set; }
        }
    }

    [MetadataType(typeof(Metadata))]
    [Bind(Exclude = "Id")]
    public partial class Tag
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [ScaffoldColumn(false)]
            public EntitySet<TopicTag> TopicTags { get; set; }

            [LocalizedDisplayName("Name")]
            [LocalizedRequired(ErrorMessage = "NameRequired")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50")]
            public string Name { get; set; }
        }
    }

    public class LocalizedStringLengthAttribute : StringLengthAttribute
    {
        public LocalizedStringLengthAttribute(int length)
            : base(length)
        {
           
        }
        public override string FormatErrorMessage(string name)
        {
            return Localization.GetMessage(this.ErrorMessage);
        }
    }
    public class LocalizedRegularExpressionAttribute : RegularExpressionAttribute
    {
        public LocalizedRegularExpressionAttribute(string pattern) : base(pattern)
        {
            ErrorMessage = Localization.GetMessage(ErrorMessage);
        }
        public override string FormatErrorMessage(string name)
        {
            return Localization.GetMessage(this.ErrorMessage);
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
                return Localization.GetMessage(base.DisplayName);
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
            return Localization.GetMessage(this.ErrorMessage);
        }
    }
    public class LocalizedDropDownListAttribute : DropDownListAttribute
    {

        public LocalizedDropDownListAttribute(string dropDownListKey)
            : base()
        {
            OptionLabel = Localization.GetMessage(dropDownListKey);
        }
    }
}