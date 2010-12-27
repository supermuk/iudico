﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;

namespace IUDICO.CurriculumManagement.Models
{
    public class CreateAssignmentModel
    {
        public IEnumerable<SelectListItem> Groups { get; set; }
        public int GroupId { get; set; }
    }
}