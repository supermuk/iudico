using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.CurriculumManagement.Models.Enums;

namespace IUDICO.CurriculumManagement.Models {
   public class ValidationError {
      public ValidationErrorType Type { get; set; }
      public object ErrorData { get; set; }
   }
}