using System;
using IUDICO.Common.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.DisciplineManagement.Models;
using IUDICO.DisciplineManagement.Models.Storage;
using IUDICO.DisciplineManagement.Models.ViewDataClasses;

namespace IUDICO.DisciplineManagement.Controllers
{
    /// <summary>
    /// DisciplineBaseController.
    /// </summary>
    public class DisciplineBaseController : PluginController
    {
        protected IDisciplineStorage Storage { get; private set; }
        protected Validator Validator { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisciplineBaseController"/> class.
        /// </summary>
        /// <param name="disciplineStorage">The discipline storage.</param>
        public DisciplineBaseController(IDisciplineStorage disciplineStorage)
        {
            this.Storage = disciplineStorage;
            this.Validator = new Validator(this.Storage);
        }

        /// <summary>
        /// Adds errors to model state.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public void AddValidationErrorsToModelState(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error); // should be string.Empty!
            }
        }
    }
}