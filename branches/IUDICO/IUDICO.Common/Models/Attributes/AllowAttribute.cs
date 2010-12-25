using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace IUDICO.Common.Models.Attributes
{
    class AllowAttribute : AuthorizeAttribute
    {
        protected new string Roles { get; set; }

        public User.UserRole Role
        {
            get
            {
                User.UserRole role;

                Enum.TryParse(Roles, true, out role);

                return role;
            }

            set { Roles = value.ToString(); }
        }
    }
}
