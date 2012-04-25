using IUDICO.Common;
namespace IUDICO.CurriculumManagement
{
    public class Localization
    {
        private static LocalizationMessageProvider provider = new LocalizationMessageProvider("CurriculumManagement");

        public static string GetMessage(string search)
        {
            return provider.GetMessage(search);
        }
    }
}