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

        public static string getMessage(string search)
        {
            return provider.getMessage(search);
        }

        public static class Keys
        {
            public static readonly string SECURITY = "Security";
            public static readonly string SECURITY_PLUGIN = "SecurityPlugin";
        }
    }
}