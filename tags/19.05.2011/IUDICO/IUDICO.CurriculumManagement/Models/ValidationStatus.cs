using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.CurriculumManagement.Models
{
    /// <summary>
    /// ValidationStatus.
    /// </summary>
    public class ValidationStatus
    {
        public bool IsValid 
        {
            get
            {
                return Errors.Count() == 0;
            }
        }
        public List<string> Errors { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationStatus"/> class.
        /// Initial status is valid.
        /// </summary>
        public ValidationStatus()
        {
            Errors = new List<string>();
        }
    }
}