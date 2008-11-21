using System;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Controllers
{
    [AttributeUsage(AttributeTargets.Field)]
    [BaseTypeRequired(typeof(PageControllerBase))]
    public sealed class ControllerValueAttribute : Attribute
    {
    }

    public abstract class PageControllerBase
    {
    }
}
