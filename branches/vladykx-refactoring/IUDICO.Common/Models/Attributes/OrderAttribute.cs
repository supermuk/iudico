using System;


namespace IUDICO.Common.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class OrderAttribute : Attribute
    {
        readonly int _Order;

        public OrderAttribute(int order)
        {
            _Order = order;
        }

        public int Order
        {
            get { return _Order; }
        }
    }
}
