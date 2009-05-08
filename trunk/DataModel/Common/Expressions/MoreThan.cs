using System;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Common.Expressions
{
    public class MoreThan<T> : BoolExpression<T>
        where T : IComparable<T>
    {
        public MoreThan(IValue<T> a, IValue<T> b) 
            : base(a, b)
        {
        }

        public override bool Value
        {
            get { return _a.Value.CompareTo(_b.Value) > 0; }
        }
    }
}
