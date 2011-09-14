using IUDICO.DataModel.DB;
using TestingSystem;

namespace IUDICO.DataModel.Common.StudentUtils
{
    /// <summary>
    /// Class to represent data of programming language
    /// </summary>
    public static class LanguageHelper
    {
        public static Language FxLanguagesToLanguage(int id)
        {
            if (FxLanguages.Delphi7.ID == id)
                return Language.Delphi7;

            if (FxLanguages.DotNet2.ID == id)
                return Language.CSharp2;

            if (FxLanguages.DotNet3.ID == id)
                return Language.CSharp3;

            if (FxLanguages.Java6.ID == id)
                return Language.Java6;

            if (FxLanguages.Vs6CPlusPlus.ID == id)
                return Language.VC6;

            if (FxLanguages.Vs8CPlusPlus.ID == id)
                return Language.VC8;

            return Language.CSharp3;
        }
    }
}