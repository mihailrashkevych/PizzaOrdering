using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingData.Repository
{
    public class User
    {
        readonly Models.Pizza_Ordering_DbContext context;
        public User()
        {
            context = new Models.Pizza_Ordering_DbContext();
        }

        public async Task<Models.User> AddUserAsync(Models.User user)
        {
            if (user == null)
            {
                return null;

            }
            var entry = await context.Users.AddAsync(user);
            await SaveAsync();
            return entry.Entity;
        }

        public async Task<Models.User> GetUserAsync(string email, string password)
        {
            return await context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
        }

        private async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
