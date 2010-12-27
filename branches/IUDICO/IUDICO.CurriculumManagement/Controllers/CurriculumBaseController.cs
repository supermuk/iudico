using IUDICO.Common.Controllers;
using IUDICO.CurriculumManagement.Models.Storage;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumBaseController : PluginController
    {
        protected ICurriculumStorage Storage { get; private set; }

        public CurriculumBaseController(ICurriculumStorage curriculumStorage)
        {
            Storage = curriculumStorage;
            Storage.RefreshState();
        }
    }
}