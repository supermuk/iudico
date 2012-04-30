using IUDICO.Common;

namespace IUDICO.Security.ViewModels
{
    public class LocalizedViewModel
    {
        public string GetMessage(string key)
        {
            return Localization.GetMessage(key);
        }
    }
}