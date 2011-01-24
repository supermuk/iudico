using IUDICO.Common.Controllers;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.CurriculumManagement.Models;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumBaseController : PluginController
    {
        protected ICurriculumStorage Storage { get; private set; }
        protected Validator Validator { get; private set; }

        public CurriculumBaseController(ICurriculumStorage curriculumStorage)
        {
            Storage = curriculumStorage;
            Validator = new Validator(Storage);
            Storage.RefreshState();
        }
    }
}