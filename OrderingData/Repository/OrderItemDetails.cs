using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingData.Repository
{
    public class OrderItemDetails
    {
        readonly Models.Pizza_Ordering_DbContext context;
        public OrderItemDetails()
        {
            context = new Models.Pizza_Ordering_DbContext();
        }

        public async Task Add(Models.OrderItemDetail orderItemDetail)
        {
            await context.OrderItemDetails.AddAsync(orderItemDetail);
            await SaveAsync();
        }

        public async Task<bool> IsExist(Models.OrderItemDetail orderItemDetail)
        {
            if (await context.OrderItemDetails.AnyAsync(o => o.OrderDetailsId == orderItemDetail.OrderDetailsId && o.ToppingId == orderItemDetail.ToppingId))
            {
                return true;
            }
            else return false;
        }

        private async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
