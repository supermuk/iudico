using IUDICO.DataModel.DB;
using TestingSystem;

namespace IUDICO.DataModel.Common.StudentUtils
{
    public static class LanguageHelper
    {
        public static Language FxLanguagesToLanguage(int id)
        {
            if (FxLanguages.Delphi7.ID == id)
                return Language.Delphi7;

            if (FxLanguages.DotNet2.ID == id)
                return Language.DotNet2;

            if (FxLanguages.DotNet3.ID == id)
                return Language.DotNet3;

            if (FxLanguages.Java6.ID == id)
                return Language.Java6;

            if (FxLanguages.Vs6CPlusPlus.ID == id)
                return Language.Vs6CPlusPlus;

            if (FxLanguages.Vs8CPlusPlus.ID == id)
                return Language.Vs8CPlusPlus;

            return Language.DotNet3;
        }
    }
}