using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.CurriculumManagement.Models {
   public class ValidationErrorList {
      public List<ValidationError> Errors { get; set; }
      public List<string> ErrorsText { get; set; }
   }
}