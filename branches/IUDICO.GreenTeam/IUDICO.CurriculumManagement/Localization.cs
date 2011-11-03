using IUDICO.Common;
namespace IUDICO.CurriculumManagement
{
    public class Localization
    {
        private static LocalizationMessageProvider provider = new LocalizationMessageProvider("CurriculumManagement");

        public static string getMessage(string search)
        {
            return provider.getMessage(search);
        }
    }
}