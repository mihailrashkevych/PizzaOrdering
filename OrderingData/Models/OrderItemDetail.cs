using System;
using System.Collections.Generic;

#nullable disable

namespace OrderingData.Models
{
    public partial class OrderItemDetail
    {
        public int OrderDetailsId { get; set; }
        public int ToppingId { get; set; }

        public virtual OrderDetail OrderDetails { get; set; }
        public virtual Topping Topping { get; set; }
    }
}
