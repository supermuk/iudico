using IUDICO.Common;
namespace IUDICO.LMS
{
    public class Localization
    {
        private static LocalizationMessageProvider provider = new LocalizationMessageProvider("LMS");

        public static string getMessage(string search)
        {
            return provider.getMessage(search);
        }
    }
}