using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebEditor.Models
{
    [MetadataType(typeof(Course.Metadata))]
    [Bind(Exclude="Id")]
    public partial class Course
    {
        private sealed class Metadata
        {
            [DisplayName]
            public int Id { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            public string Name { get; set; }

            [DisplayName("Owner")]
            public string Owner { get; set; }

            [DisplayName("Created Date")]
            public DateTime Created { get; set; }

            [DisplayName("Last Upadte")]
            public DateTime Updated { get; set; }

        }
    }

    [MetadataType(typeof(Node.Metadata))]
    [Bind(Exclude = "Id, ParentId, CourseId")]
    public partial class Node
    {
        private sealed class Metadata
        {
            
        }
    }
}