using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public string OpenID { get; set; }

        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class EditModel
    {
        public EditModel(OpenIDMembershipUser user)
        {
            OpenID = user.OpenID;
            Email = user.Email;
        }

        [DisplayName("Open ID")]
        public string OpenID { get; set; }

        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}