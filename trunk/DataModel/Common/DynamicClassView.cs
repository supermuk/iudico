using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;

namespace IUDICO.DataModel.Common
{
    public class DynamicClassView : List<DynamicClass>, ITypedList, IViewStateSerializable 
    {
        #region DSPropertyDescriptor

        private class DSPropertyDescriptor : PropertyDescriptor
        {
            public DSPropertyDescriptor(string name, Type type, bool isReadonly)
                : base(name, __EmptyAttributes)
            {
                _Readonly = isReadonly;
                _Type = type;
            }

            #region Overrides of PropertyDescriptor

            public override bool CanResetValue(object component)
            {
                return !Readonly;
            }

            public override object GetValue(object component)
            {
                return ((DynamicClass) component).GetPropertyValue(Name);
            }

            public override void ResetValue(object component)
            {
                throw new NotImplementedException();
            }

            public override void SetValue(object component, object value)
            {
                throw new NotImplementedException();
            }

            public override bool ShouldSerializeValue(object component)
            {
                return true;
            }

            public override Type ComponentType
            {
                get { return typeof (DynamicClass); }
            }

            public override bool IsReadOnly
            {
                get { return Readonly; }
            }

            public override Type PropertyType
            {
                get { return _Type; }
            }

            public bool Readonly
            {
                get { return _Readonly; }
            }

            #endregion

            private readonly bool _Readonly;
            private readonly Type _Type;
        }

        #endregion

        public DynamicClassView()
        {
        }

        public DynamicClassView(int capacity)
            : base(capacity)
        {
        }

        public DynamicClass Add()
        {
            return new DynamicClass(this);
        }

        public DynamicClassView(IEnumerable<DynamicClass> collection)
            : base(collection)
        {
        }

        #region Implementation of ITypedList

        public string GetListName(PropertyDescriptor[] listAccessors)
        {
            return "DynamicClassView";
        }

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return _Properties;
        }

        #endregion

        #region Implementation of IViewStateSerializable

        public object SaveViewStateData()
        {
            var props = new object[_Properties.Count];
            for (var i = _Properties.Count - 1; i >= 0; --i)
            {
                var p = (DSPropertyDescriptor) _Properties[i];
                props[i] = new Triplet(p.Name, p.PropertyType.FullName, p.Readonly);
            }

            var values = new List<object>(this.Select(dc => dc.SaveViewStateData())).ToArray();

            return new Pair(props, values);
        }

        public void LoadViewStateData(object data)
        {
            var p = (Pair) data;
            var props = (object[]) p.First;
            var values = (object[]) p.Second;

            foreach (Triplet pi in props)
            {
                DefineProperty((string)pi.First, __DefinedTypes[(string)pi.Second], (bool)pi.Third);
            }

            foreach (var o in values)
            {
                Add().LoadViewStateData(o);
            }
        }

        #endregion

        public void DefineProperty(string name, Type type, bool @readonly)
        {
            if (!__DefinedTypes.ContainsKey(type.FullName))
            {
                __DefinedTypes.Add(type.FullName, type);
            }

            _Properties.Add(new DSPropertyDescriptor(name, type, @readonly));
        }

        public void DefineProperty(string name, Type type)
        {
            DefineProperty(name, type, false);
        }

        private readonly PropertyDescriptorCollection _Properties = new PropertyDescriptorCollection(__EmptyProperties);
        private static readonly PropertyDescriptor[] __EmptyProperties = new PropertyDescriptor[0];
        private static readonly Attribute[] __EmptyAttributes = new Attribute[0];
        private static readonly Dictionary<string, Type> __DefinedTypes = new Dictionary<string, Type>();
    }
}
