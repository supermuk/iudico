using System;
using System.ComponentModel.DataAnnotations;

namespace IUDICO.Common.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class DropDownListAttribute : UIHintAttribute
    {
        public DropDownListAttribute()
            : base("DropDownList")
        {
        }

        public string TargetProperty { get; set; }
        public string OptionLabel { get; set; }
    }
}
