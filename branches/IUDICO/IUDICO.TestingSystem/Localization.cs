// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Localization.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using IUDICO.Common;

namespace IUDICO.TestingSystem
{
    public class Localization
    {
        private static readonly LocalizationMessageProvider Provider = new LocalizationMessageProvider("TestingSystem");

        public static string GetMessage(string search)
        {
            return Provider.GetMessage(search);
        }
    }
}