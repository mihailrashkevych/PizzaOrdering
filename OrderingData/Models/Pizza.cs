using System;
using System.Collections.Generic;

#nullable disable

namespace OrderingData.Models
{
    public partial class Pizza
    {
        public Pizza()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string PType { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
