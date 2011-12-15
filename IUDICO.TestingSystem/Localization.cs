using IUDICO.Common;
namespace IUDICO.TestingSystem
{
    public class Localization
    {
        private static LocalizationMessageProvider provider = new LocalizationMessageProvider("TestingSystem");

        public static string getMessage(string search)
        {
            return provider.getMessage(search);
        }
    }
}