using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Security.Models
{
    public interface ISecurityDataContext : IMockableDataContext
    {
        IMockableTable<Room> Rooms { get; }
        IMockableTable<Computer> Computers { get; }
    }
}