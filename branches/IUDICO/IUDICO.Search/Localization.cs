using IUDICO.Common;
namespace IUDICO.Search
{
    public class Localization
    {
        private static LocalizationMessageProvider provider = new LocalizationMessageProvider("Search");

        public static string GetMessage(string search)
        {
            return provider.GetMessage(search);
        }
    }
}