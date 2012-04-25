using System.IO;
using System.Reflection;

namespace IUDICO.UnitTests.CourseManagement
{
    using System.Configuration;

    public class BaseCourseManagementTest
    {
        protected string root = Path.Combine(ConfigurationManager.AppSettings["RootTestFolder"], @"CourseManagement\Data");
    }
}
