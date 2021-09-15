using System;
using System.Collections.Generic;

#nullable disable

namespace OrderingData.Models
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            OrderItemDetails = new HashSet<OrderItemDetail>();
        }

        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? PizzaId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Pizza Pizza { get; set; }
        public virtual ICollection<OrderItemDetail> OrderItemDetails { get; set; }
    }
}
