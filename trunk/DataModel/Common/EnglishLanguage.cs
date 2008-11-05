using LEX.CONTROLS;

namespace IUDICO.DataModel.Common
{
    public static class EnglishLanguage
    {
        public static string Pluralize([NotNull] this string word)
        {
            // TODO: Implement better algorithm
            return word + "s";
        }

        public static string Capitalize([NotNull] this string word)
        {
            return word[0].ToString().ToUpper()[0] + word.Remove(0, 1);
        }
    }
}
