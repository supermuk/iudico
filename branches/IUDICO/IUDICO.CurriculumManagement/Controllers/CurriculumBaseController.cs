using IUDICO.Common.Controllers;
using IUDICO.Common.Models.Services;

namespace IUDICO.CurriculumManagement.Controllers
{
    public class CurriculumBaseController: PluginController
    {
        protected ICurriculumManagement Storage
        {
            get
            {
                return LmsService.FindService<ICurriculumManagement>();
            }
        }
    }
}