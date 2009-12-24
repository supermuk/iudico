using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Common
{
    public interface IMemberAL
    {
        object this[object obj] { get; set; }
        Type MemberType { get; }
    }

    public static class AbstractionLayerExtenders
    { 
        [NotNull]
        public static IMemberAL ToAbstaction([NotNull] this FieldInfo fieldInfo)
        {
            return new FieldInfoAbstaction(fieldInfo);
        }

        [NotNull]
        public static IMemberAL ToAbstraction([NotNull] this PropertyInfo propertyInfo)
        {
            return new PropertyInfoAbstraction(propertyInfo);
        }

        [CanBeNull]
        public static IMemberAL GetMember([NotNull] this IMemberAL owner, [NotNull]string memberName)
        {
            var t = owner.MemberType;
            var f = t.GetField(memberName);
            if (f != null)
            {
                return new ChainAbstraction(owner, f.ToAbstaction());
            }
            var p = t.GetProperty(memberName);
            if (p != null)
            {
                return new ChainAbstraction(owner, p.ToAbstraction());
            }
            return null;
        }

#region Internal Types
        [DebuggerDisplay("Field: {_F}")]
        private struct FieldInfoAbstaction : IMemberAL
        {
            public FieldInfoAbstaction([NotNull] _FieldInfo f)
            {
                _F = f;
            }

            public object this[object obj] 
            { 
                get
                {
                    return _F.GetValue(obj);
                }  
                set
                {
                    _F.SetValue(obj, value);   
                } 
            }

            public Type MemberType
            {
                get { return _F.FieldType; }
            }

            private readonly _FieldInfo _F;
        }

        [DebuggerDisplay("Property: {_P}")]
        private struct PropertyInfoAbstraction: IMemberAL
        {
            public PropertyInfoAbstraction(_PropertyInfo p)
            {
                _P = p;
            }

            public object this[object obj]
            {
                get { return _P.GetValue(obj, null); }
                set { _P.SetValue(obj, value, null); }
            }

            public Type MemberType
            {
                get { return _P.PropertyType; }
            }

            private readonly _PropertyInfo _P;
        }

        private struct ChainAbstraction : IMemberAL
        {
            public ChainAbstraction(IMemberAL owner, IMemberAL member)
            {
                _Owner = owner;
                _Member = member;
            }

            public object this[object obj]
            {
                get { return _Member[_Owner[obj]]; }
                set { _Member[_Owner[obj]] = value; }
            }

            public Type MemberType
            {
                get { return _Member.MemberType; }
            }

            private readonly IMemberAL _Owner;
            private readonly IMemberAL _Member;
        }
#endregion

    }
}
