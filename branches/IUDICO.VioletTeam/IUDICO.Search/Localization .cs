using IUDICO.Common;
namespace IUDICO.Search
{
    public class Localization
    {
        private static LocalizationMessageProvider provider = new LocalizationMessageProvider("Search");

        public static string getMessage(string search)
        {
            return provider.getMessage(search);
        }
    }
}