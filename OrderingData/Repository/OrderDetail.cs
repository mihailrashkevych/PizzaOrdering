using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OrderingData.Repository
{
    public class OrderDetail
    {
        readonly Models.Pizza_Ordering_DbContext context;
        public OrderDetail()
        {
            context = new Models.Pizza_Ordering_DbContext();
        }

        public async Task<IEnumerable<Models.OrderDetail>> GetByOrder(int orderId)
        {
            return await context.OrderDetails.Where(o => o.OrderId == orderId).Include(o => o.Pizza).Include(o => o.OrderItemDetails).ThenInclude(o=>o.Topping).ToListAsync();
        }

        public async Task<Models.OrderDetail> Add(Models.OrderDetail orderDetail)
        {
            var entry = await context.OrderDetails.AddAsync(orderDetail);
            await SaveAsync();
            return entry.Entity;
        }

        public async Task Remove(int id)
        {
            var entity = await context.OrderDetails.FindAsync(id);
            context.OrderDetails.Remove(entity);
            await SaveAsync();
        }

        private async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
