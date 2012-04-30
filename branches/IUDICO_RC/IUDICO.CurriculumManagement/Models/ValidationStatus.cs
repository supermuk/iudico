using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.CurriculumManagement.Models
{
    using IUDICO.Common;

    /// <summary>
    /// ValidationStatus.
    /// </summary>
    public class ValidationStatus
    {
        public bool IsValid
        {
            get
            {
                return !this.Errors.Any();
            }
        }
        public List<string> Errors { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationStatus"/> class.
        /// Initial status is valid.
        /// </summary>
        public ValidationStatus()
        {
            this.Errors = new List<string>();
        }

        /// <summary>
        /// Adds the localized error.
        /// </summary>
        /// <param name="key">The key in resource file.</param>
        public void AddLocalizedError(string key, params object[] args)
        {
            this.Errors.Add(string.Format(Localization.GetMessage(key), args));
        }
    }
}