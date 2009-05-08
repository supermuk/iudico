using System;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Common.Expressions
{
    public abstract class BoolExpression<T> : IValue<bool>
        where T : IComparable<T>
    {
        protected IValue<T> _a;
        protected IValue<T> _b;

        protected BoolExpression(IValue<T> a, IValue<T> b)
        {
            _a = a;
            _b = b;
        }

        protected void ValueChanged(IValue<T> arg1, T arg2)
        {
            if (Changed != null)
            {
                Changed(this, Value);
            }
        }

        public abstract bool Value { get; }

        public event Action<IValue<bool>, bool> Changed;
    }
}