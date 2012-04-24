using IUDICO.Common;
namespace IUDICO.LMS
{
    public class Localization
    {
        private static LocalizationMessageProvider provider = new LocalizationMessageProvider("LMS");

        public static string GetMessage(string search)
        {
            return provider.GetMessage(search);
        }
    }
}