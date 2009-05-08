using System;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Common.Expressions
{
    public class Equal<T> : BoolExpression<T>
        where T : IComparable<T>
    {
        public Equal(IValue<T> a, IValue<T> b)
            : base(a, b)
        {
            _a.Changed += ValueChanged;
            _b.Changed += ValueChanged;
        }

        public override bool Value
        {
            get
            {
                if ((_a.Value == null) != (_b.Value == null))
                {
                    return false;
                }
                if (_a.Value == null && _b.Value == null)
                {
                    return true;
                }
                return _a.Value.CompareTo(_b.Value) == 0;
            }
        }
    }
}
