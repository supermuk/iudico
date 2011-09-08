using System;
using IUDICO.Common.Models.Attributes;
namespace IUDICO.Common.Models
{
    public class ActionReal : IAction
    {
        protected Action _action;
        public string Name { get; protected set; }
        public string Link { get; protected set; }

        public ActionReal(Action action)
        {
            _action = action;
        }

        public Role GetRole()
        {
            var attribute = Attribute.GetCustomAttribute(_action.Method, typeof(AllowAttribute), false) as AllowAttribute;

            return attribute.Role;
        }
    }
}

