using System;
using System.Reflection;
using IUDICO.DataModel.Common;
using System.Linq;

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

        public static implicit operator PropertyCondition (string name)
        {
            return new PropertyCondition(name);
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

    public class ValueCondition<TValue> : IDBCondition
    {
        public TValue Value { get; private set; }

        public ValueCondition(TValue value)
        {
            Value = value;
        }

        public void Write(SqlSerializationContext context)
        {
            context.Write(context.AddParameter(Value));            
        }

        public static implicit operator ValueCondition<TValue> (TValue v)
        {
            return new ValueCondition<TValue>(v);
        }
    }

    public enum IN_CONDITION_KIND
    {
        IN,
        NOT_IN,
        EQUAL
    }

    public class InCondition : IDBCondition
    {
        public readonly IDBCondition Arg;
        public readonly ISubSelectCondition SubSelect;
        public readonly IN_CONDITION_KIND Kind;

        public InCondition(IDBCondition arg, ISubSelectCondition subSelect)
            : this(arg, subSelect, IN_CONDITION_KIND.IN)
        {
        }

        public InCondition(IDBCondition arg, ISubSelectCondition subSelect, IN_CONDITION_KIND kind)
        {
            Arg = arg;
            SubSelect = subSelect;
            Kind = kind;
        }

        public void Write(SqlSerializationContext context)
        {
            Arg.Write(context);
            context.Write(" ");
            context.Write(KindToString(Kind));
            context.Write(" (");
            SubSelect.Write(context);
            context.Write(")");
        }

        private static string KindToString(IN_CONDITION_KIND kind)
        {
            switch (kind)
            {
                case IN_CONDITION_KIND.EQUAL:
                    return "=";

                case IN_CONDITION_KIND.IN:
                    return "IN";

                case IN_CONDITION_KIND.NOT_IN:
                    return "NOT IN";

                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public class SubSelectCondition<TDataObject> : ISubSelectCondition
        where TDataObject : IDataObject, new()
    {
        public readonly string[] FieldNames;
        public readonly IDBCondition Condition;

        public SubSelectCondition(string fieldName, IDBCondition cond)
            : this(new[] {fieldName}, cond)
        {
        }

        public SubSelectCondition(string[] fieldNames, IDBCondition cond)
        {
            FieldNames = fieldNames;
            Condition = cond;
        }

        public void Write(SqlSerializationContext context)
        {
            context.Write("SELECT ");
            context.Write(FieldNames.Select(f => SqlUtils.WrapDbId(f)).ConcatComma());
            context.Write(" FROM ");
            context.Write(SqlUtils.WrapDbId(DataObjectInfo<TDataObject>.TableName));
            if (Condition != null)
            {
                context.Write(" WHERE ");
                Condition.Write(context);
            }
        }
    }
}