using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace IUDICO.UM.Models
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class EmailAddressAttribute : DataTypeAttribute
    {
        private readonly Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.Compiled);

        public EmailAddressAttribute()
            : base(DataType.EmailAddress)
        {
        }

        public override bool IsValid(object value)
        {
            string str = Convert.ToString(value, CultureInfo.CurrentCulture);

            if (string.IsNullOrEmpty(str) == true)
            {
                return true;
            }

            Match match = regex.Match(str);

            return ((match.Success && (match.Index == 0)) && (match.Length == str.Length));
        }
    }
}