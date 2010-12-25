using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IUDICO.Common.Models;

namespace IUDICO.UserManagement.Models
{
    public class RegisterModel
    {
        [Required]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Open ID")]
        public string OpenId { get; set; }

        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class EditModel
    {
        public EditModel(OpenIdMembershipUser user)
        {
            OpenId = user.OpenId;
            Email = user.Email;
        }

        [DisplayName("Open ID")]
        public string OpenId { get; set; }

        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class EditUserModel
    {
        public EditUserModel(User user)
        {
            Name = user.Name;
            Password = user.Password;
            Email = user.Email;
            RoleRef = user.RoleRef;
            OpenId = user.OpenId;
        }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(200, ErrorMessage = "Name can not be longer than 200")]
        public string Name { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "Password can not be longer than 50")]
        public string Password { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, ErrorMessage = "Email can not be longer than 100")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Role")]
        [Required(ErrorMessage = "Role is required")]
        public int RoleRef { get; set; }

        [DisplayName("OpenId")]
        [Required(ErrorMessage = "OpenId is required")]
        [StringLength(200, ErrorMessage = "OpenId can not be longer than 200")]
        public string OpenId { get; set; }
    }
}