using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace IUDICO.UM.Models
{
    [MetadataType(typeof(User.Metadata))]
    [Bind(Exclude = "ID")]
    public partial class User
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public Guid ID { get; set; }

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
            public string OpenID { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            [StringLength(200, ErrorMessage = "Name can not be longer than 200")]
            public string Name { get; set; }

            [ScaffoldColumn(false)]
            public int RoleID { get; set; }
        }
    }

    [MetadataType(typeof(Role.Metadata))]
    [Bind(Exclude = "ID")]
    public partial class Role
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int ID { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            [StringLength(50, ErrorMessage = "Name can not be longer than 50")]
            public string Name { get; set; }
        }
    }
}