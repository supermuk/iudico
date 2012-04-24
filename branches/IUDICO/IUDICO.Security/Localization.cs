using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common;

namespace IUDICO.Security
{
    public class Localization
    {
        private static LocalizationMessageProvider provider = new LocalizationMessageProvider("Security");

        public static LocalizationMessageProvider GetProvider()
        {
            return provider;
        }
        
        public static string GetMessage(string search)
        {
            return provider.GetMessage(search);
        }

        public static class Keys
        {
            public static readonly string Security = "Security";
            public static readonly string UserActivity = "UserActivity";
            public static readonly string SecurityPlugin = "SecurityPlugin";
        }
    }
}