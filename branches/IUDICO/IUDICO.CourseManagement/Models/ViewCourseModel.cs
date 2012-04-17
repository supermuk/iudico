using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.CourseManagement.Models
{
    public class ViewCourseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Locked { get; set; }
        public bool Shared { get; set; }
    }
}