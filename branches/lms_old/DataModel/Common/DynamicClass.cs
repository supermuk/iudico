using System;
using System.Collections.Generic;
using System.ComponentModel;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Common
{
    public class DynamicClass : CustomTypeDescriptor, IViewStateSerializable
    {
        public DynamicClass([NotNull] DynamicClassView owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }

            _Owner = owner;
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            return _Owner.GetItemProperties(null);
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public void AddProperty(string name, object value)
        {
            _Properties.Add(name, value);
        }

        public void RemoveProperty(string name)
        {
            throw new NotImplementedException();
        }

        public object GetPropertyValue(string name)
        {
            return _Properties[name];
        }

        #region Implementation of IViewStateSerializable

        public object SaveViewStateData()
        {
            PropertyDescriptorCollection props = GetProperties();
            var res = new object[props.Count];
            for (var i = props.Count - 1; i >= 0; i--)
            {
                res[i] = GetPropertyValue(props[i].Name);
            }
            return res;
        }

        public void LoadViewStateData(object data)
        {
            PropertyDescriptorCollection props = GetProperties();
            var ds = (object[])data;
            for (var i = props.Count - 1; i >= 0; i--)
            {
                AddProperty(props[i].Name, ds[i]);
            }
        }

        #endregion

        private readonly Dictionary<string, object> _Properties = new Dictionary<string, object>();
        private readonly DynamicClassView _Owner;
    }
}
