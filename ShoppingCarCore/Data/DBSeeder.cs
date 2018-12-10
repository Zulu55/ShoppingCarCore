using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using ShoppingCarCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCarCore.Data
{
    public class DBSeeder
    {
        private readonly ApplicationDbContext context;
        private readonly IHostingEnvironment hosting;
        private readonly UserManager<StoreUser> userManager;

        public DBSeeder(ApplicationDbContext context, IHostingEnvironment hosting, UserManager<StoreUser> userManager)
        {
            this.context = context;
            this.hosting = hosting;
            this.userManager = userManager;
        }

        public async Task SeedAsync()
        {
            this.context.Database.EnsureCreated();

            var user = await this.userManager.FindByEmailAsync("jzuluaga55@hotmail.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    LastName = "Zuluaga",
                    FirstName = "Juan",
                    Email = "jzuluaga55@hotmail.com",
                    UserName = "jzuluaga55@hotmail.com"
                };

                var result = await this.userManager.CreateAsync(user, "Roger1974.");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create user in Seeding");
                }
            }
        }
    }
}
