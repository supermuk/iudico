using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IUDICO.Common.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DropDownListAttribute : UIHintAttribute
    {
        public DropDownListAttribute()
            : base("DropDownList")
        {
            OptionLabel = Localization.GetMessage(OptionLabel);
        }

        public string SourceProperty { get; set; }
        public string OptionLabel { get; set; }
        public IEnumerable<SelectListItem> List { get; set; }
    }
}
