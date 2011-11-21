using IUDICO.Common;
namespace IUDICO.UserManagement
{
    public class Localization
    {
        private static LocalizationMessageProvider provider = new LocalizationMessageProvider("UserManagement");

        public static string getMessage(string search)
        {
            return provider.getMessage(search);
        }
    }
}