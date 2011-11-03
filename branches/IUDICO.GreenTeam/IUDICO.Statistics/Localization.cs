using IUDICO.Common;
namespace IUDICO.Statistics
{
    public class Localization
    {
        private static LocalizationMessageProvider provider = new LocalizationMessageProvider("Statistics");

        public static string getMessage(string search)
        {
            return provider.getMessage(search);
        }
    }
}