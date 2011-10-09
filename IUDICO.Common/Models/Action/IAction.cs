using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Action
{
    public interface IAction
    {
        string Name { get; }
        string Link { get; }
        Role GetRole();
    }
}
