using System;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Common.Expressions
{
    public class NotEqual<T> : IValue<bool>
        where T : IComparable<T>
    {
        public NotEqual(IValue<T> a, IValue<T> b)
        {
            _equal = new Equal<T>(a, b);
        }

        #region Implementation of IValue<bool>

        public bool Value
        {
            get { return !_equal.Value; }
        }

        public event Action<IValue<bool>, bool> Changed
        {
            add
            {
                _equal.Changed += value;
            }
            remove
            {
                _equal.Changed -= value;
            }
        }

        #endregion

        private readonly Equal<T> _equal;
    }
}
