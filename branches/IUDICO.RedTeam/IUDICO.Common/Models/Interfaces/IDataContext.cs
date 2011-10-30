﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Interfaces
{
    public interface IDataContext: IMockableDataContext
    {
        IMockableTable<User> Users { get; }
        IMockableTable<Course> Courses { get; }
        IMockableTable<CourseUser> CourseUsers { get; }
        IMockableTable<Node> Nodes { get; }
        IMockableTable<NodeResource> NodeResources { get; }
    }
}
