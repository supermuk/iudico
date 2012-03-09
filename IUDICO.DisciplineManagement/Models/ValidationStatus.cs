﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace IUDICO.DisciplineManagement.Models
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
                return !Errors.Any();
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

        /// <summary>
        /// Adds the localized error.
        /// </summary>
        /// <param name="key">The key in resource file.</param>
        /// <param name="args">The args.</param>
        public void AddLocalizedError(string key, params object[] args)
        {
            Errors.Add(String.Format(Localization.getMessage(key), args));
        }
    }
}