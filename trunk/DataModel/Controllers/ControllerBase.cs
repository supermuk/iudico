using System;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Controllers
{
    [AttributeUsage(AttributeTargets.Field)]
    [BaseTypeRequired(typeof(ControllerBase))]
    public sealed class ControllerValueAttribute : Attribute
    {
    }

    public abstract class ControllerBase
    {
    }
}
