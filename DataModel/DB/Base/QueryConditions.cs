using System;
using System.Reflection;

namespace IUDICO.DataModel.DB.Base
{
    public class OrCondtion : IDBCondition
    {
        public OrCondtion(IDBCondition a, IDBCondition b)
        {
            _A = a;
            _B = b;
        }

        public void Write(SqlSerializationContext context)
        {
            context.Write("(");
            _A.Write(context);
            context.Write(" OR ");
            _B.Write(context);
            context.Write(")");
        }

        private readonly IDBCondition _A, _B;
    }

    public class AndCondtion : IDBCondition
    {
        public AndCondtion(IDBCondition a, IDBCondition b)
        {
            _A = a;
            _B = b;
        }

        public void Write(SqlSerializationContext context)
        {
            context.Write("(");
            _A.Write(context);
            context.Write(" AND ");
            _B.Write(context);
            context.Write(")");
        }

        private readonly IDBCondition _A, _B;
    }

    public class EqualCondition : IDBCondition
    {
        public EqualCondition(object param)
        {
            _Param = param;
        }

        public void Write(SqlSerializationContext context)
        {
            bool notFirst = false;
            context.Write("(");
            foreach (var p in _Param.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (notFirst)
                {
                    context.Write(" AND ");
                }
                else
                {
                    notFirst = true;
                }
                context.Write("([{0}] = {1})", p.Name, context.AddParameter(p.GetValue(_Param, null)));
            }
            context.Write(")");
        }

        private readonly object _Param;
    }

    public class PropertyCondition : IDBCondition
    {
        public string PropertyName { get; private set; }

        public PropertyCondition(string propertyName)
        {
            PropertyName = propertyName;
        }

        public void Write(SqlSerializationContext context)
        {
            context.Write("[" + PropertyName + "]");            
        }
    }

    public enum COMPARE_KIND
    {
        EQUAL,
        NOT_EQUAL,
        MORE,
        LESS,
        NOT_MORE,
        NOT_LESS,
        IN,
        LIKE
    }

    public class CompareCondition : IDBCondition
    {
        public IDBCondition A { get; private set; }
        public IDBCondition B { get; private set; }
        public COMPARE_KIND Kind { get; private set; }

        public CompareCondition(IDBCondition a, IDBCondition b, COMPARE_KIND kind)
        {
            A = a;
            B = b;
            Kind = kind;
        }

        public void Write(SqlSerializationContext context)
        {
            context.Write("(");
            A.Write(context);
            string opSym;
            switch(Kind)
            {
                case COMPARE_KIND.EQUAL:
                    opSym = " = ";
                    break;
                case COMPARE_KIND.IN:
                    opSym = " IN ";
                    break;
                case COMPARE_KIND.LESS:
                    opSym = " < ";
                    break;
                case COMPARE_KIND.MORE:
                    opSym = " > ";
                    break;
                case COMPARE_KIND.NOT_EQUAL:
                    opSym = " <> ";
                    break;
                case COMPARE_KIND.NOT_LESS:
                    opSym = " >= ";
                    break;
                case COMPARE_KIND.NOT_MORE:
                    opSym = " <= ";
                    break;

                case COMPARE_KIND.LIKE:
                    opSym = " LIKE ";
                    break;

                default:
                    throw new InvalidOperationException();
            }

            context.Write(opSym);
            B.Write(context);
            context.Write(")");
        }
    }

    public class ValueCondition : IDBCondition
    {
        public object Value { get; private set; }

        public ValueCondition(object value)
        {
            Value = value;
        }

        public void Write(SqlSerializationContext context)
        {
            context.Write(context.AddParameter(Value));            
        }
    }
}