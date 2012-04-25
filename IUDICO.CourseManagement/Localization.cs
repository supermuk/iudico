using IUDICO.Common;
namespace IUDICO.CourseManagement
{
    public class Localization
    {
        private static LocalizationMessageProvider provider = new LocalizationMessageProvider("CourseManagement");

        public static string GetMessage(string search)
        {
            return provider.GetMessage(search);
        }
    }
}