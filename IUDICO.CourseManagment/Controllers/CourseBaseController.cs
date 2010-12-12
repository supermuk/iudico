using IUDICO.Common.Controllers;
using IUDICO.Common.Models.Services;

namespace IUDICO.CourseManagement.Controllers
{
    public class CourseBaseController : PluginController
    {
        protected ICourseManagement Storage
        {
            get
            {
                return LmsService.FindService<ICourseManagement>();
            }
        }
    }
}