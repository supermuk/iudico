using System;


namespace IUDICO.Common.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class OrderAttribute : Attribute
    {
        public int Order { private set; get;  }

        public OrderAttribute(int order)
        {
            this.Order = order;
        }
    }
}
