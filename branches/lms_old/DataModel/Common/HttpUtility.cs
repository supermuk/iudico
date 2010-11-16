namespace CourseImport.Common
{
    public static class HtmlUtility
    {
        ///<summary>
        /// Encodes quotes of <see cref="text" /> to can be used in Html code
        ///</summary>
        ///<param name="text">Text to treat</param>
        ///<returns>Text with encoded quotes</returns>
        public static string QuotesEncode(string text)
        {
            return !string.IsNullOrEmpty(text) ? text.Replace("\"", "\\\"").Replace("'", "\'") : text;
        }

        ///<summary>
        ///  Decoded quotes of <see cref="text"/> to can be used in design mode
        ///</summary>
        ///<param name="text">Text to decode</param>
        ///<returns>Decoded string</returns>
        public static string QuotesDecode(string text)
        {
            return !string.IsNullOrEmpty(text) ? text.Replace("\\\"", "\"").Replace("\'", "'") : text;
        }
    }
}