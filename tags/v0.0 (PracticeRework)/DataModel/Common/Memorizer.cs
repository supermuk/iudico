using System;
using System.Collections.Generic;

namespace IUDICO.DataModel.Common
{
    public class Memorizer<TType, TValue>
    {
        public Memorizer(Func<TType, TValue> calculator)
        {
            _Calculator = calculator;
        }

        public TValue Get(TType t)
        {
            TValue res;
            if (!_Values.TryGetValue(t, out res))
            {
                res = _Calculator(t);
                _Values.Add(t, res);
            }
            return res;
        }

        public static implicit operator Func<TType, TValue> (Memorizer<TType, TValue> m)
        {
            return m.Get;
        }

        private readonly Func<TType, TValue> _Calculator;
        private readonly Dictionary<TType, TValue> _Values = new Dictionary<TType, TValue>();
    }

    public static class MemorizeHelper
    {
        public static Func<T, TResult> Memorize<T, TResult>(this Func<T, TResult> func)
        {
            return new Memorizer<T, TResult>(func);
        }
    }
}
