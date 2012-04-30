using System;

namespace IUDICO.Common.Models.Shared
{
    public class ShareUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Shared { get; set; }
        public string[] Roles { get; set; }
    }
}