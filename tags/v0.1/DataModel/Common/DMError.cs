using System;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Common
{
    public class DMError : ApplicationException
    {
        public DMError([NotNull] string errorMessage)
            : base(errorMessage)
        {
        }

        [StringFormatMethod("errorMessageFormat")]
        public DMError([NotNull] string errorMessageFormat, params object[] args)
            : this(string.Format(errorMessageFormat, args))
        {
        }
    }
}
