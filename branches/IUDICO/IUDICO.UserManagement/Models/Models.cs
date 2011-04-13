using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IUDICO.Common.Models;
using System.Collections.Generic;
using IUDICO.Common.Models.Attributes;
using OpenIdMembershipUser = IUDICO.UserManagement.Models.Auth.OpenIdMembershipUser;
using Guid = System.Guid;
using System.Web.Mvc;
using System;
using System.Globalization;
using System.Reflection;
using UsManagRes;

namespace IUDICO.UserManagement.Models
{
    public class DetailsModel
    {
        public DetailsModel(User user, IEnumerable<Group> groups)
        {
            Username = user.Username;
            Name = user.Name;
            OpenId = user.OpenId;
            Email = user.Email;
            Groups = groups;
            Role = user.Role;
        }

        
        [LocalizedDisplayName("Username", NameResourceType = typeof(UserManagem))]
        [Order(1)]
        public string Username { get; set; }

        [LocalizedDisplayName("Name", NameResourceType = typeof(UserManagem))]
        [Order(2)]
        public string Name { get; set; }

        [DisplayName("Open ID")]
        [Order(3)]
        public string OpenId { get; set; }

        [LocalizedDisplayName("Email", NameResourceType = typeof(UserManagem))]
        [EmailAddress]
        [Order(4)]
        public string Email { get; set; }

        [LocalizedDisplayName("Role", NameResourceType = typeof(UserManagem))]
        [Order(5)]
        public Role Role { get; set; }

        [ScaffoldColumn(false)]
        public IEnumerable<Group> Groups { get; set; }
    }

    public class AdminDetailsModel : DetailsModel
    {
        public AdminDetailsModel(User user, IEnumerable<Group> groups)
            : base(user, groups)
        {
            Id = user.Id;
            IsApproved = user.IsApproved;
        }

        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [LocalizedDisplayName("Activated", NameResourceType = typeof(UserManagem))]
        [DataType(DataType.Text)]
        [Order(6)]
        public bool IsApproved { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required")]
        [LocalizedDisplayName("Username", NameResourceType = typeof(UserManagem))]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("Password", NameResourceType = typeof(UserManagem))]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("ConfirmPassword", NameResourceType = typeof(UserManagem))]
        public string ConfirmPassword { get; set; }

        [DisplayName("Open ID")]
        public string OpenId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [LocalizedDisplayName("Email", NameResourceType = typeof(UserManagem))]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [LocalizedDisplayName("Name", NameResourceType = typeof(UserManagem))]
        public string Name { get; set; }
    }

    public class EditModel
    {
        public EditModel(User user)
        {
            OpenId = user.OpenId;
            Email = user.Email;
            Name = user.Name;
        }

        public EditModel()
        {
        }

        [Required(ErrorMessage = "Name is required")]
        [LocalizedDisplayName("Name", NameResourceType = typeof(UserManagem))]
        public string Name { get; set; }

        [DisplayName("Open ID")]
        public string OpenId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [LocalizedDisplayName("Email", NameResourceType = typeof(UserManagem))]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Old Password is required")]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("OldPassword", NameResourceType = typeof(UserManagem))]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("NewPassword", NameResourceType = typeof(UserManagem))]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("ConfirmPassword", NameResourceType = typeof(UserManagem))]
        public string ConfirmPassword { get; set; }
    }

    public class EditUserModel
    {
        public EditUserModel(User user)
        {
            Name = user.Name;
            Password = user.Password;
            Email = user.Email;
            RoleId = user.RoleId;
            OpenId = user.OpenId;
        }

        public EditUserModel()
        {
        }

        [LocalizedDisplayName("Name", NameResourceType = typeof(UserManagem))]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(200, ErrorMessage = "Name can not be longer than 200")]
        public string Name { get; set; }

        [LocalizedDisplayName("Password", NameResourceType = typeof(UserManagem))]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "Password can not be longer than 50")]
        public string Password { get; set; }

        [LocalizedDisplayName("Email", NameResourceType = typeof(UserManagem))]
        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, ErrorMessage = "Email can not be longer than 100")]
        [EmailAddress]
        public string Email { get; set; }

        [LocalizedDisplayName("Role", NameResourceType = typeof(UserManagem))]
        [Required(ErrorMessage = "Role is required")]
        public int RoleId { get; set; }

        [DisplayName("OpenId")]
        [Required(ErrorMessage = "OpenId is required")]
        [StringLength(200, ErrorMessage = "OpenId can not be longer than 200")]
        public string OpenId { get; set; }
    }

    public class UserGroupModel
    {
        public IEnumerable<SelectListItem> GroupList { get; set; }

        [DropDownList(OptionLabel = "Select Group", SourceProperty = "GroupList")]
        [LocalizedDisplayName("Group", NameResourceType = typeof(UserManagem))]
        public int GroupRef { get; set; }
    }
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private PropertyInfo _nameProperty;
        private Type _resourceType;

        public LocalizedDisplayNameAttribute(string displayNameKey)
            : base(displayNameKey)
        {

        }

        public Type NameResourceType
        {
            get
            {
                return _resourceType;
            }
            set
            {
                _resourceType = value;

                _nameProperty = _resourceType.GetProperty(base.DisplayName, BindingFlags.Static | BindingFlags.Public);
            }
        }

        public override string DisplayName
        {
            get
            {

                if (_nameProperty == null)
                {
                    return base.DisplayName;
                }

                return (string)_nameProperty.GetValue(_nameProperty.DeclaringType, null);
            }
        }
    }
}