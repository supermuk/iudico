using IUDICO.Common.Controllers;
using IUDICO.Common.Models.Services;
using IUDICO.CurriculumManagement.Models.Storage;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumBaseController : PluginController
    {
        protected ICurriculumManagement Storage { get; private set; }

        public CurriculumBaseController()
        {
            Storage = LmsService.FindService<ICurriculumManagement>();
            Storage.RefreshState();
        }
    }
}