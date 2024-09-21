using System;

namespace OrderMaster
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class OrderAttribute : Attribute
    {
        public int OrderID { get; set; }

        public OrderAttribute(int ordera)
        { 
            OrderID = ordera;
        }
    }
}
