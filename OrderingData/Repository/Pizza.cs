using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingData.Repository
{
    public class Pizza
    {
        readonly Models.Pizza_Ordering_DbContext context;
        public Pizza()
        {
            context = new Models.Pizza_Ordering_DbContext();
        }

        public async Task<IEnumerable<Models.Pizza>> GetAll()
        {
            return await context.Pizzas.ToListAsync();
        }

        public async Task<Models.Pizza> GetByName(string name)
        {
            return await context.Pizzas.Where(p => p.Name == name).FirstOrDefaultAsync();
        }
    }
}
