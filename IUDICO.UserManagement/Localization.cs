using IUDICO.Common;

namespace IUDICO.UserManagement
{
    public class Localization
    {
        private static readonly LocalizationMessageProvider Provider = new LocalizationMessageProvider("UserManagement");

        public static string GetMessage(string search)
        {
            return Provider.getMessage(search);
        }
    }
}