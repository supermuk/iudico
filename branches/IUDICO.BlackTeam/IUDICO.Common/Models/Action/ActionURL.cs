using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Action
{
    public class ActionURL : IAction
    {
        public string Link { get; protected set; }
        public string Name { get; protected set; }
        public Role Role { get; protected set; }

        public ActionURL(string name, string link, Role role)
        {
            Name = name;
            Link = link;
            Role = role;
        }

        public Role GetRole()
        {
            return Role;
        }
    }
}
