using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingData.Repository
{
    public class Order
    {
        readonly Models.Pizza_Ordering_DbContext context;
        public Order()
        {
            context = new Models.Pizza_Ordering_DbContext();
        }

        public async Task<Models.Order> GetNewOrderAsync()
        {
            var entry = await context.Orders.AddAsync(new Models.Order());
            await SaveAsync();
            return entry.Entity;
        }

        public async Task UpdateOrderAsync(Models.Order order)
        {
            context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await context.Orders.FindAsync(id);
            context.Orders.Remove(entity);
            await SaveAsync();
        }

        private async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
