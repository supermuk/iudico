using System;
using IUDICO.Common.Controllers;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace IUDICO.CurriculumManagement.Controllers
{
    /// <summary>
    /// CurriculumBaseController.
    /// </summary>
    public class CurriculumBaseController : PluginController
    {
        protected ICurriculumStorage Storage { get; private set; }
        protected Validator Validator { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurriculumBaseController"/> class.
        /// </summary>
        /// <param name="disciplineStorage">The discipline storage.</param>
        public CurriculumBaseController(ICurriculumStorage disciplineStorage)
        {
            Storage = disciplineStorage;
            Validator = new Validator(Storage);
        }

        /// <summary>
        /// Adds errrors to model state.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public void AddValidationErrorsToModelState(IEnumerable<string> errors)
        {
            foreach (string error in errors)
            {
                ModelState.AddModelError(string.Empty, error);//should be string.Empty!
            }
        }
    }
}