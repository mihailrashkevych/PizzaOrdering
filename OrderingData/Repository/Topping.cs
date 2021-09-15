using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingData.Repository
{
    public class Topping
    {
        readonly Models.Pizza_Ordering_DbContext context;
        public Topping()
        {
            context = new Models.Pizza_Ordering_DbContext();
        }

        public async Task<IEnumerable<Models.Topping>> GetAll()
        {
            return await context.Toppings.ToListAsync();
        }

        public async Task<Models.Topping> GetByName(string name)
        {
            return await context.Toppings.Where(p => p.Name == name).FirstOrDefaultAsync();
        }
    }
}
