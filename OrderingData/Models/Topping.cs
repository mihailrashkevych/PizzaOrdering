using System;
using System.Collections.Generic;

#nullable disable

namespace OrderingData.Models
{
    public partial class Topping
    {
        public Topping()
        {
            OrderItemDetails = new HashSet<OrderItemDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }

        public virtual ICollection<OrderItemDetail> OrderItemDetails { get; set; }
    }
}
