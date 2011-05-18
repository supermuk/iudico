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
using System.Threading;


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

        [LocalizedDisplayName("Role")]
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
            OpenId = user.OpenId;
            Email = user.Email;
            Name = user.Name;
        }

        public EditModel()
        {
        }

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

    public class EditUserModel
    {
        public EditUserModel(User user)
        {
            Username = user.Username;
            Name = user.Name;
            Email = user.Email;
            RoleId = user.RoleId;
            OpenId = user.OpenId;
            RolesList = user.RolesList;
        }

        public EditUserModel()
        {
        }

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

        [LocalizedDisplayName("Role")]
        [LocalizedDropDownList("SelectRole", SourceProperty = "RolesList")]
        [LocalizedRequired(ErrorMessage = "RoleRequired")]
        public int RoleId { get; set; }

        [DisplayName("OpenId")]
        [StringLength(200, ErrorMessage = "OpenId can not be longer than 200")]
        public string OpenId { get; set; }

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

        [ScaffoldColumn(false)]
        public string Username { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }

    public class UserGroupModel
    {
        public IEnumerable<SelectListItem> GroupList { get; set; }

        [LocalizedDropDownList("SelectGroup", SourceProperty = "GroupList")]
        [LocalizedDisplayName("Group")]
        public int GroupRef { get; set; }
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
        private PropertyInfo _nameProperty;
        private string _resource;

        private static System.Resources.ResourceManager ManagerEN;
        private static System.Resources.ResourceManager ManagerUK;

        public LocalizedRequiredAttribute()
            : base()
        {
            
            ManagerEN = new System.Resources.ResourceManager("IUDICO.UserManagement.Resource", Assembly.GetExecutingAssembly());
            ManagerUK = new System.Resources.ResourceManager("IUDICO.UserManagement.Resourceuk", Assembly.GetExecutingAssembly());
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
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
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
    public class LocalizedDropDownListAttribute : DropDownListAttribute
    {

        private static System.Resources.ResourceManager ManagerEN;
        private static System.Resources.ResourceManager ManagerUK;

        public LocalizedDropDownListAttribute(string DropDownListKey)
            : base()
        {

            ManagerEN = new System.Resources.ResourceManager("IUDICO.UserManagement.Resource", Assembly.GetExecutingAssembly());
            ManagerUK = new System.Resources.ResourceManager("IUDICO.UserManagement.Resourceuk", Assembly.GetExecutingAssembly());
            if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
            {
                OptionLabel = ManagerEN.GetString(DropDownListKey, Thread.CurrentThread.CurrentUICulture);
            }
            else
            {
                OptionLabel = ManagerUK.GetString(DropDownListKey, Thread.CurrentThread.CurrentUICulture);
            }
        }
    }
}