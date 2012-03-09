using IUDICO.Common.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using IUDICO.DisciplineManagement.Models;
using IUDICO.DisciplineManagement.Models.Storage;

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
            Storage = disciplineStorage;
            Validator = new Validator(Storage);
        }

        /// <summary>
        /// Saves model state errors between requests.
        /// </summary>
        public void SaveValidationErrors()
        {
            TempData["ViewData"] = ViewData;
        }

        /// <summary>
        /// Loads model state errors between requests.
        /// </summary>
        public void LoadValidationErrors()
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
                TempData["ViewData"] = null;
            }
        }

        /// <summary>
        /// Adds errrors to model state.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public void AddValidationErrorsToModelState(IEnumerable<string> errors)
        {
            foreach (string error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }
    }
}