using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.CurriculumManagement.Models
{
    public class ValidationStatus
    {
        public bool IsValid { get; private set; }
        public string Message { get; private set; }

        public ValidationStatus(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }
    }
}