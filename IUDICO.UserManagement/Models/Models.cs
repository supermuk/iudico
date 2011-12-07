using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IUDICO.Common.Models;
using System.Collections.Generic;
using IUDICO.Common.Models.Attributes;
using IUDICO.Common.Models.Shared;
using Guid = System.Guid;
using System.Web.Mvc;
using System.Linq;

namespace IUDICO.UserManagement.Models
{
    public class DetailsModel
    {
        public DetailsModel(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Name = user.Name;
            OpenId = user.OpenId;
            Email = user.Email;
            Roles = user.UserRoles.Select(r => (Role)r.RoleRef);
            Groups = user.GroupUsers.Select(g => g.Group);
            /*Groups = user.Groups;
            Roles = user.Roles;*/
            UserId = user.UserId;
        }

        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [LocalizedDisplayName("Loginn")]
        [Order(1)]
        public string Username { get; set; }

        [LocalizedDisplayName("FullName")]
        [Order(2)]
        public string Name { get; set; }

        [DisplayName("Open ID")]
        [Order(3)]
        public string OpenId { get; set; }

        [LocalizedDisplayName("Email")]
        [EmailAddress]
        [Order(4)]
        public string Email { get; set; }

        [LocalizedDisplayName("UserID")]
        [Order(5)]
        public string UserId { get; set; }

        [ScaffoldColumn(false)]
        public IEnumerable<Role> Roles { get; set; }

        [ScaffoldColumn(false)]
        public IEnumerable<Group> Groups { get; set; }
    }

    public class AdminDetailsModel : DetailsModel
    {
        public AdminDetailsModel(User user)
            : base(user)
        {
            //Id = user.Id;
            IsApproved = user.IsApproved;
        }

        //[ScaffoldColumn(false)]
        //public Guid Id { get; set; }

        [LocalizedDisplayName("Activated")]
        [DataType(DataType.Text)]
        [Order(6)]
        public bool IsApproved { get; set; }
    }

    public class RegisterModel
    {
        [LocalizedRequired(ErrorMessage = "LoginRequired")]
        [LocalizedDisplayName("Loginn")]
        public string Username { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        [LocalizedRequired(ErrorMessage = "PasswordRequired")]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("Password")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Confirm Password is required")]
        [LocalizedRequired(ErrorMessage = "ConfirmPasswordRequired")]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Open ID")]
        public string OpenId { get; set; }

        //[Required(ErrorMessage = "Email is required")]
        [LocalizedRequired(ErrorMessage = "EmailRequired")]
        [LocalizedDisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Name is required")]
        [LocalizedRequired(ErrorMessage = "FullNameRequiered")]
        [LocalizedDisplayName("FullName")]
        public string Name { get; set; }
    }

    public class EditModel
    {
        public EditModel(User user)
        {
            Id = user.Id;
            OpenId = user.OpenId;
            Email = user.Email;
            Name = user.Name;
            UserId = user.UserId;
        }

        public EditModel()
        {
        }

        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [LocalizedRequired(ErrorMessage = "FullNameRequiered")]
        [LocalizedDisplayName("FullName")]
        public string Name { get; set; }

        [DisplayName("Open ID")]
        [StringLength(200, ErrorMessage = "OpenId can not be longer than 200")]
        public string OpenId { get; set; }

        [LocalizedRequired(ErrorMessage = "EmailRequired")]
        [LocalizedDisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }

        [LocalizedRequired(ErrorMessage = "UserID")]
        [LocalizedDisplayName("UserID")]
        [StringLength(100, ErrorMessage = "ID can not be longer than 100")]
        public string UserId { get; set; }
    }

    public class ChangePasswordModel
    {
        //[Required(ErrorMessage = "Old Password is required")]
        [LocalizedRequired(ErrorMessage = "OldPasswordRequired")]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("OldPassword")]
        public string OldPassword { get; set; }

        //[Required(ErrorMessage = "New Password is required")]
        [LocalizedRequired(ErrorMessage = "NewPasswordRequired")]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("NewPassword" )]
        public string NewPassword { get; set; }

        [LocalizedRequired(ErrorMessage = "ConfirmPasswordRequired")]
        [DataType(DataType.Password)]
        [LocalizedDisplayName("ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }

    public class RestorePasswordModel
    {
        [LocalizedRequired(ErrorMessage = "EmailRequired")]
        [LocalizedDisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class EditUserModel
    {
        public EditUserModel(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Name = user.Name;
            Email = user.Email;
            OpenId = user.OpenId;
            UserId = user.UserId;
        }

        public EditUserModel()
        {
        }

        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [LocalizedDisplayName("FullName")]
        [LocalizedRequired(ErrorMessage = "FullNameRequiered")]
        [StringLength(200, ErrorMessage = "Name can not be longer than 200")]
        public string Name { get; set; }

        [LocalizedDisplayName("Password")]
        [StringLength(50, ErrorMessage = "Password can not be longer than 50")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [LocalizedDisplayName("Email")]
        [LocalizedRequired(ErrorMessage = "EmailRequired")]
        [StringLength(100, ErrorMessage = "Email can not be longer than 100")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("OpenId")]
        [StringLength(200, ErrorMessage = "OpenId can not be longer than 200")]
        public string OpenId { get; set; }

        [ScaffoldColumn(false)]
        public string Username { get; set; }
 
        [LocalizedRequired(ErrorMessage = "UserID")]
        [LocalizedDisplayName("UserID")]
        [StringLength(100, ErrorMessage = "ID can not be longer than 100")]
        public string UserId { get; set; }
    }

    public class UserGroupModel
    {
        public IEnumerable<SelectListItem> GroupList { get; set; }

        [LocalizedDropDownList("SelectGroup", SourceProperty = "GroupList")]
        [LocalizedDisplayName("Group")]
        public int GroupRef { get; set; }
    }

    public class UserRoleModel
    {
        public IEnumerable<SelectListItem> RoleList { get; set; }

        [LocalizedDropDownList("SelectRole", SourceProperty = "RoleList")]
        [LocalizedDisplayName("Role")]
        public int RoleRef { get; set; }
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