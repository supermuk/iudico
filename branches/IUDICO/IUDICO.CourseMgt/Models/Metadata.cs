using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IUDICO.Common.Models
{
    [MetadataType(typeof(Course.Metadata))]
    [Bind(Exclude="Id")]
    public partial class Course
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            public string Name { get; set; }

            [DisplayName("Owner")]
            public string Owner { get; set; }

            [DisplayName("Created Date")]
            [ScaffoldColumn(false)]
            public DateTime Created { get; set; }

            [DisplayName("Last Updated")]
            [ScaffoldColumn(false)]
            public DateTime Updated { get; set; }

            [ScaffoldColumn(false)]
            public bool Deleted { get; set; }
        }
    }

    [MetadataType(typeof(Node.Metadata))]
    [Bind(Exclude = "Id, CourseId")]
    public partial class Node
    {
        private sealed class Metadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Name is required")]
            public string Name { get; set; }

            [ScaffoldColumn(false)]
            public int? ParentId { get; set; }

            [ScaffoldColumn(false)]
            public int CourseId { get; set; }

            [DisplayName("Is Folder")]
            public bool IsFolder { get; set; }

            [ScaffoldColumn(false)]
            public int Position { get; set; }
        }
    }
}