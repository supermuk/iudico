using IUDICO.Common;
namespace IUDICO.CourseManagement
{
    public class Localization
    {
        private static LocalizationMessageProvider provider = new LocalizationMessageProvider("CourseManagement");

        public static string getMessage(string search)
        {
            return provider.getMessage(search);
        }
    }
}